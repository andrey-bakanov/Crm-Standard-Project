using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using StandardProject.Scheduler.Tasks._Core;
using System;
using System.Configuration;

namespace StandardProject.Scheduler.Tasks.Implementation.Mantenance
{
    public class DeleteEntitiesTask : BaseCrmTask
    {
        public static readonly int DefaultMonthOffset = 3;

        public override void Execute(ICrmTaskContext context)
        {
            string entityName = context.Configuration["entityName"];
            string rawMonth = context.Configuration?["xMonth"];

            int months;
            if (!Int32.TryParse(rawMonth, out months))
            {
                months = DefaultMonthOffset;
            }
            DateTime deadLine = DateTime.Now.AddMonths((-1) * months);


            QueryExpression query = new QueryExpression(entityName);
            query.PageInfo = new PagingInfo();
            query.PageInfo.PageNumber = 1;
            query.PageInfo.Count = 250;
            query.Criteria.AddCondition("createdon", ConditionOperator.LessThan, deadLine);

            var service = context.CreateCrmService();

            EntityCollection result;
            do
            {
                result = service.RetrieveMultiple(query);
                foreach (var entity in result.Entities)
                {
                    try
                    {
                        service.Delete(entity.LogicalName, entity.Id);
                    }
                    catch (Exception exc)
                    {
                        context.Logger.InfoFormat("DeleteEntitiesTask: Ошибка при удалении сущности {0} с id={1} ; {2}", entity.LogicalName, entity.Id, exc);
                    }
                }

                query.PageInfo.PageNumber++;
                query.PageInfo.PagingCookie = result?.PagingCookie;
            }
            while (result.MoreRecords);

            context.Logger.Info("DeleteEntitiesTask: завершение задачи");
        }
    }
}

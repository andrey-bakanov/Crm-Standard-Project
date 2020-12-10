using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System.Collections.Generic;
using System.Linq;

namespace StandardProject.Crm.Extensions
{
    /// <summary>
    /// Extension for IOrganizationService
    /// </summary>
    public static class OrganizationServiceExtensions
    {
        /// <summary>
        /// Returns the result of a query by collecting the results 
        /// </summary>
        /// <param name="service"></param>
        /// <param name="query"></param>
        /// <returns></returns>
        public static List<T> RetrieveAll<T>(this IOrganizationService service,
            QueryExpression query) where T : Entity
        {
            query.PageInfo = new PagingInfo
            {
                PageNumber = 1,
                PagingCookie = null
            };

            query.NoLock = true;
            var result = new List<T>();

            while (true)
            {
                var queryResult = service.RetrieveMultiple(query);

                if (queryResult.Entities.Count == 0)
                {
                    break;
                }

                result.AddRange(queryResult.Entities.Select(e => e.ToEntity<T>()));

                if (!queryResult.MoreRecords)
                {
                    break;
                }

                query.PageInfo.PageNumber++;
                query.PageInfo.PagingCookie = queryResult.PagingCookie;
            }

            return result;
        }
    }
}

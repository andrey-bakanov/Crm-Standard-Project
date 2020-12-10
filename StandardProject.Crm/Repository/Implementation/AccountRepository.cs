using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using StandardProject.Crm.Model;
using System.Collections.Generic;
using System.Linq;

namespace StandardProject.Crm.Repository.Implementation
{
    /// <summary>
    /// Example of repository pattern implementation
    /// </summary>
    public sealed class AccountRepository : BaseRepository<Account>, IAccountRepository
    {
        public AccountRepository(IOrganizationService service) : base(service)
        {
        }

        /// <summary>
        /// Perform search specied value
        /// </summary>
        /// <param name="fieldName">field</param>
        /// <param name="searchPattern">value to search</param>
        /// <param name="columns">column to return</param>
        /// <returns></returns>

        public List<Account> FindByName(string fieldName, string searchPattern, ColumnSet columns)
        {
            QueryExpression query = CreateQuery(columns);
            query.TopCount = 1;
            query.Criteria.AddCondition(fieldName, ConditionOperator.Like, $"%{searchPattern}%");

            return this.RetrieveMultiple(query).Entities.Select(x => x.ToEntity<Account>()).ToList();
        }
    }
}

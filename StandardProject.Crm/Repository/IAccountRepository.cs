using Microsoft.Xrm.Sdk.Query;
using StandardProject.Crm.Model;
using System.Collections.Generic;

namespace StandardProject.Crm.Repository
{
    public interface IAccountRepository : IBaseRepository<Account>
    {
        List<Account> FindByName(string fieldName, string searchPattern, ColumnSet columns);
    }
}
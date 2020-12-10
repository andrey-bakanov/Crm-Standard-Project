using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StandardProject.Scheduler.Tasks
{
    public interface ICrmTaskContext : ITaskContext
    {
        /// <summary>
        /// Создает сервис для доступа к crm
        /// </summary>
        /// <returns></returns>
        IOrganizationService CreateCrmService();
    }
}

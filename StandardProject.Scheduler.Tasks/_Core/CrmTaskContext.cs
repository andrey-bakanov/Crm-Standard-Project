using log4net;
using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StandardProject.Scheduler.Tasks._Core
{
    public class CrmTaskContext : TaskContext, ICrmTaskContext
    {
        public CrmTaskContext(string name, ILog logger, NameValueCollection config) : base(name, logger, config)
        {
        }

        public IOrganizationService CreateCrmService()
        {
            return OrganizationServiceFactory.CreateCrmService();
        }

    }
}

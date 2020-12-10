using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StandardProject.Scheduler.Tasks._Core
{
    /// <summary>
    /// Базовый класс для задач с CRM
    /// </summary>
    public abstract class BaseCrmTask : ITask
    {
        public void Execute(ITaskContext context)
        {
            throw new NotImplementedException();
        }

        public abstract void Execute(ICrmTaskContext context);
    }
}

using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StandardProject.Crm.Logging.Implementation
{
    public sealed class TracingLogger : IApplicationLogger
    {
        private readonly ITracingService _service;

        public TracingLogger(ITracingService service)
        {
            this._service = service ?? throw new ArgumentNullException(nameof(service));
        }
        public void Error(object message)
        {
            _service.Trace(message as string);
        }

        public void Fatal(object message)
        {
            _service.Trace(message as string);
        }

        public void Info(object message)
        {
            _service.Trace(message as string);
        }
    }
}

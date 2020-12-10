using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StandardProject.Crm.Logging
{
    /// <summary>
    /// Generic application logger
    /// </summary>
    public interface IApplicationLogger
    {

        /// <summary>
        /// Log info messages
        /// </summary>
        /// <param name="message"></param>
        void Info(object message);

        /// <summary>
        /// Log an error
        /// </summary>
        /// <param name="message"></param>
        void Error(object message);

        /// <summary>
        /// Log a fatal error
        /// </summary>
        void Fatal(object message);
    }
}

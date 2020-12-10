using log4net;
using System;
using System.Collections.Specialized;

namespace StandardProject.Scheduler.Tasks._Core
{
    /// <summary>
    /// Реализация класса контекста
    /// </summary>
    public class TaskContext : ITaskContext
    {
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public ILog Logger { get; private set; }

        public NameValueCollection Configuration { get; private set; }

        public TaskContext(string name, ILog logger, NameValueCollection configuration)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Logger = logger ?? throw new ArgumentNullException(nameof(logger));
            Configuration = configuration;
        }

    }
}

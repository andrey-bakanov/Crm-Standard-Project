using log4net;
using System.Collections.Specialized;

namespace StandardProject.Scheduler.Tasks
{
    /// <summary>
    /// Интерфейс контекста для выполняющейся задачи
    /// </summary>
    public interface ITaskContext
    {
        /// <summary>
        /// Имя задачи
        /// </summary>
        string Name { get;  }

        /// <summary>
        /// Логгер задачи
        /// </summary>
        ILog Logger { get;  }

        /// <summary>
        /// Конфигурация для плагина
        /// </summary>
        NameValueCollection Configuration { get; }
    }
}

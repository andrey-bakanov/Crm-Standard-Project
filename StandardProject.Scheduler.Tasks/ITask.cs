using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StandardProject.Scheduler.Tasks
{
    /// <summary>
    /// Интерфейс для выполнения заданий.
    /// 
    /// Заданием можно считать любую выделенную часть логики, которая может встречаться в разных частях приложения
    /// и может быть доступной через дополнительные сервисы
    /// 
    /// 
    /// </summary>
    /// <example>
    /// public class MyTask : ITask
    /// {
    ///     public void Execute(ITaskContext context)
    ///     {
    ///         context.Logger.Info("Старт..");
    ///         
    ///         //Код задачи
    ///         Thread.CurrentThread.Sleep(5000);
    ///         
    ///         context.Logger.Info("Завершение...");
    ///     }
    /// }
    /// </example>

    public interface ITask
    {
        /// <summary>
        /// Метод выполнения задания
        /// </summary>
        /// <param name="context">Ссылка на контекст</param>

        void Execute(ITaskContext context);
    }
}

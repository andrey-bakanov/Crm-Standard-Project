using System;
using System.IO;

namespace StandardProject.Scheduler.Tasks.Implementation.Mantenance
{
    public class DeleteFilesTask : ITask
    {

        /// <summary>
        /// Количество месяцев по умолчанию
        /// </summary>
        public readonly int DefaultMonthOffset = 3;

        public void Execute(ITaskContext context)
        {
            context.Logger.Info("DeleteFilesTask: старт задачи");

            string rawPaths = context.Configuration["folders"];
            string[] paths = rawPaths.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

            string rawMonth = context.Configuration?["xMonth"];
            int months;
            if (!Int32.TryParse(rawMonth, out months))
            {
                months = DefaultMonthOffset;
            }

            foreach (var path in paths)
            {
                DateTime deadLine = DateTime.Now.AddMonths((-1) * months);
                context.Logger.Info("DeleteFilesTask: удаление файлов из папки " + path + " ранее " + deadLine);

                string[] files = Directory.GetFiles(path);
                foreach (var file in files)
                {
                    try
                    {
                        FileInfo info = new FileInfo(file);
                        if (info.LastWriteTime < deadLine)
                        {
                            context.Logger.Info("DeleteFilesTask: Удаляем файл " + Path.GetFileName(file));
                            File.Delete(file);
                        }
                    }
                    catch (Exception exc)
                    {
                        context.Logger.Info("DeleteFilesTask: Ошибка при удалении файлов " + Path.GetFileName(file) + " " + exc);
                    }

                }
            }

            context.Logger.Info("DeleteFilesTask: завершение задачи");
        }
    }
}

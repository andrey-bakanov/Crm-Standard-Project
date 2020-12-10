using log4net;
using log4net.Config;
using StandardProject.Scheduler.Tasks;
using StandardProject.Scheduler.Tasks._Core;
using System;
using System.Collections.Specialized;
using System.Configuration;
using System.IO;

namespace StandardProject.Scheduler
{
    class Program
    {
        private static ILog _Logger;

        static void Main(string[] args)
        {
            try
            {
                FileInfo finfo = new FileInfo("log4net.config");
                XmlConfigurator.Configure(finfo);
                _Logger = LogManager.GetLogger("Scheduler");

                if (args == null || args.Length == 0)
                {
                    WriteToConsole("Обязательным параметром является имя задания.");
                    return;
                }

                string taskName = args[0];
                Log("Выполнение задания " + taskName);
                try
                {
                    NameValueCollection section = (NameValueCollection)ConfigurationManager.GetSection("Plugins");
                    string typeName = section[taskName];
                    if (string.IsNullOrEmpty(typeName))
                    {
                        Log("Не найден тип обьекта, выполняющего задание " + taskName, true);
                        return;
                    }

                    Log("Сведения о тип " + typeName);
                    Type type = Type.GetType(typeName);
                    if (type == null)
                    {
                        Log("Тип не найден " + typeName);
                        return;
                    }


                    Log("Создание обьекта, выполняющего задание.");
                    ITask task = (ITask)Activator.CreateInstance(type);

                    ILog tasklogger = LogManager.GetLogger(taskName);
                    var taskConfiguration = (NameValueCollection)ConfigurationManager.GetSection(taskName);

                    TaskContext context = new TaskContext(taskName, tasklogger, taskConfiguration);
                    Log("Выполнение задания...");
                    task.Execute(context);
                }
                catch (Exception exc)
                {
                    Log(exc.ToString(), true);
                }
                Log("Окончание выполнения задания");
            }
            catch (Exception exc)
            {
                Log(exc.ToString(), true);
            }
        }

        private static void Log(string s)
        {
            Log(s, false);
        }

        private static void Log(string s, bool hasError)
        {
            WriteToConsole(s);
            if (hasError)
            {
                _Logger.Error(s);
            }
            else
            {
                _Logger.Info(s);
            }
        }

        private static void WriteToConsole(string s)
        {
            Console.WriteLine("{0:G}  {1}", DateTime.Now, s);
        }
    }
}

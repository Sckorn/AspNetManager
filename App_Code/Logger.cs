using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Configuration;
using System.Threading;
//using System.Threading.Tasks;

namespace FootballManager
{
    public static class Logger
    {
        public static string logFile = string.Empty;

        private static bool logExists = false;

        public static bool LogExists
        {
            get
            {
                return logExists;
            }
        }

        private static Object writeLock = new object();

        public static void Init()
        {
            if(logFile.Equals(string.Empty))
                logFile = ConfigurationManager.AppSettings["logFile"];

            try
            {
                if (!File.Exists(logFile))
                {
                    FileInfo fi = new FileInfo(logFile);
                    if (!Directory.Exists(fi.DirectoryName)) Directory.CreateDirectory(fi.DirectoryName);
                    FileStream fs = File.Create(logFile);
                    if (fs != null)
                    {
                        Logger.WriteToLog("Лог успешно создан!");
                        logExists = true;
                    }

                    fs.Close();
                }
                else
                    logExists = true;
            }
            catch (Exception ex)
            {
                throw new Exception("Ошибка создания лога!", ex);
            }
        }

        public static void WriteToLog(string message)
        {
            if (LogExists)
            {
                if (File.Exists(logFile))
                {
                    lock(writeLock)
                    {
                        File.AppendAllText(logFile, String.Format("[{0}] | (T:{1}) : {2}\n", DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss:ffffff"), Thread.CurrentThread.ManagedThreadId.ToString(), message));
                    }
                }
            }
        }
    }
}

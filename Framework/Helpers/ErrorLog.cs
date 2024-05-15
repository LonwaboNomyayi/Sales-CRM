using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Helpers
{
    public static class ErrorLog
    {
        public static void Logger(this Exception ex)
        {
            var path = ConfigurationManager.AppSettings["ErrorLog"];
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            StreamWriter sw = new StreamWriter(path + @"\LogFile " + DateTime.Now.ToString("yyyy.MM.dd HH.mm.ss") + ".txt");
            sw.WriteLine(ex.Message + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            sw.WriteLine(ex.StackTrace + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            sw.WriteLine(ex.Source + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            sw.Close();
        }
    }
}

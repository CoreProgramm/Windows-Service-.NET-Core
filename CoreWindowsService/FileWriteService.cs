using System;
using System.Collections.Generic;
using System.IO;
using System.ServiceProcess;
using System.Text;
using System.Threading;

namespace CoreWindowsService
{
  public  class FileWriteService : ServiceBase
    {
        public Thread Worker = null;
        public FileWriteService()
        {
            ServiceName = "MyCoreService";
        }
        protected override void OnStart(string[] args)
        {
           
            ThreadStart start = new ThreadStart(Working);
            Worker = new Thread(start);
            Worker.Start();
           
        }
        private void Working()
        {
            int nSleep = 1; // 1 minute
            try
            {
                while (true)
                {
                    string filename = @"c:\MyCoreService.txt";
                    using (StreamWriter writer = new StreamWriter(filename, true))
                    {
                        writer.WriteLine(string.Format(".NET Core Windows Service Called on " + DateTime.Now.ToString("dd /MM/yyyy hh:mm:ss tt")));
                        writer.Close();
                    }
                    Thread.Sleep(nSleep * 60 * 1000);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void onDebug()
        {
            OnStart(null);
        }
        protected override void OnStop()
        {
            if ((Worker != null) & Worker.IsAlive)
            {
                string filename = @"c:\MyCoreService.txt";
                using (StreamWriter writer = new StreamWriter(filename, true))
                {
                    writer.WriteLine(string.Format(".NET Core Windows Service Stopped on " + DateTime.Now.ToString("dd /MM/yyyy hh:mm:ss tt")));
                    writer.Close();
                }
                Worker.Abort();
            }
        }

      
    }
}

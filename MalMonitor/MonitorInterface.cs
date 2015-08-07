using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MalMonitor
{
    public class MonitorInterface : MarshalByRefObject
    {
        public void Output(DateTime InTime, Int32 InClientPID, String[] InStr)
        {
            Console.WriteLine("MalMon output----------");
            foreach (string str in InStr)
            {
                Console.WriteLine(str);
            }
        }

        public void IsInstalled(Int32 InClientPID)
        {
            Console.WriteLine("MalMon has been installed in target {0}.\r\n", InClientPID);
        }

        public void ReportException(Exception InInfo)
        {
            Console.WriteLine("The target process has reported an error:\r\n" + InInfo.ToString());
        }

        public void Ping()
        {
        }
    }
}

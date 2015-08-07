using System;
using System.Collections.Generic;
using System.Runtime.Remoting;
using System.Text;
using System.IO;
using EasyHook;

namespace MalMonitor
{

    class Program
    {
        static String ChannelName = null;

        static void Main(string[] args)
        {
            Int32 TargetPID = 0;

            if ((args.Length != 1) || !Int32.TryParse(args[0], out TargetPID))
            {
                Console.WriteLine();
                Console.WriteLine("Usage: MalMonitor.exe %PID%");
                Console.WriteLine();

                return;
            }

            try
            {
                try
                {
                    //absolute path
                    /*
                    Config.Register(
                        "A MalMonitor like demo application.",
                        "D:\\Source\\MalMonInject\\bin\\Release\\MalMonitor.exe",
                        "D:\\Source\\MalMonInject\\bin\\Release\\MalMonInject.dll");
                    */

                    //relative path
                    Config.Register(
                        "A MalMonitor like demo app",
                        "MalMonitor.exe",
                        "MalMonInject.dll");
                }
                catch (ApplicationException)
                {
                    Console.WriteLine("file not found!....");
                    System.Diagnostics.Process.GetCurrentProcess().Kill();
                }

                RemoteHooking.IpcCreateServer<MonitorInterface>(ref ChannelName, WellKnownObjectMode.SingleCall);

                RemoteHooking.Inject(
                    TargetPID,
                    "MalMonInject.dll",
                    "MalMonInject.dll",
                    ChannelName);

                Console.ReadLine();
            }
            catch (Exception ExtInfo)
            {
                Console.WriteLine("There was an error while connecting to target:\r\n{0}", ExtInfo.ToString());
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Runtime.Remoting;
using System.Text;
using System.IO;
using EasyHook;

namespace MalMonitor
{
    public class MalMonInterface : MarshalByRefObject
    {
        public void IsInstalled(Int32 InClientPID)
        {
            Console.WriteLine("MalMon has been installed in target {0}.\r\n", InClientPID);
        }

        public void OnCreateFile(Int32 InClientPID, String[] InFileNames)
        {
            for (int i = 0; i < InFileNames.Length; i++)
            {
                Console.WriteLine(InFileNames[i]);
            }
        }

        public void ReportException(Exception InInfo)
        {
            Console.WriteLine("The target process has reported an error:\r\n" + InInfo.ToString());
        }

        public void Ping()
        {
        }
    }

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

                RemoteHooking.IpcCreateServer<MalMonInterface>(ref ChannelName, WellKnownObjectMode.SingleCall);

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
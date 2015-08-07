using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace Test
{
    class Program
    {
        public static String FormatMessage(DateTime time, String api, String msg)
        {
            //[TIME][PID:TID]:"API":"MSG"
            String str = "[" + time.ToString() + "][" + Process.GetCurrentProcess().Id + ":" +
                        Process.GetCurrentProcess().Id + "]: \"" + api + "\":\"" +
                        msg + "\"";
            return str;
        }
        public static void Main(){
            Console.WriteLine(Program.FormatMessage(DateTime.Now, "CreateFile", "msg"));
            Console.ReadLine();
        }
    }
}

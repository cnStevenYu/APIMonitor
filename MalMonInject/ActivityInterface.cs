using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EasyHook;

namespace MalMonInject
{
    public class ActivityMonitor
    {
        protected MalMonInject Injector;

        public ActivityMonitor(MalMonInject inject)
        {
            this.Injector = inject;
        }

        virtual public void InstallHook() { }
        virtual public void UninstallHook() { }
        public static String FormatMessage(DateTime time, String api, String msg)
        {
            //[TIME][PID:TID]:"API":"MSG"
            String str = "[" + time.ToString() + "][" + RemoteHooking.GetCurrentProcessId() + ":" +
                        RemoteHooking.GetCurrentThreadId() + "]: \"" + api + "\":\"" +
                        msg + "\"";
            return str;
        }
    }
}

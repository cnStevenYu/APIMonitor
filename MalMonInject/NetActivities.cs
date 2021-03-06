﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EasyHook;
using System.Runtime.InteropServices;

namespace MalMonInject
{
    public class NetActivities:ActivityMonitor
    {
        LocalHook sendHook;
        LocalHook sendtoHook;
        LocalHook recvHook;
        LocalHook recvfromHook;

        private Drecv recvFunc;
        private Drecvfrom recvfromFunc;

        public NetActivities(MalMonInject Injector):base(Injector)
        {
        }

        public override void InstallHook()
        {
            sendHook = LocalHook.Create(
                    LocalHook.GetProcAddress("Ws2_32.dll", "send"),
                    new Dsend(send_Hooked),
                    this.Injector);
            sendHook.ThreadACL.SetExclusiveACL(new Int32[] { 0 });
            
            sendtoHook = LocalHook.Create(
                    LocalHook.GetProcAddress("Ws2_32.dll", "sendto"),
                    new Dsendto(sendto_Hooked),
                    this.Injector);
            sendtoHook.ThreadACL.SetExclusiveACL(new Int32[] { 0 });
            
            recvHook = LocalHook.Create(
                    LocalHook.GetProcAddress("Ws2_32.dll", "recv"),
                    new Drecv(recv_Hooked),
                    this.Injector);
            recvHook.ThreadACL.SetExclusiveACL(new Int32[] { 0 });
            recvFunc = LocalHook.GetProcDelegate<Drecv>("Ws2_32.dll", "recv");

            recvfromHook = LocalHook.Create(
                   LocalHook.GetProcAddress("Ws2_32.dll", "recvfrom"),
                   new Drecvfrom(recvfrom_Hooked),
                   this.Injector);
            recvfromHook.ThreadACL.SetExclusiveACL(new Int32[] { 0 });
            recvfromFunc = LocalHook.GetProcDelegate<Drecvfrom>("Ws2_32.dll", "recvfrom");
        }

        [System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential, CharSet = System.Runtime.InteropServices.CharSet.Ansi)]
        public struct sockaddr
        {

            /// u_short->unsigned short
            public ushort sa_family;

            /// char[14]
            [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValTStr, SizeConst = 14)]
            public string sa_data;
        }
        /*----code generated by script CodeGenerator.py----*/
        /*----send----*/
        [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Unicode, SetLastError = true)]
        delegate int Dsend( uint s, [MarshalAsAttribute(UnmanagedType.LPStr)] string buf, int len, int flags);
        [DllImportAttribute("ws2_32.dll", EntryPoint = "send", CallingConvention = CallingConvention.StdCall)]
        static extern int send(uint s, [InAttribute()] [MarshalAsAttribute(UnmanagedType.LPStr)] string buf, int len, int flags);

        static int send_Hooked(uint s, [MarshalAsAttribute(UnmanagedType.LPStr)] string buf, int len, int flags)
        {
            try
            {
                MalMonInject This = (MalMonInject)HookRuntimeInfo.Callback;

                lock (This.Queue)
                {
                    //Time + Pid + Tid + Api + Content
                    This.Queue.Push(ActivityMonitor.FormatMessage(DateTime.Now, "send", buf));
                }
            }
            catch
            {
            }

            return send(s, buf, len, flags);
        }
        
        /*----code generated by script CodeGenerator.py----*/
        /*----sendto----*/
        [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Unicode, SetLastError = true)]
        delegate int Dsendto( uint s, [MarshalAsAttribute(UnmanagedType.LPStr)] string buf, int len, int flags, ref sockaddr to, int tolen);
        [DllImportAttribute("ws2_32.dll", EntryPoint = "sendto", CallingConvention = CallingConvention.StdCall)]
        static extern int sendto( uint s, [InAttribute()] [MarshalAsAttribute(UnmanagedType.LPStr)] string buf, int len, int flags, ref sockaddr to, int tolen);

        static int sendto_Hooked( uint s, [MarshalAsAttribute(UnmanagedType.LPStr)] string buf, int len, int flags, ref sockaddr to, int tolen)
        {
            try
            {
                MalMonInject This = (MalMonInject)HookRuntimeInfo.Callback;

                lock (This.Queue)
                {
                    //Time + Pid + Tid + Api + Content
                    This.Queue.Push(ActivityMonitor.FormatMessage(DateTime.Now, "send", buf));
                }
            }
            catch
            {
            }

            return send(s, buf, len, flags);
        }
        /*----code generated by script CodeGenerator.py----*/
        /*----recv----*/
        [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Unicode, SetLastError = true)]
        delegate int Drecv( uint s, IntPtr buf, int len, int flags);
        [DllImportAttribute("ws2_32.dll", EntryPoint = "recv", CallingConvention = CallingConvention.StdCall)]
        static extern int recv( uint s, IntPtr buf, int len, int flags);

        static int recv_Hooked( uint s, IntPtr buf, int len, int flags)
        {
            int res = 0;
            try
            {
                MalMonInject This = (MalMonInject)HookRuntimeInfo.Callback;
                res = This.NetApis.recvFunc(s, buf, len, flags);

                lock (This.Queue)
                {
                    //Time + Pid + Tid + Api + Content
                    This.Queue.Push(ActivityMonitor.FormatMessage(DateTime.Now, "recv:", Marshal.PtrToStringUni(buf)));
                }
            }
            catch
            {
            }

            return res;
        }
        /*----code generated by script CodeGenerator.py----*/
        /*----recvfrom----*/
        [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Unicode, SetLastError = true)]
        delegate int Drecvfrom(uint s, IntPtr buf, int len, int flags, ref sockaddr from, ref int fromlen);
        [DllImportAttribute("ws2_32.dll", EntryPoint = "recvfrom", CallingConvention = CallingConvention.StdCall)]
        static extern int recvfrom(uint s, IntPtr buf, int len, int flags, ref sockaddr from, ref int fromlen);

        static int recvfrom_Hooked(uint s, IntPtr buf, int len, int flags, ref sockaddr from, ref int fromlen)
        {
            int res = 0;
            try
            {
                MalMonInject This = (MalMonInject)HookRuntimeInfo.Callback;
                res = This.NetApis.recvfromFunc(s, buf, len, flags, ref from, ref fromlen);

                lock (This.Queue)
                {
                    //Time + Pid + Tid + Api + Content
                    This.Queue.Push(ActivityMonitor.FormatMessage(DateTime.Now, "recvfrom", Marshal.PtrToStringUni(buf)));
                }
            }
            catch
            {
            }

            return res;

        }
    }
}

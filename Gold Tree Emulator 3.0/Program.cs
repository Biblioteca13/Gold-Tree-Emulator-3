using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using GoldTree.Core;
namespace GoldTree
{
    internal class Program
    {
        private delegate bool EventHandler(CtrlType sig);
        private enum CtrlType
        {
            CTRL_BREAK_EVENT = 1,
            CTRL_C_EVENT = 0,
            CTRL_CLOSE_EVENT = 2,
            CTRL_LOGOFF_EVENT = 5,
            CTRL_SHUTDOWN_EVENT = 6
        }
        private static bool bool_0 = false;
        private static EventHandler delegate0_0;
        [DllImport("Kernel32")]
        private static extern bool SetConsoleCtrlHandler(Program.EventHandler handler, bool add);
        [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.ControlAppDomain)]
        public static void Main(string[] args)
        {
            AppDomain currentDomain = AppDomain.CurrentDomain;
            currentDomain.UnhandledException += new UnhandledExceptionEventHandler(Program.smethod_0);
            Program.delegate0_0 = (Program.EventHandler)Delegate.Combine(Program.delegate0_0, new Program.EventHandler(Program.smethod_1));
            Program.SetConsoleCtrlHandler(Program.delegate0_0, true);
            try
            {
                GoldTree @class = new GoldTree();
                @class.Initialize();
                Program.bool_0 = true;
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
            }
            while (true)
            {
                Console.ReadKey();
            }
        }
        private static void smethod_0(object sender, UnhandledExceptionEventArgs e)
        {
            Logging.smethod_7();
            Exception ex = (Exception)e.ExceptionObject;
            Logging.LogCriticalException(ex.ToString());
        }
        private static bool smethod_1(CtrlType enum0_0)
        {
            if (Program.bool_0)
            {
                Logging.smethod_7();
                Console.Clear();
                Console.WriteLine("The server is saving users furniture, rooms, etc. WAIT FOR THE SERVER TO CLOSE, DO NOT EXIT THE PROCESS IN TASK MANAGER!!");
                GoldTree.Destroy("", true);
            }
            return true;
        }
    }
}
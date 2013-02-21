using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MonitoringCollectDataConsole.Lib;
using SharedLibrary;
using SharedLibraryMonitoringDevice;
using NLog;

namespace MonitoringCollectDataConsole
{
    class Program
    {
        private static PhoenixContact.HFI.Controller_IBS_ETH controller18 = null;
        private static PhoenixContact.HFI.Controller_IBS_ETH controller19 = null;
        private static HFI_Appl18 appl18 = null;
        private static HFI_Appl19 appl19 = null;
        private static SaveDeviceStatus saveDeviceStatus18 = null;
        private static SaveDeviceStatus saveDeviceStatus19 = null;
        private static List<MonitoringAIOServer> MonitoringCollection18 = null;
        private static List<MonitoringAIOServer> MonitoringCollection19 = null;
        private static Logger logger = null;
        private static bool FirstChanceExceptionHandled = false;

        static void Main(string[] args)
        {

            AppDomain.CurrentDomain.FirstChanceException += new EventHandler<System.Runtime.ExceptionServices.FirstChanceExceptionEventArgs>(CurrentDomain_FirstChanceException);

            logger = LogManager.GetCurrentClassLogger();

            //#if RELEASE
            InitializeController18();
            InitializeController19();

            controller18.Enable();
            controller19.Enable();

            controller18.OnException += new PhoenixContact.PxC_Library.Util.ExceptionHandler(controller18_OnException);
            controller19.OnException += new PhoenixContact.PxC_Library.Util.ExceptionHandler(controller19_OnException);
            //#endif


            InitializeMonitoringCollection18();
            InitializeMonitoringCollection19();

            AddMonitoringCollection18();
            AddMonitoringCollection19();

            StartMonitoringCollection18();
            StartMonitoringCollection19();

            string ip18 = "192.168.17.18";
            string ip19 = "192.168.17.19";

            //#if DEBUG
            //            ip18 = "localhost/monitoring/17.18";
            //            ip19 = "localhost/monitoring/17.19";
            //#endif

            saveDeviceStatus18 = new SaveDeviceStatus(appl18, ip18);
            saveDeviceStatus19 = new SaveDeviceStatus(appl19, ip19);

            saveDeviceStatus18.Start();
            saveDeviceStatus19.Start();

            Console.WriteLine("Service Started Successfully.");
            Console.ReadKey();
        }

        static void CurrentDomain_FirstChanceException(object sender, System.Runtime.ExceptionServices.FirstChanceExceptionEventArgs e)
        {
            //if (FirstChanceExceptionHandled)
            //{
            //    return;
            //}

            //FirstChanceExceptionHandled = true;
            //StopMonitoringCollection18();
            //StopMonitoringCollection19();
            //System.Threading.Thread.Sleep(1000 * 60);


            //controller18.Dispose();
            //controller19.Dispose();
            //ClearMonitoringCollection18();
            //ClearMonitoringCollection19();
            //System.Threading.Thread.Sleep(1000);

            //InitializeController18();
            //InitializeController19();

            //controller18.Enable();
            //controller19.Enable();

            //controller18.OnException += new PhoenixContact.PxC_Library.Util.ExceptionHandler(controller18_OnException);
            //controller19.OnException += new PhoenixContact.PxC_Library.Util.ExceptionHandler(controller19_OnException);


            //InitializeMonitoringCollection18();
            //InitializeMonitoringCollection19();

            //AddMonitoringCollection18();
            //AddMonitoringCollection19();

            //StartMonitoringCollection18();
            //StartMonitoringCollection19();

            //FirstChanceExceptionHandled = false;
        }

        private static void StartMonitoringCollection18()
        {
            foreach (var monitoringAioServer in MonitoringCollection18)
            {
                monitoringAioServer.Start();
            }
        }

        private static void StopMonitoringCollection18()
        {
            foreach (var monitoringAioServer in MonitoringCollection18)
            {
                monitoringAioServer.Stop();
            }
        }

        private static void StartMonitoringCollection19()
        {
            foreach (var monitoringAioServer in MonitoringCollection19)
            {
                monitoringAioServer.Start();
            }
        }

        private static void StopMonitoringCollection19()
        {
            foreach (var monitoringAioServer in MonitoringCollection19)
            {
                monitoringAioServer.Stop();
            }
        }

        private static void AddMonitoringCollection19()
        {
            //MonitoringCollection19.Add(new MonitoringAIOServer(28, appl19));
            MonitoringCollection19.Add(new MonitoringAIOServer(29, appl19));
            MonitoringCollection19.Add(new MonitoringAIOServer(30, appl19));
            MonitoringCollection19.Add(new MonitoringAIOServer(31, appl19));
            MonitoringCollection19.Add(new MonitoringAIOServer(32, appl19));
            MonitoringCollection19.Add(new MonitoringAIOServer(33, appl19));
            MonitoringCollection19.Add(new MonitoringAIOServer(34, appl19));
            MonitoringCollection19.Add(new MonitoringAIOServer(35, appl19));
            MonitoringCollection19.Add(new MonitoringAIOServer(36, appl19));
            MonitoringCollection19.Add(new MonitoringAIOServer(37, appl19));
            MonitoringCollection19.Add(new MonitoringAIOServer(38, appl19));
            MonitoringCollection19.Add(new MonitoringAIOServer(39, appl19));
            MonitoringCollection19.Add(new MonitoringAIOServer(40, appl19));
            MonitoringCollection19.Add(new MonitoringAIOServer(41, appl19));
            MonitoringCollection19.Add(new MonitoringAIOServer(42, appl19));
            MonitoringCollection19.Add(new MonitoringAIOServer(43, appl19));
            MonitoringCollection19.Add(new MonitoringAIOServer(44, appl19));
            MonitoringCollection19.Add(new MonitoringAIOServer(45, appl19));
            MonitoringCollection19.Add(new MonitoringAIOServer(46, appl19));
            MonitoringCollection19.Add(new MonitoringAIOServer(47, appl19));
            MonitoringCollection19.Add(new MonitoringAIOServer(48, appl19));
            MonitoringCollection19.Add(new MonitoringAIOServer(49, appl19));
            MonitoringCollection19.Add(new MonitoringAIOServer(50, appl19));
            MonitoringCollection19.Add(new MonitoringAIOServer(51, appl19));
            MonitoringCollection19.Add(new MonitoringAIOServer(52, appl19));
            MonitoringCollection19.Add(new MonitoringAIOServer(53, appl19));
            MonitoringCollection19.Add(new MonitoringAIOServer(54, appl19));
            MonitoringCollection19.Add(new MonitoringAIOServer(55, appl19));
            MonitoringCollection19.Add(new MonitoringAIOServer(56, appl19));
            MonitoringCollection19.Add(new MonitoringAIOServer(57, appl19));
            MonitoringCollection19.Add(new MonitoringAIOServer(58, appl19));
            MonitoringCollection19.Add(new MonitoringAIOServer(59, appl19));
            MonitoringCollection19.Add(new MonitoringAIOServer(60, appl19));
            MonitoringCollection19.Add(new MonitoringAIOServer(61, appl19));
            MonitoringCollection19.Add(new MonitoringAIOServer(62, appl19));
            MonitoringCollection19.Add(new MonitoringAIOServer(63, appl19));
            MonitoringCollection19.Add(new MonitoringAIOServer(64, appl19));
        }

        private static void InitializeMonitoringCollection18()
        {
            MonitoringCollection18 = new List<MonitoringAIOServer>();
        }

        private static void InitializeMonitoringCollection19()
        {
            MonitoringCollection19 = new List<MonitoringAIOServer>();
        }

        private static void ClearMonitoringCollection18()
        {
            MonitoringCollection18.Clear();
        }

        private static void ClearMonitoringCollection19()
        {
            MonitoringCollection19.Clear();
        }

        private static void AddMonitoringCollection18()
        {
            MonitoringCollection18.Add(new MonitoringAIOServer(1, appl18));
            MonitoringCollection18.Add(new MonitoringAIOServer(2, appl18));
            MonitoringCollection18.Add(new MonitoringAIOServer(3, appl18));
            MonitoringCollection18.Add(new MonitoringAIOServer(4, appl18));
            MonitoringCollection18.Add(new MonitoringAIOServer(5, appl18));
            MonitoringCollection18.Add(new MonitoringAIOServer(6, appl18));
            MonitoringCollection18.Add(new MonitoringAIOServer(7, appl18));
            MonitoringCollection18.Add(new MonitoringAIOServer(8, appl18));
            MonitoringCollection18.Add(new MonitoringAIOServer(9, appl18));
            MonitoringCollection18.Add(new MonitoringAIOServer(10, appl18));
            MonitoringCollection18.Add(new MonitoringAIOServer(11, appl18));
            MonitoringCollection18.Add(new MonitoringAIOServer(12, appl18));
            MonitoringCollection18.Add(new MonitoringAIOServer(13, appl18));
            MonitoringCollection18.Add(new MonitoringAIOServer(14, appl18));
            MonitoringCollection18.Add(new MonitoringAIOServer(15, appl18));
            MonitoringCollection18.Add(new MonitoringAIOServer(16, appl18));
            MonitoringCollection18.Add(new MonitoringAIOServer(17, appl18));
            MonitoringCollection18.Add(new MonitoringAIOServer(18, appl18));
            MonitoringCollection18.Add(new MonitoringAIOServer(19, appl18));
            MonitoringCollection18.Add(new MonitoringAIOServer(20, appl18));
            MonitoringCollection18.Add(new MonitoringAIOServer(21, appl18));
            MonitoringCollection18.Add(new MonitoringAIOServer(22, appl18));
            MonitoringCollection18.Add(new MonitoringAIOServer(23, appl18));
            MonitoringCollection18.Add(new MonitoringAIOServer(24, appl18));
            MonitoringCollection18.Add(new MonitoringAIOServer(25, appl18));
            MonitoringCollection18.Add(new MonitoringAIOServer(26, appl18));
            //MonitoringCollection18.Add(new MonitoringAIOServer(27, appl18));
        }

        static void controller18_OnException(Exception ExceptionData)
        {
            //saveDeviceStatus18.CheckDeviceStatus();
            //StopMonitoringCollection18();
            //System.Threading.Thread.Sleep(1000 * 60);
            //controller18.Dispose();
            //ClearMonitoringCollection18();
            //System.Threading.Thread.Sleep(1000);
            //InitializeController18();
            //controller18.Enable();
            //controller18.OnException += new PhoenixContact.PxC_Library.Util.ExceptionHandler(controller18_OnException);
            //InitializeMonitoringCollection18();
            //AddMonitoringCollection18();
            //StartMonitoringCollection18();
            //saveDeviceStatus18.CheckDeviceStatus();
            //try
            //{

            //}
            //catch (Exception ex)
            //{
            //    logger.LogException(LogLevel.Info, "Program.controller18_OnException", ex);
            //}
        }

        private static void InitializeController18()
        {
            appl18 = new HFI_Appl18();
            controller18 = appl18.Controller;
        }

        private static void InitializeController19()
        {
            appl19 = new HFI_Appl19();
            controller19 = appl19.Controller;
        }

        static void controller19_OnException(Exception ExceptionData)
        {
            //saveDeviceStatus19.CheckDeviceStatus();
            //StopMonitoringCollection19();
            //System.Threading.Thread.Sleep(1000 * 60);
            //controller19.Dispose();
            //ClearMonitoringCollection19();
            //System.Threading.Thread.Sleep(1000);
            //InitializeController19();
            //controller19.Enable();
            //controller19.OnException += new PhoenixContact.PxC_Library.Util.ExceptionHandler(controller19_OnException);
            //InitializeMonitoringCollection19();
            //AddMonitoringCollection19();
            //StartMonitoringCollection19();
            //saveDeviceStatus19.CheckDeviceStatus();
            //try
            //{

            //}
            //catch (Exception ex)
            //{
            //    logger.LogException(LogLevel.Info, "Program.controller19_OnException", ex);
            //}
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharedLibraryMonitoringDevice;

namespace ConsoleApplyDeviceSettings
{
    class Program
    {
        private static PhoenixContact.HFI.Controller_IBS_ETH controller18=null;
        private static PhoenixContact.HFI.Controller_IBS_ETH controller19 = null;

        static void Main(string[] args)
        {
            AppDomain.CurrentDomain.ProcessExit += new EventHandler(CurrentDomain_ProcessExit);

            HFI_Appl18 appl18 = new HFI_Appl18();

            controller18 = appl18.Controller;
            
        }

        static void CurrentDomain_ProcessExit(object sender, EventArgs e)
        {
            controller18.Dispose();
            controller19.Dispose();
        }
    }
}

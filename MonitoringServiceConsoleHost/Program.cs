using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.Text;

namespace MonitoringServiceConsoleHost
{
    class Program
    {
        static void Main(string[] args)
        {

            ServiceHost host = new ServiceHost(typeof(MonitoringServiceLibrary.MonitoringService));
            NetTcpSecurity security = new NetTcpSecurity() { Mode = SecurityMode.None };
            NetTcpBinding binding = new NetTcpBinding() { Security = security };

#if DEBUG
            host.AddServiceEndpoint(typeof(MonitoringServiceLibrary.IMonitoringService), binding,
                    "net.tcp://localhost:9000/MonitoringService");
#endif

#if RELEASE
                host.AddServiceEndpoint(typeof(MonitoringServiceLibrary.IMonitoringService), binding,
                        Properties.Settings.Default.MonitoringServiceUrl);
#endif


            //ServiceMetadataBehavior behavior = new ServiceMetadataBehavior();
            //host.Description.Behaviors.Add(behavior);
            //Binding mexBinding = MetadataExchangeBindings.CreateMexTcpBinding();
            //string mexAddress = "net.tcp://localhost:9000/mex";
            //host.AddServiceEndpoint(typeof(IMetadataExchange), mexBinding, mexAddress);


            host.Open();

            Console.WriteLine("Service Started Successfully.");
            Console.ReadKey();
        }
    }
}

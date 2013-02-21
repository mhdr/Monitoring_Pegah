using System.ServiceModel;
using MonitoringServiceLibrary;

namespace Monitoring.Lib
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public static class Statics
    {
        private static IMonitoringService _channel;
        private static double _version = 1.0;

        public static IMonitoringService Channel
        {
            get
            {
                if (_channel == null)
                {

                    NetTcpSecurity security = new NetTcpSecurity();
                    security.Mode = SecurityMode.None;



#if DEBUG
                    _channel = ChannelFactory<IMonitoringService>.CreateChannel(new NetTcpBinding() { MaxReceivedMessageSize = Int32.MaxValue, MaxBufferSize = Int32.MaxValue, Security = security },
                               new EndpointAddress("net.tcp://localhost:9000/MonitoringService"));
#endif

#if RELEASE
                    _channel = ChannelFactory<IMonitoringService>.CreateChannel(new NetTcpBinding() { MaxReceivedMessageSize = Int32.MaxValue, MaxBufferSize = Int32.MaxValue, Security = security },
new EndpointAddress(Properties.Settings.Default.MonitoringServiceUrl));
#endif
                }

                return _channel;
            }
            set { _channel = value; }
        }

        public static double Version
        {
            get { return _version; }
            set { _version = value; }
        }
    }
}

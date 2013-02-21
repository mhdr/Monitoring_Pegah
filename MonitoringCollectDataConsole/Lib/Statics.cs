using System.ServiceModel;
using MonitoringServiceLibrary;

namespace MonitoringCollectDataConsole.Lib
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public static class Statics
    {
        public static IMonitoringService Channel
        {
            get
            {
                if (_channel == null)
                {
                    NetTcpSecurity security = new NetTcpSecurity() { Mode = SecurityMode.None };
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

        private static IMonitoringService _channel;

    }
}

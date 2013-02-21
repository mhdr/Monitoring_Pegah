using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using MonitoringServiceLibrary;

namespace Monitoring.Lib
{
    public class CheckUpdateAsync
    {
        private Thread thread = null;
        public event CheckUpdateCompletedEventHandler CheckUpdateCompleted;
        private IMonitoringService channel = null;

        public CheckUpdateAsync()
        {
            InitializeChannel();
        }

        private void InitializeChannel()
        {
            channel = Lib.Statics.Channel;
        }

        private void OnCheckUpdateCompleted(CheckUpdateCompletedEventArgs e)
        {
            CheckUpdateCompletedEventHandler handler = CheckUpdateCompleted;
            if (handler != null) handler(this, e);
        }

        public void StartAsync()
        {
            thread = new Thread(Start);
        }

        private void Start()
        {
            bool result= channel.IsUpdateAvailabe(Lib.Statics.Version);
            OnCheckUpdateCompleted(new CheckUpdateCompletedEventArgs(result));
        }
    }
}

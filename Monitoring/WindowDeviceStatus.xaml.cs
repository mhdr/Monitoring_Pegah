using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Monitoring.Lib;
using MonitoringServiceLibrary;
using MonitoringServiceLibrary.Lib;
using NLog;

namespace Monitoring
{
    /// <summary>
    /// Interaction logic for WindowDeviceStatus.xaml
    /// </summary>
    public partial class WindowDeviceStatus : Window
    {
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        private IMonitoringService channel = null;
        private List<DeviceStatusHistoryForGrid> dataSource;
        private Logger logger = null;

        public WindowDeviceStatus()
        {
            InitializeComponent();
            logger = LogManager.GetCurrentClassLogger();
        }

        private void RibbonButtonChangeDate_Click(object sender, RoutedEventArgs e)
        {
            WindowSetDateForChart windowSetDateForChart = new WindowSetDateForChart();
            windowSetDateForChart.StartTime = this.StartTime;
            windowSetDateForChart.EndTime = this.EndTime;
            windowSetDateForChart.ChartDateChanged += new ChartDateChangedEventHandler(windowSetDateForChart_ChartDateChanged);
            windowSetDateForChart.Show();
        }

        void windowSetDateForChart_ChartDateChanged(object sender, ChartDateChangedEventArgs e)
        {
            this.StartTime = e.StartTime;
            this.EndTime = e.EndTime;
            BindGridAsync();
        }

        private void InitializeChannel()
        {
            this.channel = Lib.Statics.Channel;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            InitializeChannel();
            BindGridAsync();
        }

        private void BindGridAsync()
        {
            busyIndicator.IsBusy = true;
            Thread td = new Thread(BindGrid);
            td.Priority = ThreadPriority.AboveNormal;
            td.Start();
        }

        private void BindGrid()
        {
            try
            {
                dataSource = channel.GetDeviceStatusHistories(this.StartTime, this.EndTime);
            }
            catch (System.Exception ex)
            {
                logger.LogException(LogLevel.Info, string.Format("WindowDeviceStatus.BindGrid"), ex);
            }
            Dispatcher.BeginInvoke(new Action(BindGridUI));
        }

        void BindGridUI()
        {
            gridView.ItemsSource = dataSource;
            busyIndicator.IsBusy = false;
        }
    }
}

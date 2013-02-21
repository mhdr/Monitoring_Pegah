using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Monitoring.Lib;
using MonitoringServiceLibrary;
using MonitoringServiceLibrary.Lib;
using NationalInstruments.Controls;
using Telerik.Windows.Controls;
using NLog;

namespace Monitoring
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MonitoringAIO LastSelected = null;
        private bool _runningStatus;
        public event EventHandler RunningStatusChanged;
        private Timer timer18 = null;
        private Timer timer19 = null;
        private IMonitoringService channel = null;
        private Logger logger = null;

        public void OnRunningStatusChanged(EventArgs e)
        {
            EventHandler handler = RunningStatusChanged;
            if (handler != null) handler(this, e);
        }

        public MainWindow()
        {
            StyleManager.ApplicationTheme = new Office_BlueTheme();
            InitializeComponent();
        }

        public bool RunningStatus
        {
            get { return _runningStatus; }
            set
            {
                _runningStatus = value;
                OnRunningStatusChanged(new EventArgs());
            }
        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            logger = LogManager.GetCurrentClassLogger();

            InitializeChannel();
            var controls1 = WrapPanel1.ChildrenOfType<MonitoringAIO>();
            var controls2 = WrapPanel2.ChildrenOfType<MonitoringAIO>();
            var controls3 = WrapPanel3.ChildrenOfType<MonitoringAIO>();
            var controls4 = WrapPanel4.ChildrenOfType<MonitoringAIO>();
            var controls5 = WrapPanel5.ChildrenOfType<MonitoringAIO>();
            var controls6 = WrapPanel6.ChildrenOfType<MonitoringAIO>();
            var controls7 = WrapPanel7.ChildrenOfType<MonitoringAIO>();
            var controls8 = WrapPanel8.ChildrenOfType<MonitoringAIO>();
            var controls9 = WrapPanel9.ChildrenOfType<MonitoringAIO>();
            

            foreach (var monitoringAio in controls1)
            {
                monitoringAio.SelectionChanged += monitoringAio_SelectionChanged;
            }

            foreach (var monitoringAio in controls1)
            {
                monitoringAio.SelectionChanged += monitoringAio_SelectionChanged;
            }

            foreach (var monitoringAio in controls2)
            {
                monitoringAio.SelectionChanged += monitoringAio_SelectionChanged;
            }

            foreach (var monitoringAio in controls3)
            {
                monitoringAio.SelectionChanged += monitoringAio_SelectionChanged;
            }

            foreach (var monitoringAio in controls4)
            {
                monitoringAio.SelectionChanged += monitoringAio_SelectionChanged;
            }

            foreach (var monitoringAio in controls5)
            {
                monitoringAio.SelectionChanged += monitoringAio_SelectionChanged;
            }

            foreach (var monitoringAio in controls6)
            {
                monitoringAio.SelectionChanged += monitoringAio_SelectionChanged;
            }

            foreach (var monitoringAio in controls7)
            {
                monitoringAio.SelectionChanged += monitoringAio_SelectionChanged;
            }

            foreach (var monitoringAio in controls8)
            {
                monitoringAio.SelectionChanged += monitoringAio_SelectionChanged;
            }

            foreach (var monitoringAio in controls9)
            {
                monitoringAio.SelectionChanged += monitoringAio_SelectionChanged;
            }

            this.RunningStatusChanged += new EventHandler(MainWindow_RunningStatusChanged);


#if DEBUG
            foreach (var monitoringAio in controls)
            {
                if (monitoringAio.IPAddress == "192.168.17.18")
                {
                    monitoringAio.IPAddress = "localhost/monitoring/17.18";
                }
                else if (monitoringAio.IPAddress == "192.168.17.19")
                {
                    monitoringAio.IPAddress = "localhost/monitoring/17.19";
                }
            }
#endif

            //StartMonitoring();

            CheckUpdateAsync checkUpdateAsync = new CheckUpdateAsync();
            checkUpdateAsync.CheckUpdateCompleted += new CheckUpdateCompletedEventHandler(checkUpdateAsync_CheckUpdateCompleted);
            //checkUpdateAsync.StartAsync();
        }

        void checkUpdateAsync_CheckUpdateCompleted(object sender, CheckUpdateCompletedEventArgs e)
        {
            if (e.Result == true)
            {
                //TODO Get Update
            }
        }

        private void InitializeChannel()
        {
            channel = Lib.Statics.Channel;
        }

        private void GetStatus18(object state)
        {
            string ip = "192.168.17.18";

#if DEBUG
            ip = "localhost/monitoring/17.18";
#endif

            DeviceStatusCache result = null;

            try
            {
                result = channel.GetDeviceStatusHistoryLastValue(ip);
            }
            catch (System.Exception ex)
            {
                logger.LogException(LogLevel.Info, "MainWindow.GetStatus18", ex);
                return;
            }

            if (result == null)
            {
                return;
            }

            Dispatcher.BeginInvoke(new Action(() => GetStatusUI(result, ref Led18)));
        }

        private void GetStatusUI(DeviceStatusCache result, ref LED led)
        {
            if (result.Color == StatusColor.Green)
            {
                led.TrueBrush = Brushes.LightGreen;
            }
            else if (result.Color == StatusColor.Yellow)
            {
                led.TrueBrush = Brushes.Yellow;
            }
            else if (result.Color == StatusColor.Red)
            {
                led.TrueBrush = Brushes.Red;
            }

            ToolTipDeviceStatus toolTipDeviceStatus = new ToolTipDeviceStatus();
            toolTipDeviceStatus.IPAddress = result.IPAddress;
            toolTipDeviceStatus.Status = result.Status;
            toolTipDeviceStatus.Description = result.Description;

            led.ToolTip = toolTipDeviceStatus;

            led.Value = true;
        }

        private void GetStatus19(object state)
        {
            string ip = "192.168.17.19";

#if DEBUG
            ip = "localhost/monitoring/17.19";
#endif

            DeviceStatusCache result = null;

            try
            {
                result = channel.GetDeviceStatusHistoryLastValue(ip);
            }
            catch (System.Exception ex)
            {
                logger.LogException(LogLevel.Info, "MainWindow.GetStatus19", ex);
                return;
            }

            if (result == null)
            {
                return;
            }

            Dispatcher.BeginInvoke(new Action(() => GetStatusUI(result, ref Led19)));
        }

        void MainWindow_RunningStatusChanged(object sender, EventArgs e)
        {
            if (RunningStatus)
            {
                RibbonButtonStart.IsEnabled = false;
                RibbonButtonStop.IsEnabled = true;
            }
            else
            {
                RibbonButtonStart.IsEnabled = true;
                RibbonButtonStop.IsEnabled = false;
            }
        }

        private void RibbonButtonStart_Click(object sender, RoutedEventArgs e)
        {
            StartMonitoring();
        }

        private void StartMonitoring()
        {
            timer18 = new Timer(GetStatus18, "MHDR", 0, 5000);
            timer19 = new Timer(GetStatus19, "MHDR", 1, 5000);

            var controls1 = WrapPanel1.ChildrenOfType<MonitoringAIO>();
            var controls2 = WrapPanel2.ChildrenOfType<MonitoringAIO>();
            var controls3 = WrapPanel3.ChildrenOfType<MonitoringAIO>();
            var controls4 = WrapPanel4.ChildrenOfType<MonitoringAIO>();
            var controls5 = WrapPanel5.ChildrenOfType<MonitoringAIO>();
            var controls6 = WrapPanel6.ChildrenOfType<MonitoringAIO>();
            var controls7 = WrapPanel7.ChildrenOfType<MonitoringAIO>();
            var controls8 = WrapPanel8.ChildrenOfType<MonitoringAIO>();
            var controls9 = WrapPanel9.ChildrenOfType<MonitoringAIO>();


            foreach (var monitoringAio in controls1)
            {
                monitoringAio.StartMonitoring();
            }

            foreach (var monitoringAio in controls1)
            {
                monitoringAio.StartMonitoring();
            }

            foreach (var monitoringAio in controls2)
            {
                monitoringAio.StartMonitoring();
            }

            foreach (var monitoringAio in controls3)
            {
                monitoringAio.StartMonitoring();
            }

            foreach (var monitoringAio in controls4)
            {
                monitoringAio.StartMonitoring();
            }

            foreach (var monitoringAio in controls5)
            {
                monitoringAio.StartMonitoring();
            }

            foreach (var monitoringAio in controls6)
            {
                monitoringAio.StartMonitoring();
            }

            foreach (var monitoringAio in controls7)
            {
                monitoringAio.StartMonitoring();
            }

            foreach (var monitoringAio in controls8)
            {
                monitoringAio.StartMonitoring();
            }

            foreach (var monitoringAio in controls9)
            {
                monitoringAio.StartMonitoring();
            }

            RunningStatus = true;
        }

        void monitoringAio_SelectionChanged(object sender, EventArgs e)
        {
            var currentSelected = sender as MonitoringAIO;
            if (Equals(LastSelected, currentSelected))
            {
                return;
            }
            if (LastSelected != null)
            {
                LastSelected.IsSelected = false;
            }

            LastSelected = currentSelected;
        }

        private void RibbonButtonStop_Click(object sender, RoutedEventArgs e)
        {
            StopMonitoring();
        }

        private void StopMonitoring()
        {
            timer18.Dispose();
            timer19.Dispose();

            var controls1 = WrapPanel1.ChildrenOfType<MonitoringAIO>();
            var controls2 = WrapPanel2.ChildrenOfType<MonitoringAIO>();
            var controls3 = WrapPanel3.ChildrenOfType<MonitoringAIO>();
            var controls4 = WrapPanel4.ChildrenOfType<MonitoringAIO>();
            var controls5 = WrapPanel5.ChildrenOfType<MonitoringAIO>();
            var controls6 = WrapPanel6.ChildrenOfType<MonitoringAIO>();
            var controls7 = WrapPanel7.ChildrenOfType<MonitoringAIO>();
            var controls8 = WrapPanel8.ChildrenOfType<MonitoringAIO>();
            var controls9 = WrapPanel9.ChildrenOfType<MonitoringAIO>();


            foreach (var monitoringAio in controls1)
            {
                monitoringAio.StopMonitoring();
            }

            foreach (var monitoringAio in controls1)
            {
                monitoringAio.StopMonitoring();
            }

            foreach (var monitoringAio in controls2)
            {
                monitoringAio.StopMonitoring();
            }

            foreach (var monitoringAio in controls3)
            {
                monitoringAio.StopMonitoring();
            }

            foreach (var monitoringAio in controls4)
            {
                monitoringAio.StopMonitoring();
            }

            foreach (var monitoringAio in controls5)
            {
                monitoringAio.StopMonitoring();
            }

            foreach (var monitoringAio in controls6)
            {
                monitoringAio.StopMonitoring();
            }

            foreach (var monitoringAio in controls7)
            {
                monitoringAio.StopMonitoring();
            }

            foreach (var monitoringAio in controls8)
            {
                monitoringAio.StopMonitoring();
            }

            foreach (var monitoringAio in controls9)
            {
                monitoringAio.StopMonitoring();
            }


            RunningStatus = false;
        }

        private void RibbonButtonShowChart_Click(object sender, RoutedEventArgs e)
        {
            if (LastSelected != null && LastSelected.IsSelected == true)
            {
                WindowCharts windowCharts = new WindowCharts();
                windowCharts.Id = LastSelected.Id;
                windowCharts.EndTime = DateTime.Now;
                windowCharts.StartTime = DateTime.Now - new TimeSpan(1, 0, 0, 0);
                windowCharts.TitleFoSensor = LastSelected.Title;
                windowCharts.Show();
            }
        }

        private void RibbonButtonShowGrid_Click(object sender, RoutedEventArgs e)
        {
            if (LastSelected != null && LastSelected.IsSelected == true)
            {
                WindowGrid windowGrid = new WindowGrid();
                windowGrid.Id = LastSelected.Id;
                windowGrid.EndTime = DateTime.Now;
                windowGrid.StartTime = DateTime.Now - new TimeSpan(1, 0, 0, 0);
                windowGrid.TitleFoSensor = LastSelected.Title;
                windowGrid.Show();
            }
        }

        private void RibbonButtonShowChart2_Click(object sender, RoutedEventArgs e)
        {
            if (LastSelected != null && LastSelected.IsSelected == true)
            {
                WindowChart2 windowChart2 = new WindowChart2();
                windowChart2.Id = LastSelected.Id;
                windowChart2.EndTime = DateTime.Now;
                windowChart2.StartTime = DateTime.Now - new TimeSpan(1, 0, 0, 0);
                windowChart2.TitleFoSensor = LastSelected.Title;
                windowChart2.Show();
            }
        }

        private void RibbonButtonCalibration_Click_1(object sender, RoutedEventArgs e)
        {
            WindowCalibration windowCalibration = new WindowCalibration();

            if (LastSelected != null && LastSelected.IsSelected)
            {
                windowCalibration.Id = LastSelected.Id;
            }

            windowCalibration.Show();
        }

        private void RibbonButtonDeviceStausHistories_Click(object sender, RoutedEventArgs e)
        {
            WindowDeviceStatus windowDeviceStatus = new WindowDeviceStatus();
            windowDeviceStatus.EndTime = DateTime.Now;
            windowDeviceStatus.StartTime = DateTime.Now - new TimeSpan(1, 0, 0, 0);
            windowDeviceStatus.Show();
        }

        private void RibbonButtonAbout_Click_1(object sender, RoutedEventArgs e)
        {
            WindowAbout windowAbout = new WindowAbout();
            windowAbout.ShowDialog();
        }
    }
}

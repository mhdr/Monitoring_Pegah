using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;
using Monitoring.Lib;
using MonitoringServiceLibrary.Lib;
using SharedLibrary;
using Telerik.Windows.Controls.ChartView;
using MonitoringServiceLibrary;
using NLog;

namespace Monitoring
{
    /// <summary>
    /// Interaction logic for WindowChart2.xaml
    /// </summary>
    public partial class WindowChart2 : Window
    {
        public int Id { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        private List<HistoricalDataForChart> dataSource;
        public string TitleFoSensor { get; set; }
        private IMonitoringService channel = null;
        private Logger logger = null;

        public WindowChart2()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            logger = LogManager.GetCurrentClassLogger();

            InitializeChannel();
            InitChart();
            LoadSettings();
            BindChartAsync();
        }

        private void InitChart()
        {
            chart.Series.Clear();
            chart.Series.Add(new LineSeries());
            LineSeries series = (LineSeries)this.chart.Series[0];
            series.CategoryBinding = new PropertyNameDataPointBinding() { PropertyName = "Date" };
            series.ValueBinding = new PropertyNameDataPointBinding() { PropertyName = "Value" };
            //series.Stroke = Brushes.Green;
            series.StrokeThickness = 2;

            if (Properties.Settings.Default.DataPointForChart2Enabled)
            {
                DataTemplate dt = (DataTemplate)FindResource("dtChartPoints");
                series.PointTemplate = dt;
            }
            else
            {
                series.PointTemplate = null;
            }
        }

        private void LoadSettings()
        {
            if (Properties.Settings.Default.DataPointForChart2Enabled)
            {
                ToggleButtonEnableDataPoint.IsChecked = true;
            }
            else
            {
                ToggleButtonEnableDataPoint.IsChecked = false;
            }

        }

        private void InitializeChannel()
        {
            this.channel = Lib.Statics.Channel;
        }

        private void BindChart()
        {
            try
            {
                dataSource = channel.GetHistoricalDataForChart(this.Id, this.StartTime, this.EndTime);
            }
            catch (System.Exception ex)
            {
                logger.LogException(LogLevel.Info, string.Format("WindowChart2.BindChart"), ex);
            }
            Dispatcher.BeginInvoke(new Action(BindChartUI));
        }

        private void BindChartAsync()
        {
            busyIndicator.IsBusy = true;
            Thread td = new Thread(BindChart);
            td.Priority = ThreadPriority.AboveNormal;
            td.Start();
        }

        private void BindChartUI()
        {
            chart.Series[0].ItemsSource = dataSource;
            busyIndicator.IsBusy = false;
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
            BindChartAsync();
        }

        private void MenuItemZoomIn_Click(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {
            chart.Zoom = new Size(chart.Zoom.Width + 1, 1);
        }

        private void MenuItemResetZoom_Click(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {
            chart.Zoom = new Size(1, 1);
            chart.PanOffset = new Point(0, 0);
        }

        private void MenuItemZoomOut_Click(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {
            if (chart.Zoom.Width >= 2)
            {
                chart.Zoom = new Size(chart.Zoom.Width - 1, 1);
            }
        }

        private void ToggleButtonEnableDataPoint_Click(object sender, RoutedEventArgs e)
        {
            if ((bool)ToggleButtonEnableDataPoint.IsChecked)
            {
                Properties.Settings.Default.DataPointForChart2Enabled = true;
                Properties.Settings.Default.Save();
            }
            else
            {
                Properties.Settings.Default.DataPointForChart2Enabled = false;
                Properties.Settings.Default.Save();
            }

            InitChart();
            BindChartAsync();
        }

        private void ChartTrackBallBehavior_TrackInfoUpdated(object sender, TrackBallInfoEventArgs e)
        {
            foreach (DataPointInfo info in e.Context.DataPointInfos)
            {
                HistoricalDataForChart closestData = (HistoricalDataForChart)e.Context.ClosestDataPoint.DataPoint.DataItem;
                info.DisplayContent = string.Format("مقدار : {0}\nتاریخ  : {1}", closestData.Value, closestData.Date.ToPersianString());
            }

        }

        private void RibbonButtonShowChart_Click(object sender, RoutedEventArgs e)
        {
            WindowCharts windowCharts = new WindowCharts();
            windowCharts.Id = this.Id;
            windowCharts.EndTime = this.EndTime;
            windowCharts.StartTime = this.StartTime;
            windowCharts.TitleFoSensor = this.TitleFoSensor;
            windowCharts.Show();
        }
    }
}

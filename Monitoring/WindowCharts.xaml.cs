using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
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
using Microsoft.Win32;
using Monitoring.Lib;
using MonitoringServiceLibrary;
using MonitoringServiceLibrary.Lib;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.Charting;
using NLog;

namespace Monitoring
{
    /// <summary>
    /// Interaction logic for WindowCharts.xaml
    /// </summary>
    public partial class WindowCharts : Window
    {
        public int Id { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        private List<HistoricalDataForChart> dataSource;
        public string TitleFoSensor { get; set; }
        public bool IsDataFromOtherWindow { get; set; }
        public List<HistoricalDataForChart> DataFromOtherWindow { get; set; }
        private IMonitoringService channel = null;
        private Logger logger = null;

        public WindowCharts()
        {
            LocalizationManager.Manager = new MyLocalization();
            InitializeComponent();
            logger = LogManager.GetCurrentClassLogger();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            InitializeChannel();
            LoadSettings();
            InitChart();
            BindChartAsync();
        }

        private void LoadSettings()
        {
            if (Properties.Settings.Default.AnimationForChartEnabled)
            {
                ToggleButtonAnimationForChartEnabled.IsChecked = true;
            }
            else
            {
                ToggleButtonAnimationForChartEnabled.IsChecked = false;
            }

            if (Properties.Settings.Default.AnimationForChartEnabled)
            {
                chart.DefaultView.ChartArea.EnableAnimations = true;
            }
            else
            {
                chart.DefaultView.ChartArea.EnableAnimations = false;
            }

            int chartType = Properties.Settings.Default.ChartType;

            switch (chartType)
            {
                case (int)ChartType.LineSeriesDefinition:
                    MenuItemLineSeriesDefinition.IsChecked = true;
                    MenuItemSplineSeriesDefinition.IsChecked = false;
                    MenuItemBarSeriesDefinition.IsChecked = false;
                    break;

                case (int)ChartType.SplineSeriesDefinition:
                    MenuItemLineSeriesDefinition.IsChecked = false;
                    MenuItemSplineSeriesDefinition.IsChecked = true;
                    MenuItemBarSeriesDefinition.IsChecked = false;
                    break;
                case (int)ChartType.BarSeriesDefinition:
                    MenuItemLineSeriesDefinition.IsChecked = false;
                    MenuItemSplineSeriesDefinition.IsChecked = false;
                    MenuItemBarSeriesDefinition.IsChecked = true;
                    break;
            }
        }

        private void InitChart()
        {
            chart.SeriesMappings.Clear();

            SeriesMapping seriesMapping = new SeriesMapping();

            int chartType = Properties.Settings.Default.ChartType;

            switch (chartType)
            {
                case (int)ChartType.LineSeriesDefinition:
                    seriesMapping.SeriesDefinition = new LineSeriesDefinition() { ShowItemToolTips = true };
                    break;

                case (int)ChartType.SplineSeriesDefinition:
                    seriesMapping.SeriesDefinition = new SplineSeriesDefinition() { ShowItemToolTips = true };
                    break;
                case (int)ChartType.BarSeriesDefinition:
                    seriesMapping.SeriesDefinition = new BarSeriesDefinition() { ShowItemToolTips = true };
                    break;
            }

            seriesMapping.ItemMappings.Add(new ItemMapping("Date", DataPointMember.XCategory));
            seriesMapping.ItemMappings.Add(new ItemMapping("Value", DataPointMember.YValue));
            chart.SeriesMappings.Add(seriesMapping);
            chart.DefaultView.ChartArea.AxisX.LabelRotationAngle = 45;
            chart.DefaultView.ChartArea.AxisX.LabelStep = 10;
            chart.DefaultView.ChartArea.AxisX.DefaultLabelFormat = "g";
            chart.DefaultSeriesDefinition.LegendDisplayMode = LegendDisplayMode.None;
            chart.DefaultView.ChartTitle.Content = TitleFoSensor;
            chart.DefaultView.ChartArea.LabelFormatBehavior = LabelFormatBehavior.HumanReadable;
            chart.DefaultView.ChartArea.AxisX.IsDateTime = true;
            //chart.DefaultSeriesDefinition.Appearance.Stroke = Brushes.LightGreen;
            //chart.DefaultSeriesDefinition.Appearance.StrokeThickness = 2;
            chart.DefaultView.ChartArea.AxisX.LayoutMode = AxisLayoutMode.Between;
            chart.DefaultView.ChartLegend.UseAutoGeneratedItems = false;
            chart.DefaultView.ChartLegend.Visibility = Visibility.Hidden;
            chart.DefaultView.ChartArea.AxisX.StripLinesVisibility = Visibility.Hidden;
            chart.DefaultView.ChartArea.AxisY.StripLinesVisibility = Visibility.Hidden;
            chart.DefaultView.ChartArea.NoDataString = "اطلاعاتی برای نمایش موجود نمی باشد";
            chart.DefaultView.ChartArea.ItemToolTipOpening += new ItemToolTipEventHandler(ChartArea_ItemToolTipOpening);
            chart.DefaultView.ChartArea.ZoomScrollSettingsX.ScrollMode = ScrollMode.ScrollAndZoom;
            chart.SamplingSettings.SamplingThreshold = 0;
        }

        void ChartArea_ItemToolTipOpening(ItemToolTip2D tooltip, ItemToolTipEventArgs e)
        {
            try
            {
                var data = (HistoricalDataForChart)e.DataPoint.DataItem;
                ChartTooltip chartTooltip = new ChartTooltip();
                chartTooltip.Date = data.Date;
                chartTooltip.Value = data.Value;

                tooltip.Content = chartTooltip;
            }
            catch (Exception ex)
            {


            }
        }

        private void InitializeChannel()
        {
            this.channel = Lib.Statics.Channel;
        }

        private void BindChart()
        {
            if (IsDataFromOtherWindow)
            {
                dataSource = DataFromOtherWindow;
                IsDataFromOtherWindow = false;
            }
            else
            {
                try
                {
                    dataSource = channel.GetHistoricalDataForChart(this.Id, this.StartTime, this.EndTime);
                }
                catch (System.Exception ex)
                {
                    logger.LogException(LogLevel.Info, string.Format("WindowCharts.BindChart"), ex);
                }
            }

            Dispatcher.Invoke(new Action(BindChartUI));
        }

        private void BindChartUI()
        {
            chart.ItemsSource = dataSource;

            if (dataSource.Count > 10000)
            {
                chart.DefaultView.ChartArea.ZoomScrollSettingsX.RangeStart = 0.98;
                chart.DefaultView.ChartArea.ZoomScrollSettingsX.RangeEnd = 1;
            }
            if (dataSource.Count > 1000)
            {
                chart.DefaultView.ChartArea.ZoomScrollSettingsX.RangeStart = 0.95;
                chart.DefaultView.ChartArea.ZoomScrollSettingsX.RangeEnd = 1;
            }
            else if (dataSource.Count > 200)
            {
                chart.DefaultView.ChartArea.ZoomScrollSettingsX.RangeStart = 0.9;
                chart.DefaultView.ChartArea.ZoomScrollSettingsX.RangeEnd = 1;
            }

            busyIndicator.IsBusy = false;
        }

        private void BindChartAsync()
        {
            busyIndicator.IsBusy = true;
            Thread td = new Thread(BindChart);
            td.Priority = ThreadPriority.AboveNormal;
            td.Start();
        }

        private void RibbonButtonChangeDate_Click(object sender, RoutedEventArgs e)
        {
            WindowSetDateForChart windowSetDateForChart = new WindowSetDateForChart();
            windowSetDateForChart.StartTime = this.StartTime;
            windowSetDateForChart.EndTime = this.EndTime;
            windowSetDateForChart.ChartDateChanged += new ChartDateChangedEventHandler(windowSetDateForChart_ChartDateChanged);
            windowSetDateForChart.Show();
        }

        private void windowSetDateForChart_ChartDateChanged(object sender, ChartDateChangedEventArgs e)
        {
            this.StartTime = e.StartTime;
            this.EndTime = e.EndTime;
            BindChartAsync();
        }

        private void MenuItemZoomIn_Click(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {
            double zoomCenter = chart.DefaultView.ChartArea.ZoomScrollSettingsX.RangeStart + (chart.DefaultView.ChartArea.ZoomScrollSettingsX.Range / 2);
            double newRange = Math.Max(chart.DefaultView.ChartArea.ZoomScrollSettingsX.MinZoomRange, chart.DefaultView.ChartArea.ZoomScrollSettingsX.Range) / 2;
            chart.DefaultView.ChartArea.ZoomScrollSettingsX.RangeStart = Math.Max(0, zoomCenter - (newRange / 2));
            chart.DefaultView.ChartArea.ZoomScrollSettingsX.RangeEnd = Math.Min(1, zoomCenter + (newRange / 2));
        }

        private void MenuItemZoomOut_Click(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {
            double zoomCenter = chart.DefaultView.ChartArea.ZoomScrollSettingsX.RangeStart + (chart.DefaultView.ChartArea.ZoomScrollSettingsX.Range / 2);
            double newRange = Math.Min(1, chart.DefaultView.ChartArea.ZoomScrollSettingsX.Range) * 2;

            if (zoomCenter + (newRange / 2) > 1)
                zoomCenter = 1 - (newRange / 2);
            else if (zoomCenter - (newRange / 2) < 0)
                zoomCenter = newRange / 2;

            chart.DefaultView.ChartArea.ZoomScrollSettingsX.RangeStart = Math.Max(0, zoomCenter - newRange / 2);
            chart.DefaultView.ChartArea.ZoomScrollSettingsX.RangeEnd = Math.Min(1, zoomCenter + newRange / 2);

        }

        private void MenuItemResetZoom_Click(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {
            chart.DefaultView.ChartArea.ZoomScrollSettingsX.RangeStart = 0;
            chart.DefaultView.ChartArea.ZoomScrollSettingsX.RangeEnd = 1;
        }

        private void MenuItemExportToExcel_Click(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.DefaultExt = "xls";
            dialog.Filter = "*.xls | Excel";
            if (!(bool)dialog.ShowDialog())
                return;
            chart.ExportToExcelML(dialog.FileName);
        }

        private void MenuItemExportToPNG_Click(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.DefaultExt = "png";
            dialog.Filter = "*.png | Image";
            if (!(bool)dialog.ShowDialog())
                return;
            chart.ExportToImage(dialog.FileName);
        }

        private void MenuItemLineSeriesDefinition_Click(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {
            MenuItemLineSeriesDefinition.IsChecked = true;
            Properties.Settings.Default.ChartType = (int)ChartType.LineSeriesDefinition;
            Properties.Settings.Default.Save();
            LoadSettings();
            InitChart();
        }

        private void ToggleButtonAnimationForChartEnabled_Click(object sender, RoutedEventArgs e)
        {
            if ((bool)ToggleButtonAnimationForChartEnabled.IsChecked)
            {
                Properties.Settings.Default.AnimationForChartEnabled = true;
                Properties.Settings.Default.Save();
                LoadSettings();
            }
            else
            {
                Properties.Settings.Default.AnimationForChartEnabled = false;
                Properties.Settings.Default.Save();
                LoadSettings();
            }
        }

        private void MenuItemSplineSeriesDefinition_Click(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {
            MenuItemLineSeriesDefinition.IsChecked = true;
            Properties.Settings.Default.ChartType = (int)ChartType.SplineSeriesDefinition;
            Properties.Settings.Default.Save();
            LoadSettings();
            InitChart();
        }

        private void MenuItemBarSeriesDefinition_Click(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {
            MenuItemLineSeriesDefinition.IsChecked = true;
            Properties.Settings.Default.ChartType = (int)ChartType.BarSeriesDefinition;
            Properties.Settings.Default.Save();
            this.LoadSettings();
            this.InitChart();
        }

        private void RibbonButtonShowChart2_Click(object sender, RoutedEventArgs e)
        {
            WindowChart2 windowChart2 = new WindowChart2();
            windowChart2.Id = this.Id;
            windowChart2.EndTime = this.EndTime;
            windowChart2.StartTime = this.StartTime;
            windowChart2.TitleFoSensor = this.TitleFoSensor;
            windowChart2.Show();
        }
    }
}

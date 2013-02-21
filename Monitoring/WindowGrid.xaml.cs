using System;
using System.Collections.Generic;
using System.IO;
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
using MonitoringServiceLibrary.Lib;
using Telerik.Windows.Controls;
using MonitoringServiceLibrary;
using NLog;

namespace Monitoring
{
    /// <summary>
    /// Interaction logic for WindowGrid.xaml
    /// </summary>
    public partial class WindowGrid : Window
    {
        public int Id { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        private List<HistoricalDataForGrid> dataSource;
        public string TitleFoSensor { get; set; }
        private IMonitoringService channel = null;
        private Logger logger = null;

        public WindowGrid()
        {
            LocalizationManager.Manager = new MyLocalization();
            InitializeComponent();
            logger = LogManager.GetCurrentClassLogger();
        }

        void BindGridUI()
        {
            gridView.ItemsSource = dataSource;
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
            BindGridAsync();
        }

        private void MenuItemExportToExcel_Click(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {
            string extension = "xls";
            SaveFileDialog dialog = new SaveFileDialog()
            {
                DefaultExt = extension,
                Filter =
                    String.Format("{1} files (*.{0})|*.{0}|All files (*.*)|*.*", extension,
                                  "Excel"),
                FilterIndex = 1
            };
            if (dialog.ShowDialog() == true)
            {
                using (Stream stream = dialog.OpenFile())
                {
                    gridView.Export(stream,
                                              new GridViewExportOptions()
                                              {
                                                  Format = ExportFormat.ExcelML,
                                                  ShowColumnHeaders = true,
                                                  ShowColumnFooters = false,
                                                  ShowGroupFooters = false,
                                                  Encoding = Encoding.UTF8
                                              });
                }

            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            InitializeChannel();
            BindGridAsync();
        }

        private void InitializeChannel()
        {
            this.channel = Lib.Statics.Channel;
        }

        private void BindGrid()
        {
            try
            {
                dataSource = channel.GetHistoricalDataForGrid(this.Id, this.StartTime, this.EndTime);
            }
            catch (System.Exception ex)
            {
                logger.LogException(LogLevel.Info, string.Format("WindowGrid.BindGrid"), ex);
            }
            Dispatcher.BeginInvoke(new Action(BindGridUI));
        }

        private void BindGridAsync()
        {
            busyIndicator.IsBusy = true;
            Thread td=new Thread(BindGrid);
            td.Priority = ThreadPriority.AboveNormal;
            td.Start();
        }

        private void MenuItemExportToCSV_Click(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {
            string extension = "csv";
            SaveFileDialog dialog = new SaveFileDialog()
            {
                DefaultExt = extension,
                Filter =
                    String.Format("{1} files (*.{0})|*.{0}|All files (*.*)|*.*", extension,
                                  "CSV"),
                FilterIndex = 1
            };
            if (dialog.ShowDialog() == true)
            {
                using (Stream stream = dialog.OpenFile())
                {
                    gridView.Export(stream,
                                              new GridViewExportOptions()
                                              {
                                                  Format = ExportFormat.Csv,
                                                  ShowColumnHeaders = true,
                                                  ShowColumnFooters = false,
                                                  ShowGroupFooters = false,
                                                  Encoding = Encoding.UTF8
                                              });
                }

            }
        }

        private void MenuItemExportToHtml_Click(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {
            string extension = "html";
            SaveFileDialog dialog = new SaveFileDialog()
            {
                DefaultExt = extension,
                Filter =
                    String.Format("{1} files (*.{0})|*.{0}|All files (*.*)|*.*", extension,
                                  "HTML"),
                FilterIndex = 1
            };
            if (dialog.ShowDialog() == true)
            {
                using (Stream stream = dialog.OpenFile())
                {
                    gridView.Export(stream,
                                              new GridViewExportOptions()
                                              {
                                                  Format = ExportFormat.Html,
                                                  ShowColumnHeaders = true,
                                                  ShowColumnFooters = false,
                                                  ShowGroupFooters = false,
                                                  Encoding = Encoding.UTF8
                                              });
                }

            }
        }

        private void RibbonButtonShowChart_Click(object sender, RoutedEventArgs e)
        {

            WindowCharts windowCharts = new WindowCharts();
            windowCharts.Id = this.Id;
            windowCharts.EndTime = DateTime.Now;
            windowCharts.StartTime = DateTime.Now - new TimeSpan(0, 0, 0, 1);
            windowCharts.TitleFoSensor = this.TitleFoSensor;
            windowCharts.IsDataFromOtherWindow = true;

            List<HistoricalDataForChart> list = new List<HistoricalDataForChart>();

            foreach (var item in gridView.Items)
            {
                HistoricalDataForGrid currentItem = (HistoricalDataForGrid) item;
                list.Add(new HistoricalDataForChart(currentItem.Value,currentItem.Date));
            }

            windowCharts.DataFromOtherWindow = list;
            windowCharts.Show();

        }

    }
}

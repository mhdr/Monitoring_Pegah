using System;
using System.Collections.Generic;
using System.Linq;
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
using MonitoringServiceLibrary;
using MonitoringServiceLibrary.Lib;
using NLog;

namespace Monitoring
{
    /// <summary>
    /// Interaction logic for WindowCalibration.xaml
    /// </summary>
    public partial class WindowCalibration : Window
    {
        private List<Calibration2> dataSource = null;
        public int Id { get; set; }
        private Calibration2 LastSelected;
        private IMonitoringService channel = null;
        private Logger logger = null;

        public WindowCalibration()
        {
            InitializeComponent();
        }

        private void Window_Loaded_1(object sender, RoutedEventArgs e)
        {
            logger = LogManager.GetCurrentClassLogger();

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

        private void InitializeChannel()
        {
            this.channel = Lib.Statics.Channel;
        }

        private void BindGrid()
        {
            if (this.Id > 0)
            {
                try
                {
                    dataSource = channel.GetCalibrations(this.Id);
                }
                catch (System.Exception ex)
                {
                    logger.LogException(LogLevel.Info, string.Format("WindowCalibration.BindGrid"), ex);
                }
            }
            else
            {
                try
                {
                    dataSource = channel.GetCalibrations();
                }
                catch (System.Exception ex)
                {
                    logger.LogException(LogLevel.Info, string.Format("WindowCalibration.BindGrid"), ex);
                }
            }

            Dispatcher.BeginInvoke(new Action(BindGridUI));
        }

        private void BindGridUI()
        {
            gridView.ItemsSource = dataSource;
            busyIndicator.IsBusy = false;
        }

        private void RibbonButtonNewCalibration_Click_1(object sender, RoutedEventArgs e)
        {
            WindowNewCalibration windowNewCalibration = new WindowNewCalibration();
            windowNewCalibration.NewCalibrationAdded += windowNewCalibration_NewCalibrationAdded;
            windowNewCalibration.Show();
        }

        void windowNewCalibration_NewCalibrationAdded(object sender, EventArgs e)
        {
            BindGridAsync();
        }

        private void RibbonButtonDeleteCalibration_Click_1(object sender, RoutedEventArgs e)
        {
            if (LastSelected == null)
            {
                return;
            }

            //TODO DialogBox

            try
            {
                if (channel.DeleteCalibration(LastSelected.CalibrationId))
                {
                    BindGridAsync();
                }
            }
            catch (System.Exception ex)
            {
                logger.LogException(LogLevel.Info, string.Format("WindowCalibration.RibbonButtonDeleteCalibration_Click_1"), ex);
            }
        }

        private void gridView_SelectionChanged_1(object sender, Telerik.Windows.Controls.SelectionChangeEventArgs e)
        {
            LastSelected = gridView.SelectedItem as Calibration2;
        }

        private void RibbonButtonEnableCalibration_Click_1(object sender, RoutedEventArgs e)
        {
            if (LastSelected == null)
            {
                return;
            }

            if (LastSelected.IsEnabled == false)
            {
                EnableCalibrationResult result = EnableCalibrationResult.Failed;

                try
                {
                    result = channel.EnableCalibration(LastSelected.CalibrationId);
                }
                catch (System.Exception ex)
                {
                    logger.LogException(LogLevel.Info, string.Format("WindowCalibration.RibbonButtonEnableCalibration_Click_1"), ex);
                }

                if (result == EnableCalibrationResult.Successful)
                {
                    BindGridAsync();
                }
                else if (result == EnableCalibrationResult.AlreadyEnabledForThisId)
                {
                    DialogBoxOk dialogBoxOk = new DialogBoxOk();
                    dialogBoxOk.Message = string.Format("برای {0} گزینه فعال وجود دارد،ابتدا آن را غیر فعال کنید",
                                                        LastSelected.Title);
                    dialogBoxOk.Width = 300;
                    dialogBoxOk.Height = 120;
                    dialogBoxOk.ShowDialog();
                }
            }
        }

        private void RibbonButtonDisableCalibration_Click_1(object sender, RoutedEventArgs e)
        {
            if (LastSelected == null)
            {
                return;
            }

            if (LastSelected.IsEnabled == true)
            {
                try
                {
                    if (channel.DisableCalibration(LastSelected.CalibrationId))
                    {
                        BindGridAsync();
                    }
                }
                catch (System.Exception ex)
                {
                    logger.LogException(LogLevel.Info, string.Format("WindowCalibration.RibbonButtonDisableCalibration_Click_1"), ex);
                }
            }
        }
    }
}

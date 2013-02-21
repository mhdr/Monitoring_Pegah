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
using MonitoringServiceLibrary.Lib;
using MonitoringServiceLibrary;
using SharedLibrary;
using NLog;


namespace Monitoring
{
    /// <summary>
    /// Interaction logic for WindowNewCalibration.xaml
    /// </summary>
    public partial class WindowNewCalibration : Window
    {
        private List<IdMinimum> dataSource = null;
        public event EventHandler NewCalibrationAdded;
        private IMonitoringService channel = null;
        private Logger logger = null;
        
        public void OnNewCalibrationAdded(EventArgs e)
        {
            EventHandler handler = NewCalibrationAdded;
            if (handler != null) handler(this, e);
        }

        public WindowNewCalibration()
        {
            InitializeChannel();
            InitializeComponent();
        }

        private void InitializeChannel()
        {
            this.channel = Lib.Statics.Channel;
        }

        private void Window_Loaded_1(object sender, RoutedEventArgs e)
        {
            logger = LogManager.GetCurrentClassLogger();

            BindComboBoxTitlesAsync();
        }

        private void BindComboBoxTitlesAsync()
        {
            Thread td=new Thread(BindComboBoxTitles);
            td.Priority = ThreadPriority.AboveNormal;
            td.Start();
        }

        private void BindComboBoxTitles()
        {
            try
            {
                dataSource = channel.GetIdsMinimum();
            }
            catch (System.Exception ex)
            {
                logger.LogException(LogLevel.Info, string.Format("WindowNewCalibration.BindComboBoxTitles"), ex);
            }
            Dispatcher.BeginInvoke(new Action(BindComboBoxTitlesUI));
        }

        private void BindComboBoxTitlesUI()
        {
            ComboBoxTitles.ItemsSource = dataSource;
            ComboBoxTitles.SelectedIndex = 0;
        }

        private void ButtonSave_Click_1(object sender, RoutedEventArgs e)
        {
            int id = (int)ComboBoxTitles.SelectedValue;
            CalibrationOperation calibrationOperation = CalibrationOperation.Jam;

            if (ComboBoxCalibrationOperation.SelectedIndex == 0)
            {
                calibrationOperation = CalibrationOperation.Jam;
            }
            else if (ComboBoxCalibrationOperation.SelectedIndex == 1)
            {
                calibrationOperation = CalibrationOperation.Zarb;
            }

            double value = (double)NumericUpDownValue.Value;

            if (value == 0)
            {
                DialogBoxOk dialogBoxOk = new DialogBoxOk();
                dialogBoxOk.Message = "مقدار کالیبره نمی تواند ۰ باشد";
                dialogBoxOk.Width = 300;
                dialogBoxOk.Height = 120;
                dialogBoxOk.ShowDialog();
                return;
            }

            try
            {
                if (channel.AddNewCalibration(id, calibrationOperation, value))
                {
                    OnNewCalibrationAdded(new EventArgs());
                    NumericUpDownValue.Value = 0;
                }
            }
            catch (System.Exception ex)
            {
                logger.LogException(LogLevel.Info, string.Format("WindowNewCalibration.ButtonSave_Click_1"), ex);
            }

        }
    }
}

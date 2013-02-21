using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
using System.Windows.Threading;
using Monitoring.Lib;
using MonitoringServiceLibrary;
using SharedLibrary;
using MonitoringServiceLibrary.Lib;
using Telerik.Charting;
using Telerik.Windows.Controls.ChartView;
using NLog;

namespace Monitoring
{
    /// <summary>
    /// Interaction logic for MonitoringAIO.xaml
    /// </summary>
    public partial class MonitoringAIO : UserControl
    {
        private Timer timer = null;
        public event EventHandler SelectionChanged;
        private TimeSpan _timerInterval = new TimeSpan(0, 0, 0, 5);
        private bool _isMonitoringEnabled = true;
        private Double _calibration;
        private CalibrationOperation _calibrationOperation;
        private Double _value;
        private bool IsCalibrationFetched { get; set; }
        private IMonitoringService channel = null;
        public string PersianDateString { get; set; }
        private ObservableCollection<ChartMonitoringLiveData> collection = null;
        private Logger logger = null;


        public void OnSelectionChanged(EventArgs e)
        {
            EventHandler handler = SelectionChanged;
            if (handler != null) handler(this, e);
        }

        public MonitoringAIO()
        {
            InitializeComponent();
            InitializeOnLoad();
        }

        public string IPAddress { get; set; }

        public int ILModuleNumber { get; set; }

        public int PDInWord { get; set; }

        public string Title
        {
            get { return textBlockTitle.Text; }
            set
            {
                textBlockTitle.Text = string.Format("{0}", value);
            }
        }
        public int Id { get; set; }

        private bool _isSelected;

        public TimeSpan TimerInterval
        {
            get { return _timerInterval; }
            set
            {
                _timerInterval = value;
                timer.Change(0, TimerInterval.Seconds * 1000);
            }
        }


        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                _isSelected = value;

                //if (_isSelected)
                //{
                //    borderSelect.Background = Brushes.LightBlue;
                //}
                //else
                //{
                //    borderSelect.Background = null;
                //}

                if (_isSelected)
                {
                    toggleButtonValue.IsChecked = true;
                }
                else
                {
                    toggleButtonValue.IsChecked = false;
                }
            }
        }

        private void InitializeChannel()
        {
            this.channel = Lib.Statics.Channel;
        }

        public bool IsMonitoringEnabled
        {
            get { return _isMonitoringEnabled; }
            set
            {
                _isMonitoringEnabled = value;
                if (_isMonitoringEnabled)
                {
                    this.Visibility = Visibility.Visible;
                }
                else
                {
                    this.Visibility = Visibility.Collapsed;
                }
            }
        }

        public Double Calibration
        {
            get { return _calibration; }
            set { _calibration = value; }
        }

        public CalibrationOperation CalibrationOperation
        {
            get { return _calibrationOperation; }
            set { _calibrationOperation = value; }
        }

        public Double Value
        {
            get { return _value; }
            set { _value = Math.Round(value, 2); }
        }


        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            // TODO hazf shavad
        }

        private void InitializeOnLoad()
        {
            if (this.IsMonitoringEnabled == false)
            {
                return;
            }

            logger = LogManager.GetCurrentClassLogger();

            InitializeChannel();
            GetReadIntervalAsync();
            GetCalibrationAsync();
            InitChart();

            collection = new ObservableCollection<ChartMonitoringLiveData>();

            LineSeries series = (LineSeries)this.chart.Series[0];
            series.ItemsSource = collection;
        }

        private void InitializeTimer()
        {
            timer = new Timer(timer_Tick, "MHDR", 0, TimerInterval.Seconds * 1000);
        }

        private void GetReadIntervalAsync()
        {
            Task.Factory.StartNew(GetReadInterval);
        }

        private void GetReadInterval()
        {
            int result = 0;
            try
            {
                result = channel.GetReadInterval(this.Id);
            }
            catch (System.Exception ex)
            {
                logger.LogException(LogLevel.Info, string.Format("MonitoringAIO.GetReadInterval.{0}", this.Id), ex);
                return;
            }
            if (result > 0)
            {
                this.TimerInterval = new TimeSpan(0, 0, 0, result);
            }
        }

        private void GetCalibrationAsync()
        {
            Task.Factory.StartNew(GetCalibration);
        }

        private void GetCalibration()
        {
            CalibrationForAIO result = null;

            try
            {
                result = channel.GetCalibrationForAIO(this.Id);
            }
            catch (System.Exception ex)
            {
                logger.LogException(LogLevel.Info, string.Format("MonitoringAIO.GetCalibration.{0}", this.Id), ex);
                return;
            }

            if (result != null)
            {
                this.Calibration = result.CalibrationValue;
                this.CalibrationOperation = (CalibrationOperation)result.CalibrationOperation;
            }

            IsCalibrationFetched = true;
        }

        void timer_Tick(object state)
        {
            ReadValueFromDevice();
        }

        private void ReadValueFromDeviceAsync()
        {
            Task.Factory.StartNew(ReadValueFromDevice);
        }

        private void ReadValueFromDevice()
        {
            if (IsCalibrationFetched == false)
            {
                return;
            }

            LastValue2 result = null;

            try
            {
                result = channel.GetLastValue(this.Id);
            }
            catch (System.Exception ex)
            {
                logger.LogException(LogLevel.Info, string.Format("MonitoringAIO.ReadValueFromDevice.{0}", this.Id), ex);
                return;
            }

            if (result == null)
            {
                return;
            }

            if (result.HasError)
            {
                return;
            }

            var temp = result.Value;

            if (temp == null)
            {
                return;
            }

            if (this.Calibration != 0)
            {
                if (this.CalibrationOperation == CalibrationOperation.Jam)
                {
                    temp += this.Calibration;
                }
                else if (this.CalibrationOperation == CalibrationOperation.Zarb)
                {
                    temp *= this.Calibration;
                }
            }

            this.Value = (double)temp;
            this.PersianDateString = string.Format("تاریخ : {0}", result.PersianDateString);

            Dispatcher.BeginInvoke(new Action(SetValueOnUI));
        }

        private void SetValueOnUI()
        {
            textBlockValue.Text = Value.ToString().ToFarsi();
            textBlockValue.ToolTip = this.PersianDateString;

            if (collection.Count > 60)
            {
                collection.RemoveAt(0);
            }

            collection.Add(new ChartMonitoringLiveData(this.Value, DateTime.Now));
            chart.Visibility = Visibility.Visible;
        }


        public void StartMonitoring()
        {
            ReadValueFromDeviceAsync();
            InitializeTimer();
            toggleButtonValue.Visibility = Visibility.Visible;
        }

        public void StopMonitoring()
        {
            timer.Dispose();
        }

        private void InitChart()
        {
            chart.Series.Clear();
            chart.Series.Add(new LineSeries());
            LineSeries series = (LineSeries)this.chart.Series[0];
            series.CategoryBinding = new PropertyNameDataPointBinding() { PropertyName = "Date" };
            series.ValueBinding = new PropertyNameDataPointBinding() { PropertyName = "Value" };
            series.Stroke = Brushes.Green;
            //series.Fill = Brushes.Green;
            series.StrokeThickness = 2;
        }

        private void borderSelect_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (IsSelected == false)
            {
                IsSelected = true;
            }
            else
            {
                IsSelected = false;
            }

            OnSelectionChanged(new EventArgs());
        }

        private void toggleButtonValue_Click(object sender, RoutedEventArgs e)
        {
            if (IsSelected == false)
            {
                IsSelected = true;
            }
            else
            {
                IsSelected = false;
            }

            OnSelectionChanged(new EventArgs());
        }

    }
}

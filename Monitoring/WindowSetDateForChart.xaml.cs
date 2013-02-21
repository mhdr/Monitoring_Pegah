using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Monitoring.Lib;

namespace Monitoring
{
    /// <summary>
    /// Interaction logic for WindowSetDateForChart.xaml
    /// </summary>
    public partial class WindowSetDateForChart : Window
    {
        public event ChartDateChangedEventHandler ChartDateChanged;
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        public void OnChartDateChanged(ChartDateChangedEventArgs e)
        {
            ChartDateChangedEventHandler handler = ChartDateChanged;
            if (handler != null) handler(this, e);
        }

        public WindowSetDateForChart()
        {
            InitializeComponent();
        }

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            SetDates();
            OnChartDateChanged(new ChartDateChangedEventArgs(StartTime, EndTime));
        }

        private void SetDates()
        {
            DateTime tempDate1;
            DateTime tempDate2;
            int hour1;
            int hour2;
            int minute1;
            int minute2;

            tempDate1 = TarikhShoroe.GetDateG();
            TimeSpan hour1ts = (TimeSpan)DateTimePickerShoroe.SelectedTime;
            hour1 = hour1ts.Hours;
            minute1 = hour1ts.Minutes;
            StartTime = new DateTime(tempDate1.Year, tempDate1.Month, tempDate1.Day, hour1, minute1, 0);

            tempDate2 = TarikhPayan.GetDateG();
            TimeSpan hour2ts = (TimeSpan)DateTimePickerPayan.SelectedTime;
            hour2 = hour2ts.Hours;
            minute2 = hour2ts.Minutes;
            EndTime = new DateTime(tempDate2.Year, tempDate2.Month, tempDate2.Day, hour2, minute2, 0);
        }

        private void ButtonSaveAndClose_Click(object sender, RoutedEventArgs e)
        {
            SetDates();
            OnChartDateChanged(new ChartDateChangedEventArgs(StartTime, EndTime));
            this.Close();
        }

        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            TarikhShoroe.SetDateG(StartTime.Year, StartTime.Month, StartTime.Day);
            TarikhPayan.SetDateG(EndTime.Year, EndTime.Month, EndTime.Day);
            DateTimePickerShoroe.SelectedTime = new TimeSpan(0, StartTime.Hour, StartTime.Minute, 0);
            DateTimePickerPayan.SelectedTime = new TimeSpan(0, EndTime.Hour, EndTime.Minute, 0);
        }

        private void ButtonSetDateToCurrent_Click(object sender, RoutedEventArgs e)
        {
            DateTime currentDate = DateTime.Now;
            TarikhPayan.SetDateG(currentDate.Year, currentDate.Month, currentDate.Day);
            DateTimePickerPayan.SelectedTime = new TimeSpan(0, currentDate.Hour, currentDate.Minute, 0);
        }
    }
}

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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Monitoring.Lib;
using SharedLibrary;

namespace Monitoring
{
    /// <summary>
    /// Interaction logic for ChartTooltip.xaml
    /// </summary>
    public partial class ChartTooltip : UserControl
    {
        public double Value { get; set; }
        public DateTime Date { get; set; }

        public ChartTooltip()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            TextblockValue.Text = Value.ToString().ToFarsi();
            TextBlockDate.Text = Date.ToPersianString();
        }
    }
}

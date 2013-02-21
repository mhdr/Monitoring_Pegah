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

namespace Monitoring
{
    /// <summary>
    /// Interaction logic for ToolTipDeviceStatus.xaml
    /// </summary>
    public partial class ToolTipDeviceStatus : UserControl
    {
        private string _iPAddress;
        private string _status;
        private string _description;

        public ToolTipDeviceStatus()
        {
            InitializeComponent();
        }

        public string IPAddress
        {
            get { return _iPAddress; }
            set { _iPAddress = value; }
        }

        public string Status
        {
            get { return _status; }
            set { _status = value; }
        }

        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            TextBlockIP.Text = this.IPAddress;
            TextBlockStatus.Text = this.Status;
            TextBlockDescription.Text = this.Description;
        }
    }
}

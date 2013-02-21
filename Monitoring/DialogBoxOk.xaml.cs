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

namespace Monitoring
{
    /// <summary>
    /// Interaction logic for DialogBoxOk.xaml
    /// </summary>
    public partial class DialogBoxOk : Window
    {
        public string Message { get; set; }

        public DialogBoxOk()
        {
            InitializeComponent();
        }

        private void Window_Loaded_1(object sender, RoutedEventArgs e)
        {
            TextBlockMessage.Text = this.Message;
        }

        private void ButtonOk_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using MonitoringServiceLibrary.Lib;

namespace Monitoring.Lib
{
    
    class GridViewStyleForCalibration : StyleSelector
    {
        public Style NormalStyle { get; set; }
        public Style EnabledStyle { get; set; }

        public override System.Windows.Style SelectStyle(object item, System.Windows.DependencyObject container)
        {
            Calibration2 currentItem = item as Calibration2;

            if (currentItem.IsEnabled)
            {
                return EnabledStyle;
            }

            return NormalStyle;
        }
    }
}

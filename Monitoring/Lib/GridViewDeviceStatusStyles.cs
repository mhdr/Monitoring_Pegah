using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using MonitoringServiceLibrary.Lib;

namespace Monitoring.Lib
{
    public class GridViewDeviceStatusStyles : StyleSelector
    {
        public Style NormalStyle { get; set; }
        public Style GreenStyle { get; set; }
        public Style YelloStyle { get; set; }
        public Style RedStyle { get; set; }

        public override System.Windows.Style SelectStyle(object item, System.Windows.DependencyObject container)
        {
            DeviceStatusHistoryForGrid statusHistoryForGrid = item as DeviceStatusHistoryForGrid;

            switch (statusHistoryForGrid.StatusColor)
            {
                    case StatusColor.Green:
                    return this.GreenStyle;
                    
                    case StatusColor.Yellow:
                    return this.YelloStyle;
                    
                    case StatusColor.Red:
                    return this.RedStyle;
            }

            return NormalStyle;
        }
    }
}

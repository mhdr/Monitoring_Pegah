using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Monitoring.Lib
{
    public delegate void ChartDateChangedEventHandler(object sender, ChartDateChangedEventArgs e);

    public delegate void CheckUpdateCompletedEventHandler(object sender, CheckUpdateCompletedEventArgs e);
}

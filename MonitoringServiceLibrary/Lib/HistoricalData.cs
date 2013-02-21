using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharedLibrary;
using TarikhFA;

namespace MonitoringServiceLibrary
{
    public partial class HistoricalData
    {
        public string PersianString
        {
            get { return this.Date.ToPersianString(); }
        }
    }
}

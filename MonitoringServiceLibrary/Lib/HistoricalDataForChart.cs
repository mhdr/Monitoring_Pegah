using System.Runtime.Serialization;

namespace MonitoringServiceLibrary.Lib
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    [DataContract]
    public class HistoricalDataForChart
    {
        [DataMember]
        public DateTime Date { get; set; }

        //public DateTime PersianDate { get; set; }

        [DataMember]
        public double Value { get; set; }

        public HistoricalDataForChart(double value,DateTime date)
        {
            this.Value = value;
            this.Date = date;
        }
    }
}

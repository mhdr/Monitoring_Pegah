using System.Runtime.Serialization;
using SharedLibrary;

namespace MonitoringServiceLibrary.Lib
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    [DataContract]
    public class HistoricalDataForGrid
    {
        [DataMember]
        public DateTime Date { get; set; }

        //public DateTime PersianDate { get; set; }

        [DataMember]
        public double Value { get; set; }

        [DataMember]
        public string Title { get; set; }

        [DataMember]
        public string PersianDateString { get; set; }

        public HistoricalDataForGrid(double value, DateTime date, string title)
        {
            this.Value = value;
            this.Date = date;
            this.Title = title;
            this.PersianDateString = this.Date.ToPersianString();
        }
    }
}

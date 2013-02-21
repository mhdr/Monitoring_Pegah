using System.Runtime.Serialization;

namespace DataAccessLayer.Lib
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class HistoricalDataForChart
    {
        public DateTime Date { get; set; }

        //public DateTime PersianDate { get; set; }

        public double Value { get; set; }

        public HistoricalDataForChart(double value,DateTime date)
        {
            this.Value = value;
            this.Date = date;
        }
    }
}

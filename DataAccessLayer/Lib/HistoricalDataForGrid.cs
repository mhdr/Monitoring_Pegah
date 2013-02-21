using System.Runtime.Serialization;
using SharedLibrary;

namespace DataAccessLayer.Lib
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class HistoricalDataForGrid
    {
        public DateTime Date { get; set; }

        //public DateTime PersianDate { get; set; }

        public double Value { get; set; }

        public string Title { get; set; }

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

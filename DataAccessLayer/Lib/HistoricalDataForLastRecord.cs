using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace DataAccessLayer.Lib
{
    [DataContract]
    public class HistoricalDataForLastRecord
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public double Value { get; set; }

        public HistoricalDataForLastRecord()
        {
            
        }

        public HistoricalDataForLastRecord(int id,double value,DateTime date)
        {
            this.Id = id;
            this.Value = value;
            this.Date = date;
        }
    }
}

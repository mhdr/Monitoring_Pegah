using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace MonitoringServiceLibrary.Lib
{
    [DataContract]
    public class HistoricalDataForLastRecord
    {

        [DataMember]    
        public int Id { get; set; }

        [DataMember]
        public DateTime Date { get; set; }

        [DataMember]
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

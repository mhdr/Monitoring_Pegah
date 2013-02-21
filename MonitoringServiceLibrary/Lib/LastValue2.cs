using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace MonitoringServiceLibrary.Lib
{
    [DataContract]
    public class LastValue2
    {
        public LastValue2()
        {
            
        }

        public LastValue2(bool hasError)
        {
            this.HasError = hasError;
        }

        public LastValue2(int id,double value,DateTime date,string persianDate,bool hasError)
        {
            this.Id = id;
            this.Value = value;
            this.Date = date;
            this.PersianDateString = persianDate;
            this.HasError = hasError;
        }

        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public double? Value { get; set; }

        [DataMember]
        public DateTime? Date { get; set; }

        [DataMember]
        public string PersianDateString { get; set; }

        [DataMember]
        public bool HasError { get; set; }
    }
}

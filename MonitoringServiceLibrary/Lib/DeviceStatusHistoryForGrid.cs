using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using SharedLibrary;

namespace MonitoringServiceLibrary.Lib
{
    [DataContract]
    public class DeviceStatusHistoryForGrid
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string IPAddress { get; set; }

        [DataMember]
        public string Status { get; set; }

        [DataMember]
        public string Description { get; set; }


        private StatusColor _statusColor;

        [DataMember]
        public string StatusColorString { get; set; }

        [DataMember]
        public StatusColor StatusColor
        {
            get { return _statusColor; }
            set
            {
                _statusColor = value;

                switch (this.StatusColor)
                {
                    case StatusColor.Green:
                        this.StatusColorString = "سبز";
                        break;
                    case StatusColor.Yellow:
                        this.StatusColorString = "زرد";
                        break;
                    case StatusColor.Red:
                        this.StatusColorString = "قرمز";
                        break;
                }
            }
        }


        private DateTime _date;

        [DataMember]
        public string PersianDateString { get; set; }

        [DataMember]
        public DateTime Date
        {
            get { return _date; }
            set
            {
                _date = value;
                this.PersianDateString = this.Date.ToPersianString();
            }
        }

        public DeviceStatusHistoryForGrid()
        {

        }

        public DeviceStatusHistoryForGrid(int id, string ip, string status, string description, StatusColor color, DateTime date)
        {
            this.Id = id;
            this.IPAddress = ip;
            this.Status = status;
            this.Description = description;
            this.StatusColor = color;
            this.Date = date;
        }
    }
}

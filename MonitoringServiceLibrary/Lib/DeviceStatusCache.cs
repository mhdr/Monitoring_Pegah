using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace MonitoringServiceLibrary.Lib
{
    [DataContract]
    public class DeviceStatusCache
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string Status { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public StatusColor Color { get; set; }

        [DataMember]
        public string IPAddress { get; set; }

        public DeviceStatusCache(int id, string status, string description, StatusColor color, string ip)
        {
            this.Id = id;
            this.Status = status;
            this.Description = description;
            this.Color = color;
            this.IPAddress = ip;
        }
    }
}

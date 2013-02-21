using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharedLibraryMonitoringDevice
{
    public class DeviceStatusCache
    {
        public int Id { get; set; }        
        public string Status { get; set; }
        public string Description { get; set; }
        public StatusColor Color { get; set; }
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

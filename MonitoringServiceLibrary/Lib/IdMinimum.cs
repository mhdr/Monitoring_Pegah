using System.Runtime.Serialization;

namespace MonitoringServiceLibrary.Lib
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    [DataContract]
    public class IdMinimum
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string Title { get; set; }

        public IdMinimum(int id, string title)
        {
            this.Id = id;
            this.Title = title;
        }
    }
}

using System.Runtime.Serialization;

namespace MonitoringServiceLibrary.Lib
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    [DataContract]
    public class Id2
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string Title { get; set; }

        [DataMember]
        public string IP { get; set; }

        [DataMember]
        public int ModuleNumber { get; set; }

        [DataMember]
        public int ModuleType { get; set; }

        [DataMember]
        public int PDIN { get; set; }
    }
}

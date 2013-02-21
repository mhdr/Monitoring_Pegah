using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace MonitoringServiceLibrary.Lib
{
    [DataContract]
    public enum StatusColor
    {
        [EnumMember]
        Green=1,

        [EnumMember]
        Yellow=2,

        [EnumMember]
        Red=3,
    }
}

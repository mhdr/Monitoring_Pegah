using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace MonitoringServiceLibrary.Lib
{
    [DataContract]
    public enum EnableCalibrationResult
    {
        [EnumMember]
        Successful = 1,

        [EnumMember]
        Failed = 2,

        [EnumMember]
        AlreadyEnabledForThisId = 3
    }
}

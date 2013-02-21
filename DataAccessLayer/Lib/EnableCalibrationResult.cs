using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace DataAccessLayer.Lib
{
    public enum EnableCalibrationResult
    {
        Successful = 1,
        Failed = 2,        
        AlreadyEnabledForThisId = 3
    }
}

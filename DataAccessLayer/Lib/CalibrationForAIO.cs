using System.Runtime.Serialization;
using SharedLibrary;

namespace MonitoringServiceLibrary.Lib
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class CalibrationForAIO
    {
        public Double CalibrationValue { get; set; }

        public CalibrationOperation CalibrationOperation { get; set; }

        public CalibrationForAIO(Double calibrationValue, int calibrationOperation)
        {
            this.CalibrationValue = calibrationValue;
            this.CalibrationOperation = (CalibrationOperation)calibrationOperation;
        }
    }
}

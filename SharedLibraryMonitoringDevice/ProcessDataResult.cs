using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharedLibraryMonitoringDevice
{
    public class ProcessDataResult
    {
        public double Value { get; set; }
        public bool HasError { get; set; }

        public ProcessDataResult()
        {
            
        }

        public ProcessDataResult(double value,bool hasError)
        {
            this.Value = value;
            this.HasError = hasError;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Monitoring.Lib
{
    public class ChartDateChangedEventArgs : EventArgs
    {
        private DateTime _startTime;
        private DateTime _endTime;

        public ChartDateChangedEventArgs(DateTime startTime,DateTime endTime)
        {
            this.StartTime = startTime;
            this.EndTime = endTime;
        }

        public DateTime StartTime
        {
            get { return _startTime; }
            set { _startTime = value; }
        }

        public DateTime EndTime
        {
            get { return _endTime; }
            set { _endTime = value; }
        }
    }
}

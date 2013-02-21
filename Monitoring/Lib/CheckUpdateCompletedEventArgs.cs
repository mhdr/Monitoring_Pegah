using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Monitoring.Lib
{
    public class CheckUpdateCompletedEventArgs:EventArgs
    {
        private bool _result;

        public bool Result
        {
            get { return _result; }
            set { _result = value; }
        }

        public CheckUpdateCompletedEventArgs(bool result)
        {
            this.Result = result;
        }
    }
}

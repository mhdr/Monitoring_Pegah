using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using SharedLibrary;

namespace DataAccessLayer.Lib
{
    public class Calibration2
    {
        private int _calibrationId;
        private int _idId;
        private CalibrationOperation _calibrationOperation;
        private double _calibrationValue;
        private bool _isEnabled;
        private string _title;
        private string _isEnabledString;
        private string _calibrationOperationString;
        private DateTime _date;
        private string _persianDateString;

        public int CalibrationId
        {
            get { return _calibrationId; }
            set { _calibrationId = value; }
        }

        public int IdId
        {
            get { return _idId; }
            set { _idId = value; }
        }

        public CalibrationOperation CalibrationOperation
        {
            get { return _calibrationOperation; }
            set { _calibrationOperation = value; }
        }

        public double CalibrationValue
        {
            get { return _calibrationValue; }
            set { _calibrationValue = value; }
        }

        public bool IsEnabled
        {
            get { return _isEnabled; }
            set { _isEnabled = value; }
        }

        public string Title
        {
            get { return _title; }
            set { _title = value; }
        }

        public string IsEnabledString
        {
            get { return _isEnabledString; }
            set { _isEnabledString = value; }
        }

        public string CalibrationOperationString
        {
            get { return _calibrationOperationString; }
            set { _calibrationOperationString = value; }
        }

        public DateTime Date
        {
            get { return _date; }
            set { _date = value; }
        }

        public string PersianDateString
        {
            get { return _persianDateString; }
            set { _persianDateString = value; }
        }
    }
}

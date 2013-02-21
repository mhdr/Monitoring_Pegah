using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MonitoringServiceLibrary;
using MonitoringServiceLibrary.Lib;
using SharedLibrary;
using SharedLibraryMonitoringDevice;
using NLog;

namespace MonitoringCollectDataConsole
{
    public class MonitoringAIOServer
    {
        private Timer timer = null;
        private TimeSpan _timerInterval = new TimeSpan(0, 0, 0, 5);
        private TimeSpan _maxTimeInterval = new TimeSpan(0, 0, 30, 0);
        private Double _calibration;
        private CalibrationOperation _calibrationOperation;
        private Double _value;
        private IMonitoringService channel = null;
        private static Logger logger = null;

        private bool IsCalibrationFetched { get; set; }

        public int Id { get; set; }

        public string IPAddress { get; set; }

        public int ILModuleNumber { get; set; }

        public int PDInWord { get; set; }

        private double _differenceBetweenCurrentAndLastRecord = 1;

        public HFI_Appl18 Appl18 { get; set; }
        public HFI_Appl19 Appl19 { get; set; }

        public MonitoringAIOServer(int id, HFI_Appl19 hfi_appl)
        {
            this.Id = id;
            this.Appl19 = hfi_appl;
            Initialize();
        }

        public MonitoringAIOServer(int id, HFI_Appl18 hfi_appl)
        {
            this.Id = id;
            this.Appl18 = hfi_appl;
            Initialize();
        }

        private void Initialize()
        {
            logger = LogManager.GetCurrentClassLogger();

            this.InitializeChannel();

#if DEBUG
            InitializeMonitoringParameters();
#endif

            GetCalibrationAsync();
            GetReadIntervalAsync();
            GetMaxIntervalAsync();

            Start();
        }

        public void Start()
        {
            timer = new Timer(timer_ticks, "MHDR", 0, this.TimerInterval.Seconds * 1000);
        }

        public void Stop()
        {
            timer.Dispose();
        }

        private void InitializeChannel()
        {
            this.channel = Lib.Statics.Channel;
        }

        private void InitializeMonitoringParameters()
        {
            Id2 currentId = null;

            try
            {
                currentId = channel.GetId(this.Id);
            }
            catch (Exception ex)
            {
                logger.LogException(LogLevel.Info, string.Format("MonitoringAIOServer.InitializeMonitoringParameters.{0}", this.Id), ex);
                return;
            }

            if (currentId == null)
            {
                return;
            }

#if DEBUG
            if (currentId.IP == "192.168.17.18")
            {
                this.IPAddress = "localhost/monitoring/17.18";
            }
            else if (currentId.IP == "192.168.17.19")
            {
                this.IPAddress = "localhost/monitoring/17.19";
            }
#endif

#if RELEASE
                    this.IPAddress = currentId.IP;
#endif

            this.ILModuleNumber = currentId.ModuleNumber;
            this.PDInWord = currentId.PDIN;
        }

        public TimeSpan TimerInterval
        {
            get { return _timerInterval; }
            set
            {
                _timerInterval = value;
                timer.Change(0, this.TimerInterval.Seconds * 1000);
            }
        }

        public double Calibration
        {
            get { return _calibration; }
            set { _calibration = value; }
        }

        public CalibrationOperation CalibrationOperation
        {
            get { return _calibrationOperation; }
            set { _calibrationOperation = value; }
        }

        public double Value
        {
            get { return _value; }
            set { _value = Math.Round(value, 2); }
        }

        public double DifferenceBetweenCurrentAndLastRecord
        {
            get { return _differenceBetweenCurrentAndLastRecord; }
            set { _differenceBetweenCurrentAndLastRecord = value; }
        }

        public TimeSpan MaxTimeInterval
        {
            get { return _maxTimeInterval; }
            set { _maxTimeInterval = value; }
        }

        private void timer_ticks(object state)
        {
            lock (state)
            {
                ReadValueFromDevice();
            }
        }

        private void ReadValueFromDeviceAsync()
        {
            Task.Factory.StartNew(ReadValueFromDevice);
        }

        private void ReadValueFromDevice()
        {
            if (IsCalibrationFetched == false)
            {
                return;
            }

            ProcessDataResult processDataResult = null;


#if RELEASE
            
            if (Appl18 != null)
            {
                processDataResult = Appl18.GetInputValue(this.Id);
            }
            else if (Appl19 != null)
            {
                processDataResult = Appl19.GetInputValue(this.Id);
            } 
#endif

#if DEBUG
            processDataResult = ProcessData.GetData(IPAddress, ILModuleNumber, PDInWord);
#endif

            if (processDataResult.HasError)
            {
                return;
            }
            double temp = (float)processDataResult.Value;

            if (this.Calibration != 0)
            {
                if (this.CalibrationOperation == CalibrationOperation.Jam)
                {
                    temp += this.Calibration;
                }
                else if (this.CalibrationOperation == CalibrationOperation.Zarb)
                {
                    temp *= this.Calibration;
                }
            }

            this.Value = temp;

            try
            {
                channel.AddNewLastValue(this.Id, this.Value);
            }
            catch (System.Exception ex)
            {
                logger.LogException(LogLevel.Info, string.Format("MonitoringAIOServer.ReadValueFromDevice.{0}", this.Id), ex);
                return;
            }

            HistoricalDataForLastRecord lastRecord = null;

            try
            {
                lastRecord = channel.GetLastRecord(this.Id);
            }
            catch (System.Exception ex)
            {
                logger.LogException(LogLevel.Info, string.Format("MonitoringAIOServer.ReadValueFromDevice.{0}", this.Id), ex);
                return;
            }

            if (lastRecord == null)
            {
                SaveValueInDatabase();
            }
            else
            {

                TimeSpan timeSpanDiff = DateTime.Now - lastRecord.Date;

                if (timeSpanDiff > MaxTimeInterval)
                {
                    SaveValueInDatabase();
                    return;
                }

                double lastRecordvalue = lastRecord.Value;

                var diff = Math.Abs(lastRecordvalue - this.Value);

                if (diff < this.DifferenceBetweenCurrentAndLastRecord)
                {
                    return;
                }

                SaveValueInDatabase();
            }

        }

        private void SaveValueInDatabase()
        {
            try
            {
                channel.AddNewHistoricalData(this.Id, this.Value);
            }
            catch (System.Exception ex)
            {
                logger.LogException(LogLevel.Info, string.Format("MonitoringAIOServer.SaveValueInDatabase.{0}", this.Id), ex);
                return;
            }
        }

        private void GetReadIntervalAsync()
        {
            Task.Factory.StartNew(GetReadInterval);
        }

        private void GetReadInterval()
        {
            int readInterval = 0;
            try
            {
                readInterval = channel.GetReadInterval(this.Id);
            }
            catch (System.Exception ex)
            {
                logger.LogException(LogLevel.Info, string.Format("MonitoringAIOServer.GetReadInterval.{0}", this.Id), ex);
                return;
            }

            if (readInterval == 0)
            {
                return;
            }

            this.TimerInterval = new TimeSpan(0, 0, 0, readInterval);
        }

        private void GetMaxIntervalAsync()
        {
            Task.Factory.StartNew(GetMaxInterval);
        }

        private void GetMaxInterval()
        {
            int readIntervalMax = 0;

            try
            {
                readIntervalMax = channel.GetMaxInterval(this.Id);
            }
            catch (System.Exception ex)
            {
                logger.LogException(LogLevel.Info, string.Format("MonitoringAIOServer.GetMaxInterval.{0}", this.Id), ex);
                return;
            }

            if (readIntervalMax == 0)
            {
                return;
            }

            this.MaxTimeInterval = new TimeSpan(0, 0, readIntervalMax, 0);
        }

        private void GetCalibrationAsync()
        {
            Task.Factory.StartNew(GetCalibration);
        }

        private void GetCalibration()
        {
            CalibrationForAIO calibrationForAio = null;

            try
            {
                calibrationForAio = channel.GetCalibrationForAIO(this.Id);
            }
            catch (System.Exception ex)
            {
                logger.LogException(LogLevel.Info, string.Format("MonitoringAIOServer.GetCalibration.{0}", this.Id), ex);
                return;
            }

            if (calibrationForAio == null)
            {
                IsCalibrationFetched = true;
                return;
            }

            this.Calibration = calibrationForAio.CalibrationValue;
            this.CalibrationOperation = (CalibrationOperation)calibrationForAio.CalibrationOperation;

            IsCalibrationFetched = true;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using MonitoringServiceLibrary;
using MonitoringServiceLibrary.Lib;
using NLog;
using SharedLibraryMonitoringDevice;

namespace MonitoringCollectDataConsole
{
    public class SaveDeviceStatus
    {
        private Timer timer;
        private IMonitoringService channel = null;
        public string IPAddress { get; set; }
        private Logger logger = null;
        public HFI_Appl18 Appl18 { get; set; }
        public HFI_Appl19 Appl19 { get; set; }

        public SaveDeviceStatus(HFI_Appl19 hfi_appl,string ip)
        {
            this.Appl19 = hfi_appl;
            this.IPAddress = ip;
            this.InitializeChannel();
            logger = LogManager.GetCurrentClassLogger();
        }

        public SaveDeviceStatus(HFI_Appl18 hfi_appl,string ip)
        {
            this.Appl18 = hfi_appl;
            this.IPAddress = ip;
            this.InitializeChannel();
            logger = LogManager.GetCurrentClassLogger();
        }

        public TimeSpan MaxTimeInterval
        {
            get { return _maxTimeInterval; }
            set { _maxTimeInterval = value; }
        }

        private TimeSpan _maxTimeInterval = new TimeSpan(0, 0, 30, 0);

        public SaveDeviceStatus(string ip)
        {
            this.InitializeChannel();
            this.IPAddress = ip;
        }

        public void Start()
        {
            timer = new Timer(timer_ticks, "MHDR", 0, 5000);
        }

        public void Stop()
        {
            timer.Dispose();
        }

        private void InitializeChannel()
        {
            this.channel = Lib.Statics.Channel;
        }

        private void timer_ticks(object state)
        {
            this.Stop();
            CheckDeviceStatus();
            this.Start();
        }

        public void CheckDeviceStatus()
        {
            SharedLibraryMonitoringDevice.DeviceStatusCache result = null;

            try
            {
                //result = channel.GetDeviceStatus(IPAddress);

                if (Appl18 != null)
                {
                    result = Appl18.GetDeviceStatus();
                }
                else if (Appl19 != null)
                {
                    result = Appl19.GetDeviceStatus();
                } 
            }
            catch (Exception ex)
            {
                logger.LogException(LogLevel.Info, string.Format("SaveDeviceStatus.CheckDeviceStatus.{0}", this.IPAddress), ex);
                return;
            }

            if (result == null)
            {
                return;
            }

            DeviceStatusHistory lastRecord = null;

            try
            {
                lastRecord = channel.GetLastDeviceStatusHistory(IPAddress);
            }
            catch (Exception ex)
            {
                logger.LogException(LogLevel.Info, string.Format("SaveDeviceStatus.CheckDeviceStatus.{0}", this.IPAddress), ex);
                //return;
            }

            if (lastRecord == null)
            {
                try
                {
                    channel.AddNewDeviceStatusHistory(result.Id, IPAddress);
                }
                catch (Exception ex)
                {
                    logger.LogException(LogLevel.Info, string.Format("SaveDeviceStatus.CheckDeviceStatus.{0}", this.IPAddress), ex);
                }
                SaveValueInDeviceStatusLastRecord(result.Id);
                return;
            }

            //TimeSpan timeSpanDiff = DateTime.Now - lastRecord.Date;

            //if (timeSpanDiff > MaxTimeInterval)
            //{
            //    try
            //    {
            //        channel.AddNewDeviceStatusHistory(result.Id, IPAddress);
            //    }
            //    catch (System.Exception ex)
            //    {
            //        //TODO Exception
            //    }
            //    SaveValueInDeviceStatusLastRecord(result.Id);
            //    return;
            //}

            if (result.Id != lastRecord.StatusId)
            {
                try
                {
                    channel.AddNewDeviceStatusHistory(result.Id, IPAddress);
                }
                catch (Exception ex)
                {
                    logger.LogException(LogLevel.Info, string.Format("SaveDeviceStatus.CheckDeviceStatus.{0}", this.IPAddress), ex);
                }

                SaveValueInDeviceStatusLastRecord(result.Id);
                return;
            }

            SaveValueInDeviceStatusLastRecord(result.Id);
        }

        private void SaveValueInDeviceStatusLastRecord(int id)
        {
            if (id <= 0)
            {
                return;
            }

#if DEBUG
            if (this.IPAddress == "localhost/monitoring/17.18")
            {
                try
                {
                    channel.AddNewDeviceStatusLastRecord("192.168.17.18", id);
                }
                catch (Exception ex)
                {
                    logger.LogException(LogLevel.Info, string.Format("SaveDeviceStatus.SaveValueInDeviceStatusLastRecord.{0}", this.IPAddress), ex);
                    return;
                }
            }

            if (this.IPAddress == "localhost/monitoring/17.19")
            {
                try
                {
                    channel.AddNewDeviceStatusLastRecord("192.168.17.19", id);
                }
                catch (Exception ex)
                {
                    logger.LogException(LogLevel.Info, string.Format("SaveDeviceStatus.SaveValueInDeviceStatusLastRecord.{0}", this.IPAddress), ex);
                    return;
                }
            }
#endif

#if RELEASE
            try
            {
                channel.AddNewDeviceStatusLastRecord(IPAddress, id);
            }
            catch (Exception ex)
            {
                logger.LogException(LogLevel.Info, string.Format("SaveDeviceStatus.SaveValueInDeviceStatusLastRecord.{0}", this.IPAddress), ex);
            }
#endif

        }
    }
}

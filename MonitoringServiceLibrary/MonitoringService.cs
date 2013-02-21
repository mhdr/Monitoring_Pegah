using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using HtmlAgilityPack;
using MonitoringServiceLibrary.Lib;
using SharedLibrary;
using NLog;

namespace MonitoringServiceLibrary
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    public class MonitoringService : IMonitoringService
    {
        private MonitoringEntities _entities = new MonitoringEntities();
        private Logger logger = null;


        public MonitoringService()
        {
            logger = LogManager.GetCurrentClassLogger();
        }

        public MonitoringEntities Entities
        {
            get { return _entities; }
            set { _entities = value; }
        }

        public List<HistoricalDataForChart> GetHistoricalDataForChart(int id, DateTime startTime, DateTime endTime)
        {

            var historyQuery = from data in Entities.HistoricalDatas
                               where data.IdId.Equals(id) & data.Date >= startTime & data.Date <= endTime
                               select data;

            var result = new List<HistoricalDataForChart>();

            foreach (var historicalData in historyQuery)
            {
                result.Add(new HistoricalDataForChart(historicalData.Value, historicalData.Date));
            }

            return result;
        }

        public List<HistoricalDataForGrid> GetHistoricalDataForGrid(int id, DateTime startTime, DateTime endTime)
        {
            var historyQuery = from data in Entities.HistoricalDatas
                               where data.IdId.Equals(id) & data.Date >= startTime & data.Date <= endTime
                               select data;

            var idQuery = from id1 in Entities.Ids
                          where id1.Id1 == id
                          select id1;

            string title = idQuery.FirstOrDefault().Title;

            var result = new List<HistoricalDataForGrid>();

            foreach (var historicalData in historyQuery)
            {
                result.Add(new HistoricalDataForGrid(historicalData.Value, historicalData.Date, title));
            }

            return result;
        }

        public HistoricalDataForLastRecord GetLastRecord(int id)
        {
            var lastRecordQuery = from data in Entities.HistoricalDatas
                                  where data.IdId.Equals(id)
                                  orderby data.Date descending
                                  select data;

            HistoricalData lastRecordTemp = lastRecordQuery.FirstOrDefault();

            if (lastRecordTemp != null)
            {
                HistoricalDataForLastRecord lastRecord = new HistoricalDataForLastRecord(lastRecordTemp.HistocalDataId, lastRecordTemp.Value, lastRecordTemp.Date);

                return lastRecord;
            }

            return null;
        }

        public bool AddNewHistoricalData(int id, double value)
        {
            HistoricalData newHistoricalData = new HistoricalData();
            newHistoricalData.IdId = id;
            newHistoricalData.Value = value;
            newHistoricalData.Date = DateTime.Now;

            Entities.HistoricalDatas.AddObject(newHistoricalData);

            if (Entities.SaveChanges() > 0)
            {
                return true;
            }

            return false;
        }

        public Id2 GetId(int id)
        {
            Id currentId = Entities.Ids.FirstOrDefault(x => x.Id1 == id);

            Id2 idX = new Id2();
            idX.Id = currentId.Id1;
            idX.IP = currentId.IP;
            idX.ModuleNumber = currentId.ModuleNumber;
            idX.ModuleType = currentId.ModuleType;
            idX.PDIN = currentId.PDIN;
            idX.Title = currentId.Title;

            return idX;
        }

        public CalibrationForAIO GetCalibrationForAIO(int id)
        {
            var calibrationQuery = from calibration in Entities.Calibrations
                                   where calibration.IdId == id & calibration.IsEnabled == true
                                   select calibration;

            if (!calibrationQuery.Any())
            {
                return null;
            }

            var result = calibrationQuery.FirstOrDefault();
            CalibrationForAIO calibrationForAio = new CalibrationForAIO(result.CalibrationValue, result.CalibrationOperation);

            return calibrationForAio;
        }

        public List<Calibration2> GetCalibrations(int id)
        {
            var calibrationQuery = Entities.Calibrations.Where(x => x.IdId == id);

            List<Calibration2> result = new List<Calibration2>();

            foreach (var calibration in calibrationQuery)
            {
                Calibration2 calibrationX = new Calibration2();
                calibrationX.CalibrationId = calibration.CalibrationId;
                calibrationX.IdId = calibration.IdId;
                calibrationX.CalibrationOperation = (CalibrationOperation)calibration.CalibrationOperation;
                calibrationX.CalibrationValue = calibration.CalibrationValue;
                calibrationX.IsEnabled = calibration.IsEnabled;
                calibrationX.Title = calibration.Id.Title;
                calibrationX.Date = (DateTime)calibration.Date;
                calibrationX.PersianDateString = calibration.Date.ToPersianString();

                if (calibration.IsEnabled)
                {
                    calibrationX.IsEnabledString = "فعال";
                }
                else
                {
                    calibrationX.IsEnabledString = "غیر فعال";
                }

                if (calibration.CalibrationOperation == (decimal)CalibrationOperation.Jam)
                {
                    calibrationX.CalibrationOperationString = "جمع";
                }
                else if (calibration.CalibrationOperation == (decimal)CalibrationOperation.Zarb)
                {
                    calibrationX.CalibrationOperationString = "ضرب";
                }

                result.Add(calibrationX);
            }

            return result;
        }

        public List<Calibration2> GetCalibrations()
        {
            var calibrationQuery = Entities.Calibrations;

            List<Calibration2> result = new List<Calibration2>();

            foreach (var calibration in calibrationQuery)
            {
                Calibration2 calibrationX = new Calibration2();
                calibrationX.CalibrationId = calibration.CalibrationId;
                calibrationX.IdId = calibration.IdId;
                calibrationX.CalibrationOperation = (CalibrationOperation)calibration.CalibrationOperation;
                calibrationX.CalibrationValue = calibration.CalibrationValue;
                calibrationX.IsEnabled = calibration.IsEnabled;
                calibrationX.Title = calibration.Id.Title;
                calibrationX.Date = (DateTime)calibration.Date;
                calibrationX.PersianDateString = calibration.Date.ToPersianString();

                if (calibration.IsEnabled)
                {
                    calibrationX.IsEnabledString = "فعال";
                }
                else
                {
                    calibrationX.IsEnabledString = "غیر فعال";
                }

                if (calibration.CalibrationOperation == (decimal)CalibrationOperation.Jam)
                {
                    calibrationX.CalibrationOperationString = "جمع";
                }
                else if (calibration.CalibrationOperation == (decimal)CalibrationOperation.Zarb)
                {
                    calibrationX.CalibrationOperationString = "ضرب";
                }

                result.Add(calibrationX);
            }

            return result;
        }

        public bool AddNewCalibration(int id, CalibrationOperation calibrationOperation, double value)
        {
            Calibration calibration = new Calibration();
            calibration.IdId = id;
            calibration.CalibrationOperation = (int)calibrationOperation;
            calibration.CalibrationValue = value;
            calibration.IsEnabled = false;
            calibration.Date = DateTime.Now;
            Entities.Calibrations.AddObject(calibration);

            if (Entities.SaveChanges() > 0)
            {
                return true;
            }

            return false;
        }

        public bool DeleteCalibration(int id)
        {
            try
            {
                Calibration calibration = Entities.Calibrations.SingleOrDefault(x => x.CalibrationId == id);
                Entities.Calibrations.DeleteObject(calibration);

                if (Entities.SaveChanges() > 0)
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }

            return false;
        }

        public bool DisableCalibration(int id)
        {
            try
            {
                Calibration calibration = Entities.Calibrations.SingleOrDefault(x => x.CalibrationId == id);
                calibration.IsEnabled = false;
                if (Entities.SaveChanges() > 0)
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }

            return false;
        }

        public EnableCalibrationResult EnableCalibration(int id)
        {
            try
            {
                Calibration calibration = Entities.Calibrations.SingleOrDefault(x => x.CalibrationId == id);
                int idid = calibration.IdId;

                var calibrations = Entities.Calibrations.Where(x => x.IdId == idid && x.IsEnabled == true);
                if (calibrations.Any())
                {
                    return EnableCalibrationResult.AlreadyEnabledForThisId;
                }

                calibration.IsEnabled = true;
                if (Entities.SaveChanges() > 0)
                {
                    return EnableCalibrationResult.Successful;
                }
            }
            catch (Exception ex)
            {
                return EnableCalibrationResult.Failed;
            }

            return EnableCalibrationResult.Failed;
        }

        public Int32 GetReadInterval(int id)
        {
            var readIntervalQuery = from readInterval in Entities.ReadIntervals
                                    where readInterval.IdId == id & readInterval.IsEnabled == true
                                    select readInterval;

            if (!readIntervalQuery.Any())
            {
                return 0;
            }

            return readIntervalQuery.FirstOrDefault().Interval;
        }

        public int GetMaxInterval(int id)
        {
            var readIntervalQuery = from readInterval in Entities.ReadIntervals
                                    where readInterval.IdId == id & readInterval.IsEnabled == true
                                    select readInterval;

            if (!readIntervalQuery.Any())
            {
                return 0;
            }

            return readIntervalQuery.FirstOrDefault().MaxInterval;
        }

        public bool AddNewLastValue(int id, double value)
        {
            LastValue lastValue = Entities.LastValues.FirstOrDefault(x => x.Id == id);
            lastValue.Value = value;
            lastValue.Date = DateTime.Now;

            if (Entities.SaveChanges() > 0)
            {
                return true;
            }

            return false;
        }

        public LastValue2 GetLastValue(int id)
        {
            Entities = new MonitoringEntities();
            LastValue lastValue = Entities.LastValues.FirstOrDefault(x => x.Id == id);
            LastValue2 result = new LastValue2();

            if (lastValue != null)
            {
                result.Id = id;
                result.Date = lastValue.Date;

                if (lastValue.Date != null)
                {
                    result.PersianDateString = ((DateTime)lastValue.Date).ToPersianString();
                }

                result.Value = lastValue.Value;

                result.HasError = false;
            }

            return result;
        }

        public List<IdMinimum> GetIdsMinimum()
        {
            var idQuery = Entities.Ids;

            var result = new List<IdMinimum>();

            foreach (var id in idQuery)
            {
                result.Add(new IdMinimum(id.Id1, id.Title));
            }

            return result;
        }

        public List<Id2> GetIds()
        {
            var idQuery = Entities.Ids;

            var result = new List<Id2>();

            foreach (var id in idQuery)
            {
                Id2 current = new Id2();
                current.Id = id.Id1;
                current.IP = id.IP;
                current.ModuleNumber = id.ModuleNumber;
                current.ModuleType = id.ModuleType;
                current.PDIN = id.PDIN;
                current.Title = id.Title;

                result.Add(current);
            }

            return result;
        }

//        public DeviceStatusCache GetDeviceStatus(string IPAddress)
//        {
//            string address = string.Format("http://{0}/remote_diagnostics.htm", IPAddress);

//            WebClient client = new WebClient();
//            HtmlDocument htmlDocument = new HtmlDocument();

//            try
//            {
//                htmlDocument.LoadHtml(client.DownloadString(address));
//            }
//            catch (System.Net.WebException ex)
//            {
//                logger.LogException(LogLevel.Info, string.Format("MonitoringService.GetDeviceStatus"), ex);

//                return null;
//            }

//            //HtmlDocument htmlDocument = new HtmlWeb().Load(address);

//            if (htmlDocument.DocumentNode == null)
//            {
//                return null;
//            }

//            HtmlNode StatusNode = null;

//#if DEBUG
//            StatusNode =
//             htmlDocument.DocumentNode.SelectSingleNode(
//                 "/html/body/table/tbody/tr/td[2]/table[2]/tbody/tr[2]/td[2]/table/tbody/tr/td/div/font/b/font");
//#endif

//#if RELEASE
//                StatusNode = htmlDocument.DocumentNode.SelectSingleNode(
//                    "/html/body/table/tr/td[2]/table[2]/tr[2]/td[2]/table/tr/td/div/font/b/font");
//#endif
//            string StatusString = StatusNode.InnerText.Replace("&nbsp;", "").Replace(" ", "").Trim();

//            var deviceStatusQuery = Entities.DeviceStatus;
//            DeviceStatu match = deviceStatusQuery.FirstOrDefault(x => x.Status == StatusString);
//            DeviceStatusCache result = new DeviceStatusCache(match.StatusId, match.Status, match.Description, (StatusColor)match.StatusColor, IPAddress);

//            return result;
//        }

        public bool AddNewDeviceStatusHistory(int id, string ip)
        {
            DeviceStatusHistory deviceStatusHistory = new DeviceStatusHistory();
            deviceStatusHistory.IPAddress = ip;
            deviceStatusHistory.StatusId = id;
            deviceStatusHistory.Date = DateTime.Now;
            Entities.DeviceStatusHistories.AddObject(deviceStatusHistory);

            if (Entities.SaveChanges() > 0)
            {
                return true;
            }

            return false;
        }

        public DeviceStatusHistory GetLastDeviceStatusHistory(string ip)
        {
            var lastRecordQuery = from data in Entities.DeviceStatusHistories
                                  where data.IPAddress == ip
                                  orderby data.Date descending
                                  select data;

            DeviceStatusHistory lastRecordTemp = lastRecordQuery.FirstOrDefault();

            return lastRecordTemp;
        }

        public List<DeviceStatusHistoryForGrid> GetDeviceStatusHistories(DateTime startTime, DateTime endTime)
        {
            var statusQuery = from history in Entities.DeviceStatusHistories
                              where history.Date >= startTime & history.Date <= endTime
                              select history;

            List<DeviceStatusHistoryForGrid> result = new List<DeviceStatusHistoryForGrid>();

            foreach (var deviceStatusHistory in statusQuery)
            {
                DeviceStatusHistoryForGrid historyForGrid = new DeviceStatusHistoryForGrid();
                historyForGrid.Id = deviceStatusHistory.StatusHistoryId;
                historyForGrid.IPAddress = deviceStatusHistory.IPAddress;
                historyForGrid.Status = deviceStatusHistory.DeviceStatu.Status;
                historyForGrid.Description = deviceStatusHistory.DeviceStatu.Description;
                historyForGrid.StatusColor = (StatusColor)deviceStatusHistory.DeviceStatu.StatusColor;
                historyForGrid.Date = deviceStatusHistory.Date;

                result.Add(historyForGrid);
            }

            return result;
        }

        public DeviceStatusCache GetDeviceStatusHistoryLastValue(string ip)
        {
            DeviceStatusHistoryLastValue lastValue =
                Entities.DeviceStatusHistoryLastValues.FirstOrDefault(x => x.IPAddress == ip);

            DeviceStatu deviceStatu = Entities.DeviceStatus.FirstOrDefault(x => x.StatusId == lastValue.StatusId);

            DeviceStatusCache statusCache = new DeviceStatusCache(deviceStatu.StatusId, deviceStatu.Status, deviceStatu.Description,
                (StatusColor)deviceStatu.StatusColor, ip);

            return statusCache;
        }

        public bool AddNewDeviceStatusLastRecord(string ip, int statusId)
        {
            DeviceStatusHistoryLastValue lastValue =
                Entities.DeviceStatusHistoryLastValues.FirstOrDefault(x => x.IPAddress == ip);
            lastValue.StatusId = statusId;
            lastValue.Date = DateTime.Now;

            if (Entities.SaveChanges() > 0)
            {
                return true;
            }

            return false;
        }

        public bool IsUpdateAvailabe(double currentVersion)
        {
            var updateQuery = from update in Entities.Updates
                              where update.UpdateVersion > currentVersion
                              select update;

            if (updateQuery.Any())
            {
                return true;
            }


            return false;
        }
    }
}

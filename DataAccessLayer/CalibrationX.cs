using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccessLayer.Lib;
using MonitoringServiceLibrary.Lib;
using SharedLibrary;

namespace DataAccessLayer
{
    public class CalibrationX
    {
        private MonitoringEntities _entities;

        public MonitoringEntities Entities
        {
            get { return _entities; }
            set { _entities = value; }
        }

        public CalibrationX()
        {
            this.Entities=new MonitoringEntities();
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
    }
}

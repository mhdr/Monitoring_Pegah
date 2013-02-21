using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using MonitoringServiceLibrary.Lib;
using SharedLibrary;

namespace MonitoringServiceLibrary
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IMonitoringService
    {

        #region HistoricalData

        [OperationContract]
        List<HistoricalDataForChart> GetHistoricalDataForChart(int id, DateTime startTime, DateTime endTime);

        [OperationContract]
        List<HistoricalDataForGrid> GetHistoricalDataForGrid(int id, DateTime startTime, DateTime endTime);

        [OperationContract]
        HistoricalDataForLastRecord GetLastRecord(int id);

        [OperationContract]
        bool AddNewHistoricalData(int id, double value);


        #endregion

        #region Id
        [OperationContract]
        List<IdMinimum> GetIdsMinimum();

        [OperationContract]
        List<Id2> GetIds();

        [OperationContract]
        Id2 GetId(int id);

        #endregion

        #region Calibration

        [OperationContract]
        CalibrationForAIO GetCalibrationForAIO(int id);

        [OperationContract(Name = "GetCalibrationsSingle")]
        List<Calibration2> GetCalibrations(int id);

        [OperationContract(Name = "GetCalibrationsAll")]
        List<Calibration2> GetCalibrations();

        [OperationContract]
        bool AddNewCalibration(int id, CalibrationOperation calibrationOperation, double value);

        [OperationContract]
        bool DeleteCalibration(int id);

        [OperationContract]
        bool DisableCalibration(int id);

        [OperationContract]
        EnableCalibrationResult EnableCalibration(int id);

        #endregion

        #region ReadInterval

        [OperationContract]
        Int32 GetReadInterval(int id);

        [OperationContract]
        Int32 GetMaxInterval(int id);

        #endregion

        #region LastValues

        [OperationContract]
        bool AddNewLastValue(int id, double value);

        [OperationContract]
        LastValue2 GetLastValue(int id);

        #endregion

        #region DeviceStatus

        //[OperationContract]
        //DeviceStatusCache GetDeviceStatus(string IPAddress);

        #endregion

        #region DeviceStatusHistory

        [OperationContract]
        bool AddNewDeviceStatusHistory(int id, string ip);

        [OperationContract]
        DeviceStatusHistory GetLastDeviceStatusHistory(string ip);

        [OperationContract]
        List<DeviceStatusHistoryForGrid> GetDeviceStatusHistories(DateTime startTime, DateTime endTime);

        #endregion


        #region DeviceStatusHistoryLastValue

        [OperationContract]
        DeviceStatusCache GetDeviceStatusHistoryLastValue(string ip);

        [OperationContract]
        bool AddNewDeviceStatusLastRecord(string ip, int statusId);

        #endregion


        #region Update

        bool IsUpdateAvailabe(double currentVersion);

        #endregion
    }
}

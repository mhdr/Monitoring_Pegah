using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccessLayer.Lib;

namespace DataAccessLayer
{
    public class HistoricalDataX
    {
        private MonitoringEntities _entities;

        public HistoricalDataX()
        {
            this.Entities = new MonitoringEntities();
        }

        public MonitoringEntities Entities
        {
            get { return _entities; }
            set { _entities = value; }
        }

        public HistoricalData GetLastRecord(int id)
        {
            var lastRecordQuery = from data in Entities.HistoricalDatas
                                  where data.IdId.Equals(id)
                                  orderby data.Date descending
                                  select data;

            HistoricalData lastRecord = lastRecordQuery.FirstOrDefault();


            return lastRecord;
        }

        public bool AddNewHistoricalData(int id, Double value)
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
    }
}

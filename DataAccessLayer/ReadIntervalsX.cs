using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAccessLayer
{
    public class ReadIntervalsX
    {
        private MonitoringEntities _entities;

        public MonitoringEntities Entities
        {
            get { return _entities; }
            set { _entities = value; }
        }

        public ReadIntervalsX()
        {
            this.Entities=new MonitoringEntities();
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
    }
}

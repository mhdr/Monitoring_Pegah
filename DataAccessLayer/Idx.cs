using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccessLayer.Lib;

namespace DataAccessLayer
{
    public class Idx
    {
        private MonitoringEntities _entities;

        public Idx()
        {
            this.Entities=new MonitoringEntities();
        }

        public MonitoringEntities Entities
        {
            get { return _entities; }
            set { _entities = value; }
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
    }
}

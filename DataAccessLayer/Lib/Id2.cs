using System.Runtime.Serialization;

namespace DataAccessLayer.Lib
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class Id2
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string IP { get; set; }

        public int ModuleNumber { get; set; }

        public int ModuleType { get; set; }

        public int PDIN { get; set; }
    }
}

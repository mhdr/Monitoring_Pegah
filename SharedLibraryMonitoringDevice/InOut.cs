using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PhoenixContact.HFI;

namespace SharedLibraryMonitoringDevice
{
    public class InOut
    {
        public int Id { get; set; }
        public VarInput Input { get; set; }
        public VarOutput Output { get; set; }

        public InOut(int id,VarInput input,VarOutput output)
        {
            this.Id = id;
            this.Input = input;
            this.Output = output;
        }
    }
}

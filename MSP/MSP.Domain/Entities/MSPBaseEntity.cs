using System;
using System.Collections.Generic;
using System.Text;

namespace MSP.Domain.Entities
{
    public class MSPBaseEntity
    {
        public DateTime? DTBegin { get; set; }
        public DateTime? DTUpdate { get; set; }
        public DateTime? DTEnd { get; set; }
    }
}

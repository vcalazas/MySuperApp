using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using static MSP.Domain.Helpers.Constants;

namespace MSP.Domain.Entities
{
    public class MSPAuth : MSPBaseEntity
    {
        [Key]
        public int AuthId { get; set; }

        [ForeignKey("MSPPerson")]
        public int PersonId { get; set; }
        public AuthDeviceType DeviceType { get; set; }
        public string? SO { get; set; }
        public string? Manufacturer { get; set; }
        public string? Model { get; set; }
        public string? Version { get; set; }
    }
}

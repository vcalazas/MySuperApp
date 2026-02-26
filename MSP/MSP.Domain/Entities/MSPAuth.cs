using MSP.Domain.DTOs;
using MSP.Domain.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using static MSP.Domain.Helpers.Constants;

namespace MSP.Domain.Entities
{
    public class MSPAuth : MSPBaseEntity, IConvertModel<MSPAuthDTO>
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
        public string? AccessToken { get; set; }

        public MSPAuthDTO Convert()
        {
            return new MSPAuthDTO
            {
                AuthId = this.AuthId,
                PersonId = this.PersonId,
                DeviceType = this.DeviceType,
                SO = this.SO,
                Manufacturer = this.Manufacturer,
                Model = this.Model,
                Version = this.Version,
                AccessToken = this.AccessToken,
                DTBegin = this.DTBegin,
                DTUpdate = this.DTUpdate,
                DTEnd = this.DTEnd
            };
        }
    }
}

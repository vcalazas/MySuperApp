using MSP.Domain.Entities;
using MSP.Domain.Helpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using static MSP.Domain.Helpers.Constants;

namespace MSP.Domain.DTOs
{
    public class MSPAuthDTO : MSPBaseEntityDTO, IConvertModel<MSPAuth>
    {
        public int AuthId { get; set; }
        public int PersonId { get; set; }
        public AuthDeviceType DeviceType { get; set; }
        public string? SO { get; set; }
        public string? Manufacturer { get; set; }
        public string? Model { get; set; }
        public string? Version { get; set; }
        public string? AccessToken { get; set; }

        public int? ExpiresIn { get; set; }



        // Propriedades para login
        public string? PersonLogin { get; set; }
        public string? PersonPassworld { get; set; }

        // Propriedades para configuração do JWT (serão ignoradas na serialização JSON)
        [JsonIgnore] public string? JwtConfigIssuer { get; set; }
        [JsonIgnore] public string? JwtConfigAudience { get; set; }
        [JsonIgnore] public string? JwtConfigIssuerKey { get; set; }
        [JsonIgnore] public int? JwtConfigIssuerTokenValidiyMins { get; set; }

        public MSPAuth Convert()
        {
            return new MSPAuth
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

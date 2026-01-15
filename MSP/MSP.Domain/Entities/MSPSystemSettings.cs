using MSP.Domain.DTOs;
using MSP.Domain.Helpers;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Text.Json.Serialization;

namespace MSP.Domain.Entities
{
    public class MSPSystemSettings : IConvertModel<MSPSystemSettings, MSPSystemSettingsDTO>
    {
        public string SettingKey { get; set; }

        [JsonIgnore]
        public MSPSystemSettingsDTO Convert => new MSPSystemSettingsDTO()
        {

        };
    }
}

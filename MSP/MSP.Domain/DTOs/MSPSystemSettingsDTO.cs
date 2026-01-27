using MSP.Domain.Entities;
using MSP.Domain.Helpers;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Text.Json.Serialization;

namespace MSP.Domain.DTOs
{
    public class MSPSystemSettingsDTO : MSPBaseEntityDTO, IConvertModel<MSPSystemSettingsDTO, MSPSystemSettings>
    {
        public string SettingKey { get; set; }
        public string SettingValue { get; set; }


        [JsonIgnore]
        public MSPSystemSettings Convert => new MSPSystemSettings()
        {
            SettingKey = SettingKey,
            SettingValue = SettingValue,
            DTBegin = DTBegin,
            DTUpdate = DTUpdate,
            DTEnd = DTEnd
        };
    }
}

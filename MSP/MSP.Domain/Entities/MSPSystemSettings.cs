using MSP.Domain.DTOs;
using MSP.Domain.Helpers;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace MSP.Domain.Entities
{
    public class MSPSystemSettings : MSPBaseEntity, IConvertModel<MSPSystemSettings, MSPSystemSettingsDTO>
    {
        [Key]
        public string SettingKey { get; set; }
        public string SettingValue { get; set; }

        [JsonIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        public MSPSystemSettingsDTO Convert => new MSPSystemSettingsDTO()
        {
            SettingKey = SettingKey,
            SettingValue = SettingValue,
            DTBegin = DTBegin,
            DTUpdate = DTUpdate,
            DTEnd = DTEnd
        };
    }
}

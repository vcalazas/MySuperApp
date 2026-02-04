using MSP.Domain.DTOs;
using MSP.Domain.Helpers;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace MSP.Domain.Entities
{
    public class MSPSystemSettings : MSPBaseEntity, IConvertModel<MSPSystemSettingsDTO>
    {
        [Key]
        public string SettingKey { get; set; }
        public string SettingValue { get; set; }

        public MSPSystemSettingsDTO Convert() => new MSPSystemSettingsDTO()
        {
            SettingKey = this.SettingKey,
            SettingValue = this.SettingValue,
            DTBegin = this.DTBegin,
            DTUpdate = this.DTUpdate,
            DTEnd = this.DTEnd
        };
    }
}

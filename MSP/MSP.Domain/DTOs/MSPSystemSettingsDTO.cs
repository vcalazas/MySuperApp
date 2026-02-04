using MSP.Domain.Entities;
using MSP.Domain.Helpers;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc; // Adicione este using

namespace MSP.Domain.DTOs
{
    public class MSPSystemSettingsDTO : MSPBaseEntityDTO, IConvertModel<MSPSystemSettings>
    {
        public string SettingKey { get; set; }
        public string SettingValue { get; set; }

        public MSPSystemSettings Convert() => new MSPSystemSettings()
        {
            SettingKey = this.SettingKey,
            SettingValue = this.SettingValue,
            DTBegin = this.DTBegin,
            DTUpdate = this.DTUpdate,
            DTEnd = this.DTEnd
        };
    }
}

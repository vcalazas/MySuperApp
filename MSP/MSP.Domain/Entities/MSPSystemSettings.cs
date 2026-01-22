using MSP.Domain.DTOs;
using MSP.Domain.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Text;
using System.Text.Json.Serialization;

namespace MSP.Domain.Entities
{
    public class MSPSystemSettings : IConvertModel<MSPSystemSettings, MSPSystemSettingsDTO>
    {
        [Key]
        public string SettingKey { get; set; }
        public string SettingValue { get; set; }

        public DateTime Register { get; set; } = DateTime.Now;

        [JsonIgnore]
        public MSPSystemSettingsDTO Convert => new MSPSystemSettingsDTO()
        {
            SettingKey = SettingKey,
            SettingValue = SettingValue
        };
    }
}

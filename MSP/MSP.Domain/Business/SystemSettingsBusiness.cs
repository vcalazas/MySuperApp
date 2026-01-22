using MSP.Domain.DTOs;
using MSP.Domain.Entities;
using MSP.Domain.Repositories;
using MSP.Domain.Extensions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace MSP.Domain.Business
{
    public class SystemSettingsBusiness : ISystemSettingsBusiness
    {
        private readonly ISystemSettingsRepository _systemSettingsRepository;

        public SystemSettingsBusiness(ISystemSettingsRepository systemSettingsRepository)
        {
            _systemSettingsRepository = systemSettingsRepository;
        }

        public async Task<IEnumerable<MSPSystemSettingsDTO>> GetAllAsync()
        {
            IEnumerable<MSPSystemSettings> list = await _systemSettingsRepository.GetAllAsync();
            return list.ConvertAll();
        }
    }
}

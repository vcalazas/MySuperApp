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

        public async Task<IEnumerable<MSPSystemSettingsDTO>> GetAllAsync(bool enabledOnly)
        {
            IEnumerable<MSPSystemSettings> list = await _systemSettingsRepository.GetAllAsync(enabledOnly);
            return list.ConvertAll();
        }

        public async Task<MSPSystemSettingsDTO?> AddAsync(MSPSystemSettingsDTO mSPSystemSettings)
        {
            if (mSPSystemSettings == null)
                return null;
            var updatedEntity = await _systemSettingsRepository.AddAsync(mSPSystemSettings.Convert());
            return updatedEntity?.Convert();
        }

        public async Task<MSPSystemSettingsDTO?> UpdateAsync(MSPSystemSettingsDTO mSPSystemSettings)
        {
            if (mSPSystemSettings == null)
                return null;
            var updatedEntity = await _systemSettingsRepository.UpdateAsync(mSPSystemSettings.Convert());
            return updatedEntity?.Convert();
        }

        public async Task<MSPSystemSettingsDTO?> DeleteAsync(MSPSystemSettingsDTO? mSPSystemSettings)
        {
            if (mSPSystemSettings == null)
                return null;

            var deletedEntity = await _systemSettingsRepository.DeleteAsync(mSPSystemSettings.Convert());
            return deletedEntity?.Convert();
        }
    }
}

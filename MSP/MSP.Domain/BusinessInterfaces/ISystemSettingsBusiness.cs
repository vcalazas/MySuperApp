using MSP.Domain.DTOs;
using MSP.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MSP.Domain.BusinessInterfaces
{
    public interface ISystemSettingsBusiness
    {
        Task<IEnumerable<MSPSystemSettingsDTO>> GetAllAsync(bool enabledOnly);
        Task<MSPSystemSettingsDTO?> AddAsync(MSPSystemSettingsDTO mSPSystemSettings);
        Task<MSPSystemSettingsDTO> UpdateAsync(MSPSystemSettingsDTO mSPSystemSettings);
        Task<MSPSystemSettingsDTO> DeleteAsync(MSPSystemSettingsDTO mSPSystemSettings);
    }
}

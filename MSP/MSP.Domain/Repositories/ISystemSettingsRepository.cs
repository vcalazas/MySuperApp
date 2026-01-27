using MSP.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MSP.Domain.Repositories
{
    public interface ISystemSettingsRepository
    {
        Task<MSPSystemSettings> AddAsync(MSPSystemSettings mSPSystemSettings);
        Task<IEnumerable<MSPSystemSettings>> GetAllAsync(bool enabledOnly);
        Task<MSPSystemSettings?> GetAsync(string key);
        Task<MSPSystemSettings> UpdateAsync(MSPSystemSettings mSPSystemSettings);
        Task<MSPSystemSettings> DeleteAsync(MSPSystemSettings mSPSystemSettings);
    }
}

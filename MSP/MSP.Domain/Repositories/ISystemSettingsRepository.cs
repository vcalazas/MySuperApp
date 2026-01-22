using MSP.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MSP.Domain.Repositories
{
    public interface ISystemSettingsRepository
    {
        Task<MSPSystemSettings> AddAsync(MSPSystemSettings mSPSystemSettings);
        Task<IEnumerable<MSPSystemSettings>> GetAllAsync();
    }
}

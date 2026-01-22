using Microsoft.EntityFrameworkCore;
using MSP.Domain.Entities;
using MSP.Domain.Repositories;
using NodaTime;
using System;
using System.Collections.Generic;
using System.Text;

namespace MSP.Data.Repositories
{
    public class SystemSettingsRepository : ISystemSettingsRepository
    {
        private readonly MSPContext _context;

        public SystemSettingsRepository(MSPContext context)
        {
            _context = context;
        }

        public void Dispose() => _context.Dispose();

        public async Task<MSPSystemSettings> AddAsync(MSPSystemSettings mSPSystemSettings)
        {
            mSPSystemSettings.Register = DateTime.Now;
            _context.MSPSystemSettings.Add(mSPSystemSettings);
            await _context.SaveChangesAsync();
            return mSPSystemSettings;
        }


        public async Task<IEnumerable<MSPSystemSettings>> GetAllAsync(string document, string name)
        {
            if (string.IsNullOrEmpty(document)) document = "";
            if (string.IsNullOrEmpty(name)) name = "";
            return await _context.MSPSystemSettings.ToListAsync();
        }
    }
}

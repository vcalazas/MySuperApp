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
            if(await GetAsync(mSPSystemSettings.SettingKey) != null)
                throw new Exception($"Setting with key {mSPSystemSettings.SettingKey} already exists.");
            mSPSystemSettings.DTBegin = DateTime.Now;
            mSPSystemSettings.DTEnd = DateTime.MaxValue;
            _context.MSPSystemSettings.Add(mSPSystemSettings);
            await _context.SaveChangesAsync();
            return mSPSystemSettings;
        }

        public async Task<IEnumerable<MSPSystemSettings>> GetAllAsync(bool enabledOnly)
        {
            if(enabledOnly)
                return await _context.MSPSystemSettings.Where(s => s.DTEnd > DateTime.Now).ToListAsync();
            return await _context.MSPSystemSettings.ToListAsync();
        }

        public async Task<MSPSystemSettings?> GetAsync(string key)
        {
            return (await _context.MSPSystemSettings.Where(s => s.SettingKey == key).ToListAsync()).FirstOrDefault();
        }

        public async Task<MSPSystemSettings> UpdateAsync(MSPSystemSettings mSPSystemSettings)
        {
            mSPSystemSettings.DTUpdate = DateTime.Now;
            _context.MSPSystemSettings.Update(mSPSystemSettings);
            await _context.SaveChangesAsync();
            return mSPSystemSettings;
        }

        public async Task<MSPSystemSettings> DeleteAsync(MSPSystemSettings mSPSystemSettings)
        {
            MSPSystemSettings? data = await GetAsync(mSPSystemSettings.SettingKey);
            if (data == null)
                throw new Exception($"Setting with key {mSPSystemSettings.SettingKey} does not exist.");

            data.DTEnd = DateTime.Now;
            _context.MSPSystemSettings.Update(data);
            await _context.SaveChangesAsync();
            return data;
        }
    }
}

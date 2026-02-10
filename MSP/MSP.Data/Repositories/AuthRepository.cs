using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MSP.Domain.Entities;
using MSP.Domain.Repositories;
using NodaTime;
using System;
using System.Collections.Generic;
using System.Text;

namespace MSP.Data.Repositories
{ 
    public class AuthRepository : IAuthRepository
    {
        private readonly MSPContext _context;

        public AuthRepository(MSPContext context)
        {
            _context = context;
        }

        public void Dispose() => _context.Dispose();

        public async Task<MSPAuth> AddAsync(MSPAuth mSPSystemSettings)
        {
            if(await GetByIdAsync(mSPSystemSettings) != null)
                throw new Exception($"Setting with key {mSPSystemSettings.AuthId} already exists.");
            mSPSystemSettings.DTBegin = DateTime.Now;
            mSPSystemSettings.DTEnd = DateTime.MaxValue;
            _context.MSPAuth.Add(mSPSystemSettings);
            await _context.SaveChangesAsync();
            return mSPSystemSettings;
        }

        public async Task<IEnumerable<MSPAuth>> GetAllAsync(bool enabledOnly)
        {
            if(enabledOnly)
                return await _context.MSPAuth.Where(s => s.DTEnd > DateTime.Now).ToListAsync();
            return await _context.MSPAuth.ToListAsync();
        }

        public async Task<MSPAuth> UpdateAsync(MSPAuth mSPSystemSettings)
        {
            mSPSystemSettings.DTUpdate = DateTime.Now;
            _context.MSPAuth.Update(mSPSystemSettings);
            await _context.SaveChangesAsync();
            return mSPSystemSettings;
        }

        public async Task<MSPAuth> DeleteAsync(MSPAuth mSPSystemSettings)
        {
            MSPAuth? data = await GetByIdAsync(mSPSystemSettings);
            if (data == null)
                throw new Exception($"Setting with key {mSPSystemSettings.AuthId} does not exist.");

            data.DTEnd = DateTime.Now;
            _context.MSPAuth.Update(data);
            await _context.SaveChangesAsync();
            return data;
        }

        public async Task<MSPAuth?> GetByIdAsync(MSPAuth entity)
        {
            return (await _context.MSPAuth.Where(s => s.AuthId == entity.AuthId).ToListAsync()).FirstOrDefault();
        }
    }
}

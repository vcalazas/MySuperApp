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
    public class PersonRepository : IPersonRepository
    {
        private readonly MSPContext _context;

        public PersonRepository(MSPContext context)
        {
            _context = context;
        }

        public void Dispose() => _context.Dispose();

        public async Task<MSPPerson> AddAsync(MSPPerson MSPPerson)
        {
            if(await GetAsync(MSPPerson) != null)
                throw new Exception($"Setting with key {MSPPerson.PersonId} already exists.");
            MSPPerson.DTBegin = DateTime.Now;
            MSPPerson.DTEnd = DateTime.MaxValue;
            _context.MSPPerson.Add(MSPPerson);
            await _context.SaveChangesAsync();
            return MSPPerson;
        }

        public async Task<IEnumerable<MSPPerson>> GetAllAsync(bool enabledOnly)
        {
            if(enabledOnly)
                return await _context.MSPPerson.Where(s => s.DTEnd > DateTime.Now).ToListAsync();
            return await _context.MSPPerson.ToListAsync();
        }

        public async Task<MSPPerson> UpdateAsync(MSPPerson MSPPerson)
        {
            MSPPerson.DTUpdate = DateTime.Now;
            _context.MSPPerson.Update(MSPPerson);
            await _context.SaveChangesAsync();
            return MSPPerson;
        }

        public async Task<MSPPerson> DeleteAsync(MSPPerson MSPPerson)
        {
            MSPPerson? data = await GetAsync(MSPPerson);
            if (data == null)
                throw new Exception($"Setting with key {MSPPerson.PersonId} does not exist.");

            data.DTEnd = DateTime.Now;
            _context.MSPPerson.Update(data);
            await _context.SaveChangesAsync();
            return data;
        }

        public async Task<MSPPerson?> GetAsync(MSPPerson entity)
        {
            return (await _context.MSPPerson.Where(s => s.PersonId == entity.PersonId).ToListAsync()).FirstOrDefault();
        }
    }
}

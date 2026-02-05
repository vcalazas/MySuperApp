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

        public async Task<MSPPerson> AddAsync(MSPPerson entity)
        {
            entity.DTBegin = DateTime.Now;
            entity.DTEnd = DateTime.MaxValue;
            _context.MSPPerson.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<IEnumerable<MSPPerson>> GetAllAsync(bool enabledOnly)
        {
            if(enabledOnly)
                return await _context.MSPPerson.Where(s => s.DTEnd > DateTime.Now).ToListAsync();
            return await _context.MSPPerson.ToListAsync();
        }

        public async Task<MSPPerson> UpdateAsync(MSPPerson entity)
        {
            entity.DTUpdate = DateTime.Now;
            _context.MSPPerson.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<MSPPerson> DeleteAsync(MSPPerson entity)
        {
            MSPPerson? data = await GetByIdAsync(entity);
            if (data == null)
                throw new Exception($"Setting with key {entity.PersonId} does not exist.");

            data.DTEnd = DateTime.Now;
            _context.MSPPerson.Update(data);
            await _context.SaveChangesAsync();
            return data;
        }

        public async Task<MSPPerson?> GetByIdAsync(MSPPerson entity)
        {
            return (await _context.MSPPerson.Where(s => s.PersonId == entity.PersonId).ToListAsync()).FirstOrDefault();
        }

        public async Task<MSPPerson?> GetByLoginAsync(MSPPerson entity)
        {
            return (await _context.MSPPerson.Where(s => s.Login == entity.Login).ToListAsync()).FirstOrDefault();
        }
    }
}

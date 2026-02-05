using MSP.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MSP.Domain.Repositories
{
    public interface IBaseRepository<IEntity>
    {
        Task<IEntity> AddAsync(IEntity entity);
        Task<IEnumerable<IEntity>> GetAllAsync(bool enabledOnly);
        Task<IEntity?> GetByIdAsync(IEntity entity);
        Task<IEntity> UpdateAsync(IEntity entity);
        Task<IEntity> DeleteAsync(IEntity entity);
    }
}

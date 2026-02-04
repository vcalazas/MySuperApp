using MSP.Domain.DTOs;
using MSP.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MSP.Domain.BusinessInterfaces
{
    public interface IBaseBusiness<IDTO>
    {
        Task<IEnumerable<IDTO>> GetAllAsync(bool enabledOnly);
        Task<IDTO?> AddAsync(IDTO dto);
        Task<IDTO> UpdateAsync(IDTO dto);
        Task<IDTO> DeleteAsync(IDTO dto);
    }
}

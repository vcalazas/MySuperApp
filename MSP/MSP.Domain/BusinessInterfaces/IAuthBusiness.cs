using MSP.Domain.Business;
using MSP.Domain.DTOs;
using MSP.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MSP.Domain.BusinessInterfaces
{
    public interface IAuthBusiness : IBaseBusiness<MSPAuthDTO, MSPAuth>
    {
        Task<MSPAuthDTO?> LoginAsync(MSPAuthDTO dto);
    }
}

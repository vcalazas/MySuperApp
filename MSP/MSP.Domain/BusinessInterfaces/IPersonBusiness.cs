using MSP.Domain.DTOs;
using MSP.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MSP.Domain.BusinessInterfaces
{
    public interface IPersonBusiness : IBaseBusiness<MSPPersonDTO, MSPPerson>
    {
        Task<MSPPersonDTO> ChangePassworldAsync(MSPPersonDTO dto);
    }
}

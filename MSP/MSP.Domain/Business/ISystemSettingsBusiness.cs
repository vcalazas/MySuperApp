using MSP.Domain.DTOs;
using MSP.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MSP.Domain.Business
{
    public interface ISystemSettingsBusiness
    {
        Task<IEnumerable<MSPSystemSettingsDTO>> GetAllAsync();
    }
}

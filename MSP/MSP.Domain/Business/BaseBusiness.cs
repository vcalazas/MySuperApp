using MSP.Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace MSP.Domain.Business
{
    public class BaseBusiness
    {
        protected T GetErrorDTO<T>(string message) where T : MSPBaseEntityDTO, new()
        {
            return new T()
            {
                ErrorMessage = message
            };
        }
    }
}

using MSP.Domain.DTOs;
using MSP.Domain.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MSP.Domain.Entities
{
    public class MSPPersonDTO: MSPBaseEntityDTO, IConvertModel<MSPPerson>
    {
        public int PersonId { get; set; }
        public string? Name { get; set; }
        public string? Login { get; set; }
        public string? Passworld { get; set; }

        public MSPPerson Convert()
        {
            return new MSPPerson()
            {
                PersonId = this.PersonId,
                Name = this.Name,
                Login = this.Login,
                Passworld = this.Passworld,
                DTBegin = this.DTBegin,
                DTUpdate = this.DTUpdate,
                DTEnd = this.DTEnd
            };
        }
    }
}

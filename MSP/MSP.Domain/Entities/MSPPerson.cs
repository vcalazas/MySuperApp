using MSP.Domain.DTOs;
using MSP.Domain.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MSP.Domain.Entities
{
    public class MSPPerson : MSPBaseEntity, IConvertModel<MSPPersonDTO>
    {
        [Key]
        public int PersonId { get; set; }
        public string? Name { get; set; }
        public string? Login { get; set; }
        public string? Passworld { get; set; }

        public MSPPersonDTO Convert()
        {
            return new MSPPersonDTO()
            {
                PersonId = this.PersonId,
                Name = this.Name,
                Login = this.Login,
                DTBegin = this.DTBegin,
                DTUpdate = this.DTUpdate,
                DTEnd = this.DTEnd
            };
        }
    }
}

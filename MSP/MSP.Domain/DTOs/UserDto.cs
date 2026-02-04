using System;
using System.Collections.Generic;
using System.Text;

namespace MSP.Domain.DTOs
{
    [Obsolete("UserDto está obsoleto. Use MSPPerson em seu lugar.")]
    public  class UserDto
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string AccessToken { get; set; }
        public int ExpiresIn { get; set; }
    }
}

using MSP.Data.DTOs;

namespace MSP.API.Model
{
    public class LoginResponseAPIModel
    {
        public string UserName { get; set; }
        public string AccessToken { get; set; }
        public int ExpiresIn{ get; set; }

        public UserDto UserDto { get; set; }
    }
}

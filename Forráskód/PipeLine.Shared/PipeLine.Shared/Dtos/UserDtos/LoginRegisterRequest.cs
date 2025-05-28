using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PipeLine.Shared.Dtos.UserDtos
{
    public class LoginRegisterRequest
    {
        public LoginRegisterRequest(string email, string password, bool stayLoggedIn = false)
        {
            Email = email;
            Password = password;
            StayLoggedIn = stayLoggedIn;
        }

        public LoginRegisterRequest()
        {
            
        }

        public string Email { get; set; }
        public string Password { get; set; }
        public bool StayLoggedIn { get; set; } = false;

        
    }
}

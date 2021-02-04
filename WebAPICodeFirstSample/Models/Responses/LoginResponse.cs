using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPICodeFirstSample.Models.Responses
{
    public class LoginResponse
    {      
        public string DisplayName { get; set; }
        public string Email { get; set; }       
        public string RoleName { get; set; }
        public string Token { get; set; }

    }
}

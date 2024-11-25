using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealTech.Application.Jwt
{
    public class JwtSettings
    {
        public string SecretKey { get; set; }
        public int Expires { get; set; }
        public string Audience { get; set; }
        public string Issuer { get; set; } 
    }

}

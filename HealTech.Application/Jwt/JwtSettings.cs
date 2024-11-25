using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealTech.Application.Jwt
{
    public class JwtSettings
    {
        public string Key { get; set; } = string.Empty;
        public int Expires { get; set; }
        public string Audience { get; set; } = string.Empty;
        public string Issuer { get; set; } = string.Empty;
    }

}

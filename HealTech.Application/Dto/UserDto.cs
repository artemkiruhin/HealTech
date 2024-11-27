using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealTech.Application.Dto
{
    public abstract class UserDto
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string Surname { get; set; } = null!;
        public string Username { get; set; } = null!;
        public bool IsActive { get; set; }
        public string Role { get; set; } = null!;
    }
}

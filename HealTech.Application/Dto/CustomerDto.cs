﻿namespace HealTech.Application.Dto
{
    public class CustomerDto : UserDto
    {
        public DateTime Registered { get; set; }
        public string TaxNumber { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string Address { get; set; } = null!;
    }

}

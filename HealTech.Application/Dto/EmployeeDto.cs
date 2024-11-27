namespace HealTech.Application.Dto
{
    public class EmployeeDto : UserDto
    {
        public DateTime Hired { get; set; }
        public bool IsAdmin { get; set; }
        public decimal Salary { get; set; }
    }

}

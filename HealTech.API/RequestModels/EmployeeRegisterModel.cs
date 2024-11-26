namespace HealTech.API.RequestModels
{
    public class EmployeeRegisterModel
    {
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public bool IsAdmin { get; set; }
        public decimal Salary { get; set; }
    }
}

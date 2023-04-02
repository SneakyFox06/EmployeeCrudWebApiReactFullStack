namespace EmployeeCrudAsp.Models.Employee
{
    public class EmployeeReadOnlyDto : EmployeeBaseDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string email { get; set; }
        public DateTime Dob { get; set; }
        public int Age { get; set; }
        public int Salary { get; set; }

        public string DepartmentId { get; set; }
        public string DepartmentName { get; set; }
    }
}

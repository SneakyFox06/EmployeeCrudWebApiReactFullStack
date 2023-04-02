using System.ComponentModel.DataAnnotations;

namespace EmployeeCrudAsp.Models.Employee
{
    public class EmployeeUpdateDto : EmployeeBaseDto
    {
        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        public string LastName { get; set; }

        [Required]
        [StringLength(100)]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public DateTime Dob { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public int Salary { get; set; }

        [Required]
        public int DepartmentId { get; set; }
    }
}

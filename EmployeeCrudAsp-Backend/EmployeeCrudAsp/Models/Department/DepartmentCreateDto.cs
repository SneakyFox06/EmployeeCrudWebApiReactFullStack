using System.ComponentModel.DataAnnotations;

namespace EmployeeCrudAsp.Models.Department
{
    public class DepartmentCreateDto 
    {
        [Required]
        [StringLength(50)]
        public string DepartmentName { get; set; }
    }
}

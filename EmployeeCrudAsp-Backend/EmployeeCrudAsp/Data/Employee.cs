using System;
using System.Collections.Generic;

namespace EmployeeCrudAsp.Data;

public partial class Employee
{
    public int EId { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? Email { get; set; }

    public DateTime? Dob { get; set; }

    public int? Age { get; set; }

    public int? Salary { get; set; }

    public int? DepartmentId { get; set; }

    public virtual Department? Department { get; set; }
}

using System;
using System.Collections.Generic;

namespace EmployeeCrudAsp.Data;

public partial class Department
{
    public int DId { get; set; }

    public string? DepartmentName { get; set; }

    public virtual ICollection<Employee> Employees { get; } = new List<Employee>();
}

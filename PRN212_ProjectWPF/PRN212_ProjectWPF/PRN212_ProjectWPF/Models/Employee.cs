using System;
using System.Collections.Generic;

namespace PRN212_ProjectWPF.Models;

public partial class Employee
{
    public int EmployeeId { get; set; }

    public string? Name { get; set; }

    public string? Role { get; set; }

    public string Email { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public DateOnly? Birthday { get; set; }

    public string? Password { get; set; }

    public bool? Status { get; set; }
    public string StatusDetail
    {
        get { return GetStatusDetail(); }
    }
    public string GetStatusDetail()
    {
        return Status == true ? "Active" : "Deleted";
    }
    public int? BranchId { get; set; }

    public virtual Branch? Branch { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}

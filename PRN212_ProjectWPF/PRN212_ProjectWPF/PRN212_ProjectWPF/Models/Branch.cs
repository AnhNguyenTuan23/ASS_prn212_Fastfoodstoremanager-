using System;
using System.Collections.Generic;

namespace PRN212_ProjectWPF.Models;

public partial class Branch
{
    public int BranchId { get; set; }

    public string? BranchName { get; set; }

    public string? Address { get; set; }

    public string Phone { get; set; } = null!;

    public bool? Status { get; set; }

    public string StatusDetail
    {
        get { return GetStatusDetail(); }
    }
    public string GetStatusDetail()
    {
        return Status == true ? "Active" : "Deleted";
    }
    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();

    public virtual ICollection<NumberDish> NumberDishes { get; set; } = new List<NumberDish>();

    public virtual ICollection<Table> Tables { get; set; } = new List<Table>();
}

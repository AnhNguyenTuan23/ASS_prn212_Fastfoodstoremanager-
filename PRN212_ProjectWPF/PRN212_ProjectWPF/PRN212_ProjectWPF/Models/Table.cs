using System;
using System.Collections.Generic;

namespace PRN212_ProjectWPF.Models;

public partial class Table
{
    public int TableId { get; set; }

    public string? TableNumber { get; set; }

    public int? BranchId { get; set; }

    public virtual Branch? Branch { get; set; }

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
}

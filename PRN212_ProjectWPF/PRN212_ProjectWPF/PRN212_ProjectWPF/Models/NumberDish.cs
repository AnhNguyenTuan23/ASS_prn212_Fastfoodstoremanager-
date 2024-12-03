using System;
using System.Collections.Generic;

namespace PRN212_ProjectWPF.Models;

public partial class NumberDish
{
    public int DishId { get; set; }

    public int BranchId { get; set; }

    public int? TotalNumber { get; set; }

    public virtual Branch Branch { get; set; } = null!;

    public virtual Menu Dish { get; set; } = null!;
}

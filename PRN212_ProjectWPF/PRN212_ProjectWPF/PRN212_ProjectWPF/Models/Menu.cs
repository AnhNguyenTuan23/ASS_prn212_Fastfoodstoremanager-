using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace PRN212_ProjectWPF.Models;

public partial class Menu
{
    public int DishId { get; set; }

    public string? DishName { get; set; }

    public int Price { get; set; }

    public bool? Status { get; set; }
    public string StatusDetail
    {
        get { return GetStatusDetail(); }
    }
    public string GetStatusDetail()
    {
        return Status == true ? "Active" : "Deleted";
    }
    public string? Image { get; set; }

    public virtual ICollection<NumberDish> NumberDishes { get; set; } = new List<NumberDish>();

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

    [NotMapped]
    public int TotalNumberForBranch { get; set; }
    public int GetTotalNumber(int? branchId)
    {
        var numberDish = NumberDishes.FirstOrDefault(dish => dish.BranchId == branchId && dish.DishId == DishId);
        return numberDish?.TotalNumber ?? 0;
    }
}

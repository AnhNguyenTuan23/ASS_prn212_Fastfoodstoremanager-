using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace PRN212_ProjectWPF.Models;

public partial class OrderDetail
{
    public int OrderId { get; set; }

    public int DishId { get; set; }

    [NotMapped]
    public string DishName { get; set; }
    public int Quantity { get; set; }

    public int TableId { get; set; }

    public virtual Menu Dish { get; set; } = null!;

    public virtual Order Order { get; set; } = null!;

    public virtual Table Table { get; set; } = null!;
    [NotMapped]
    public DateTime? EndDate => Order?.EndDate;
}

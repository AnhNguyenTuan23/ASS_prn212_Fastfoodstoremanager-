using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace PRN212_ProjectWPF.Models;

public partial class Order
{
    public int OrderId { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public int? TotalPrice { get; set; }

    public byte? Status { get; set; }
    public string StatusDetail
    {
        get { return GetStatusDetail(); }
    }
    public string GetStatusDetail()
    {
        String result = "";
        switch (Status)
        {
            case 0:
                result = "Paid";
                break;
            case 1:
                result = "Order";
                break;
            case 2:
                result = "Serving";
                break;
            case 3:
                result = "Delete";
                break;
        }
        return result;
    }
    public int? CustomerId { get; set; }

    public int? EmployeeId { get; set; }

    [NotMapped]
    public string BranchName { get; set; }
    public virtual Customer? Customer { get; set; }

    public virtual Employee? Employee { get; set; }

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
}

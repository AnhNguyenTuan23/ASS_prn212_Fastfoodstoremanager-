using System;
using System.Collections.Generic;

namespace PRN212_ProjectWPF.Models;

public partial class Customer
{
    public int CustomerId { get; set; }

    public string? CustomerName { get; set; }

    public string Email { get; set; } = null!;

    public string? Phone { get; set; }

    public bool? Gender { get; set; }

    public string GenderDetail
    {
        get { return GetGenderDetail(); }
    }
    public string GetGenderDetail()
    {
        return Gender == true ? "Male" : "Female";
    }
    public string? Password { get; set; }

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
                result = "Active";
                break;
            case 1:
                result = "Delete";
                break;
        }
        return result;
    }
    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}

using System;
using System.Collections.Generic;

namespace ProductsApi.Models;

public partial class Customer
{
    public int Customerid { get; set; }

    public string? Customercode { get; set; }

    public string Fullname { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public string? Email { get; set; }

    public string? Address { get; set; }

    public DateOnly? Dateofbirth { get; set; }

    public string? Gender { get; set; }

    public decimal? Totalpurchases { get; set; }

    public int? Totaltransactions { get; set; }

    public DateTime? Lastpurchasedate { get; set; }

    public string? Notes { get; set; }

    public bool Isactive { get; set; }

    public bool Isdeleted { get; set; }

    public DateTime? Createdat { get; set; }

    public DateTime? Updatedat { get; set; }

    public virtual ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();

    public virtual ICollection<Warranty> Warranties { get; set; } = new List<Warranty>();
}

using System;
using System.Collections.Generic;

namespace JewelryService.Models;

public partial class Warranty
{
    public int Warrantyid { get; set; }

    public string Warrantycode { get; set; } = null!;

    public int Invoicedetailid { get; set; }

    public int Customerid { get; set; }

    public int Productid { get; set; }

    public DateOnly Startdate { get; set; }

    public DateOnly Enddate { get; set; }

    public int Warrantyperiodmonths { get; set; }

    public string? Warrantytype { get; set; }

    public string? Coveragedescription { get; set; }

    public string? Terms { get; set; }

    public string? Exclusions { get; set; }

    public string? Status { get; set; }

    public int? Claimcount { get; set; }

    public DateTime? Lastclaimdate { get; set; }

    public string? Notes { get; set; }

    public DateTime? Createdat { get; set; }

    public DateTime? Updatedat { get; set; }

    public virtual Customer Customer { get; set; } = null!;

    public virtual Invoicedetail Invoicedetail { get; set; } = null!;

    public virtual Product Product { get; set; } = null!;

    public virtual ICollection<Warrantyclaim> Warrantyclaims { get; set; } = new List<Warrantyclaim>();
}

using System;
using System.Collections.Generic;

namespace ProductsApi.Models;

public partial class Promotion
{
    public int Promotionid { get; set; }

    public string Promotioncode { get; set; } = null!;

    public string Promotionname { get; set; } = null!;

    public string? Description { get; set; }

    public string Discounttype { get; set; } = null!;

    public decimal? Discountvalue { get; set; }

    public decimal? Minpurchaseamount { get; set; }

    public decimal? Maxdiscountamount { get; set; }

    public string? Applicablecategories { get; set; }

    public string? Applicableproducts { get; set; }

    public DateTime Startdate { get; set; }

    public DateTime Enddate { get; set; }

    public int? Usagelimit { get; set; }

    public int? Usagecount { get; set; }

    public int? Usagelimitpercustomer { get; set; }

    public bool? Isactive { get; set; }

    public bool Isdeleted { get; set; }

    public int Createdby { get; set; }

    public DateTime? Createdat { get; set; }

    public DateTime? Updatedat { get; set; }

    public virtual User CreatedbyNavigation { get; set; } = null!;

    public virtual ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();
}

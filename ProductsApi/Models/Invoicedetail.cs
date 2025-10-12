using System;
using System.Collections.Generic;

namespace ProductsApi.Models;

public partial class Invoicedetail
{
    public int Invoicedetailid { get; set; }

    public int Invoiceid { get; set; }

    public int Productid { get; set; }

    public int Quantity { get; set; }

    public string Productcode { get; set; } = null!;

    public string Productname { get; set; } = null!;

    public decimal Unitprice { get; set; }

    public decimal? Goldprice { get; set; }

    public decimal? Laborcost { get; set; }

    public decimal? Stoneprice { get; set; }

    public decimal? Goldweight { get; set; }

    public decimal? Gemweight { get; set; }

    public bool? Hasgem { get; set; }

    public string? Material { get; set; }

    public decimal? Discountpercent { get; set; }

    public decimal? Discountamount { get; set; }

    public decimal Linetotal { get; set; }

    public string? Notes { get; set; }

    public virtual Invoice Invoice { get; set; } = null!;

    public virtual Product Product { get; set; } = null!;

    public virtual ICollection<Warranty> Warranties { get; set; } = new List<Warranty>();
}

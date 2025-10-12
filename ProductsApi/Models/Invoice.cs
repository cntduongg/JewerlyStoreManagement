using System;
using System.Collections.Generic;

namespace ProductsApi.Models;

public partial class Invoice
{
    public int Invoiceid { get; set; }

    public string Invoicecode { get; set; } = null!;

    public string Invoicetype { get; set; } = null!;

    public int? Customerid { get; set; }

    public int? Counterid { get; set; }

    public int Staffid { get; set; }

    public int? Promotionid { get; set; }

    public string? Promotioncode { get; set; }

    public decimal Subtotal { get; set; }

    public decimal? Discountamount { get; set; }

    public decimal? Taxrate { get; set; }

    public decimal? Taxamount { get; set; }

    public decimal Totalamount { get; set; }

    public string? Paymentmethod { get; set; }

    public string? Paymentstatus { get; set; }

    public decimal? Paidamount { get; set; }

    public decimal? Remainingamount { get; set; }

    public string? Customernotes { get; set; }

    public string? Internalnotes { get; set; }

    public string? Invoicestatus { get; set; }

    public bool Isdeleted { get; set; }

    public DateTime? Invoicedate { get; set; }

    public DateTime? Createdat { get; set; }

    public DateTime? Updatedat { get; set; }

    public virtual Counter? Counter { get; set; }

    public virtual Customer? Customer { get; set; }

    public virtual ICollection<Invoicedetail> Invoicedetails { get; set; } = new List<Invoicedetail>();

    public virtual Promotion? Promotion { get; set; }

    public virtual User Staff { get; set; } = null!;
}

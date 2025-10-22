using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProductsApi.Models;

[Table("invoices")]
[Index("Invoicecode", Name = "invoices_invoicecode_key", IsUnique = true)]
[Index("Invoicecode", Name = "ix_invoices_code")]
[Index("Customerid", Name = "ix_invoices_customer")]
[Index("Invoicedate", Name = "ix_invoices_date", AllDescending = true)]
[Index("Staffid", Name = "ix_invoices_staff")]
[Index("Invoicestatus", Name = "ix_invoices_status")]
[Index("Invoicetype", Name = "ix_invoices_type")]
public partial class Invoice
{
    [Key]
    [Column("invoiceid")]
    public int Invoiceid { get; set; }

    [Column("invoicecode")]
    [StringLength(50)]
    public string Invoicecode { get; set; } = null!;

    [Column("invoicetype")]
    [StringLength(10)]
    public string Invoicetype { get; set; } = null!;

    [Column("customerid")]
    public int? Customerid { get; set; }

    [Column("counterid")]
    public int? Counterid { get; set; }

    [Column("staffid")]
    public int Staffid { get; set; }

    [Column("promotionid")]
    public int? Promotionid { get; set; }

    [Column("promotioncode")]
    [StringLength(50)]
    public string? Promotioncode { get; set; }

    [Column("subtotal")]
    [Precision(18, 2)]
    public decimal Subtotal { get; set; }

    [Column("discountamount")]
    [Precision(18, 2)]
    public decimal? Discountamount { get; set; }

    [Column("taxrate")]
    [Precision(5, 2)]
    public decimal? Taxrate { get; set; }

    [Column("taxamount")]
    [Precision(18, 2)]
    public decimal? Taxamount { get; set; }

    [Column("totalamount")]
    [Precision(18, 2)]
    public decimal Totalamount { get; set; }

    [Column("paymentmethod")]
    [StringLength(50)]
    public string? Paymentmethod { get; set; }

    [Column("paymentstatus")]
    [StringLength(20)]
    public string? Paymentstatus { get; set; }

    [Column("paidamount")]
    [Precision(18, 2)]
    public decimal? Paidamount { get; set; }

    [Column("remainingamount")]
    [Precision(18, 2)]
    public decimal? Remainingamount { get; set; }

    [Column("customernotes")]
    [StringLength(1000)]
    public string? Customernotes { get; set; }

    [Column("internalnotes")]
    [StringLength(1000)]
    public string? Internalnotes { get; set; }

    [Column("invoicestatus")]
    [StringLength(20)]
    public string? Invoicestatus { get; set; }

    [Column("isdeleted")]
    public bool Isdeleted { get; set; }

    [Column("invoicedate", TypeName = "timestamp without time zone")]
    public DateTime? Invoicedate { get; set; }

    [Column("createdat", TypeName = "timestamp without time zone")]
    public DateTime? Createdat { get; set; }

    [Column("updatedat", TypeName = "timestamp without time zone")]
    public DateTime? Updatedat { get; set; }

    [ForeignKey("Counterid")]
    [InverseProperty("Invoices")]
    public virtual Counter? Counter { get; set; }

    [ForeignKey("Customerid")]
    [InverseProperty("Invoices")]
    public virtual Customer? Customer { get; set; }

    [InverseProperty("Invoice")]
    public virtual ICollection<Invoicedetail> Invoicedetails { get; set; } = new List<Invoicedetail>();

    [ForeignKey("Promotionid")]
    [InverseProperty("Invoices")]
    public virtual Promotion? Promotion { get; set; }

    [ForeignKey("Staffid")]
    [InverseProperty("Invoices")]
    public virtual User Staff { get; set; } = null!;
}

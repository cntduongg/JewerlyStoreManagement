using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProductsApi.Models;

[Table("invoicedetails")]
[Index("Invoiceid", Name = "ix_invoicedetails_invoice")]
[Index("Productid", Name = "ix_invoicedetails_product")]
public partial class Invoicedetail
{
    [Key]
    [Column("invoicedetailid")]
    public int Invoicedetailid { get; set; }

    [Column("invoiceid")]
    public int Invoiceid { get; set; }

    [Column("productid")]
    public int Productid { get; set; }

    [Column("quantity")]
    public int Quantity { get; set; }

    [Column("productcode")]
    [StringLength(50)]
    public string Productcode { get; set; } = null!;

    [Column("productname")]
    [StringLength(200)]
    public string Productname { get; set; } = null!;

    [Column("unitprice")]
    [Precision(18, 2)]
    public decimal Unitprice { get; set; }

    [Column("goldprice")]
    [Precision(18, 2)]
    public decimal? Goldprice { get; set; }

    [Column("laborcost")]
    [Precision(18, 2)]
    public decimal? Laborcost { get; set; }

    [Column("stoneprice")]
    [Precision(18, 2)]
    public decimal? Stoneprice { get; set; }

    [Column("goldweight")]
    [Precision(10, 3)]
    public decimal? Goldweight { get; set; }

    [Column("gemweight")]
    [Precision(10, 3)]
    public decimal? Gemweight { get; set; }

    [Column("hasgem")]
    public bool? Hasgem { get; set; }

    [Column("material")]
    [StringLength(50)]
    public string? Material { get; set; }

    [Column("discountpercent")]
    [Precision(5, 2)]
    public decimal? Discountpercent { get; set; }

    [Column("discountamount")]
    [Precision(18, 2)]
    public decimal? Discountamount { get; set; }

    [Column("linetotal")]
    [Precision(18, 2)]
    public decimal Linetotal { get; set; }

    [Column("notes")]
    [StringLength(500)]
    public string? Notes { get; set; }

    [ForeignKey("Invoiceid")]
    [InverseProperty("Invoicedetails")]
    public virtual Invoice Invoice { get; set; } = null!;

    [ForeignKey("Productid")]
    [InverseProperty("Invoicedetails")]
    public virtual Product Product { get; set; } = null!;

    [InverseProperty("Invoicedetail")]
    public virtual ICollection<Warranty> Warranties { get; set; } = new List<Warranty>();
}

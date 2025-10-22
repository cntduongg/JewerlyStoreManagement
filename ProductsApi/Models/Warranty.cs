using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProductsApi.Models;

[Table("warranties")]
[Index("Warrantycode", Name = "ix_warranties_code")]
[Index("Customerid", Name = "ix_warranties_customer")]
[Index("Invoicedetailid", Name = "ix_warranties_invoicedetail")]
[Index("Status", Name = "ix_warranties_status")]
[Index("Warrantycode", Name = "warranties_warrantycode_key", IsUnique = true)]
public partial class Warranty
{
    [Key]
    [Column("warrantyid")]
    public int Warrantyid { get; set; }

    [Column("warrantycode")]
    [StringLength(50)]
    public string Warrantycode { get; set; } = null!;

    [Column("invoicedetailid")]
    public int Invoicedetailid { get; set; }

    [Column("customerid")]
    public int Customerid { get; set; }

    [Column("productid")]
    public int Productid { get; set; }

    [Column("startdate")]
    public DateOnly Startdate { get; set; }

    [Column("enddate")]
    public DateOnly Enddate { get; set; }

    [Column("warrantyperiodmonths")]
    public int Warrantyperiodmonths { get; set; }

    [Column("warrantytype")]
    [StringLength(50)]
    public string? Warrantytype { get; set; }

    [Column("coveragedescription")]
    public string? Coveragedescription { get; set; }

    [Column("terms")]
    public string? Terms { get; set; }

    [Column("exclusions")]
    public string? Exclusions { get; set; }

    [Column("status")]
    [StringLength(20)]
    public string? Status { get; set; }

    [Column("claimcount")]
    public int? Claimcount { get; set; }

    [Column("lastclaimdate", TypeName = "timestamp without time zone")]
    public DateTime? Lastclaimdate { get; set; }

    [Column("notes")]
    [StringLength(1000)]
    public string? Notes { get; set; }

    [Column("createdat", TypeName = "timestamp without time zone")]
    public DateTime? Createdat { get; set; }

    [Column("updatedat", TypeName = "timestamp without time zone")]
    public DateTime? Updatedat { get; set; }

    [ForeignKey("Customerid")]
    [InverseProperty("Warranties")]
    public virtual Customer Customer { get; set; } = null!;

    [ForeignKey("Invoicedetailid")]
    [InverseProperty("Warranties")]
    public virtual Invoicedetail Invoicedetail { get; set; } = null!;

    [ForeignKey("Productid")]
    [InverseProperty("Warranties")]
    public virtual Product Product { get; set; } = null!;

    [InverseProperty("Warranty")]
    public virtual ICollection<Warrantyclaim> Warrantyclaims { get; set; } = new List<Warrantyclaim>();
}

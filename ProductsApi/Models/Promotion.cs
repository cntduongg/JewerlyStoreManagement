using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProductsApi.Models;

[Table("promotions")]
[Index("Promotioncode", Name = "ix_promotions_code")]
[Index("Startdate", "Enddate", Name = "ix_promotions_dates")]
[Index("Promotioncode", Name = "promotions_promotioncode_key", IsUnique = true)]
public partial class Promotion
{
    [Key]
    [Column("promotionid")]
    public int Promotionid { get; set; }

    [Column("promotioncode")]
    [StringLength(50)]
    public string Promotioncode { get; set; } = null!;

    [Column("promotionname")]
    [StringLength(200)]
    public string Promotionname { get; set; } = null!;

    [Column("description")]
    public string? Description { get; set; }

    [Column("discounttype")]
    [StringLength(20)]
    public string Discounttype { get; set; } = null!;

    [Column("discountvalue")]
    [Precision(18, 2)]
    public decimal? Discountvalue { get; set; }

    [Column("minpurchaseamount")]
    [Precision(18, 2)]
    public decimal? Minpurchaseamount { get; set; }

    [Column("maxdiscountamount")]
    [Precision(18, 2)]
    public decimal? Maxdiscountamount { get; set; }

    [Column("applicablecategories")]
    [StringLength(500)]
    public string? Applicablecategories { get; set; }

    [Column("applicableproducts")]
    [StringLength(500)]
    public string? Applicableproducts { get; set; }

    [Column("startdate", TypeName = "timestamp without time zone")]
    public DateTime Startdate { get; set; }

    [Column("enddate", TypeName = "timestamp without time zone")]
    public DateTime Enddate { get; set; }

    [Column("usagelimit")]
    public int? Usagelimit { get; set; }

    [Column("usagecount")]
    public int? Usagecount { get; set; }

    [Column("usagelimitpercustomer")]
    public int? Usagelimitpercustomer { get; set; }

    [Column("isactive")]
    public bool? Isactive { get; set; }

    [Column("isdeleted")]
    public bool Isdeleted { get; set; }

    [Column("createdby")]
    public int Createdby { get; set; }

    [Column("createdat", TypeName = "timestamp without time zone")]
    public DateTime? Createdat { get; set; }

    [Column("updatedat", TypeName = "timestamp without time zone")]
    public DateTime? Updatedat { get; set; }

    [ForeignKey("Createdby")]
    [InverseProperty("Promotions")]
    public virtual User CreatedbyNavigation { get; set; } = null!;

    [InverseProperty("Promotion")]
    public virtual ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();
}

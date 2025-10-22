using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProductsApi.Models;

[Table("goldprices")]
[Index("Pricedate", Name = "ix_goldprices_date", AllDescending = true)]
[Index("Goldtype", Name = "ix_goldprices_type")]
[Index("Pricedate", "Goldtype", Name = "uq_goldprice_date_type", IsUnique = true)]
public partial class Goldprice
{
    [Key]
    [Column("goldpriceid")]
    public int Goldpriceid { get; set; }

    [Column("pricedate")]
    public DateOnly Pricedate { get; set; }

    [Column("goldtype")]
    [StringLength(50)]
    public string Goldtype { get; set; } = null!;

    [Column("buyprice")]
    [Precision(18, 2)]
    public decimal Buyprice { get; set; }

    [Column("sellprice")]
    [Precision(18, 2)]
    public decimal Sellprice { get; set; }

    [Column("worldgoldprice")]
    [Precision(18, 2)]
    public decimal? Worldgoldprice { get; set; }

    [Column("exchangerate")]
    [Precision(18, 4)]
    public decimal? Exchangerate { get; set; }

    [Column("updatedby")]
    public int Updatedby { get; set; }

    [Column("createdat", TypeName = "timestamp without time zone")]
    public DateTime? Createdat { get; set; }

    [Column("updatedat", TypeName = "timestamp without time zone")]
    public DateTime? Updatedat { get; set; }

    [ForeignKey("Updatedby")]
    [InverseProperty("Goldprices")]
    public virtual User UpdatedbyNavigation { get; set; } = null!;
}

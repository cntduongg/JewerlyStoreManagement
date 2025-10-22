using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProductsApi.Models;

[Table("stockmovements")]
[Index("Createdat", Name = "ix_stockmovements_date", AllDescending = true)]
[Index("Productid", Name = "ix_stockmovements_product")]
[Index("Movementtype", Name = "ix_stockmovements_type")]
public partial class Stockmovement
{
    [Key]
    [Column("movementid")]
    public int Movementid { get; set; }

    [Column("productid")]
    public int Productid { get; set; }

    [Column("movementtype")]
    [StringLength(20)]
    public string Movementtype { get; set; } = null!;

    [Column("quantity")]
    public int Quantity { get; set; }

    [Column("referencetype")]
    [StringLength(20)]
    public string? Referencetype { get; set; }

    [Column("referenceid")]
    public int? Referenceid { get; set; }

    [Column("quantitybefore")]
    public int Quantitybefore { get; set; }

    [Column("quantityafter")]
    public int Quantityafter { get; set; }

    [Column("reason")]
    [StringLength(500)]
    public string? Reason { get; set; }

    [Column("createdby")]
    public int Createdby { get; set; }

    [Column("createdat", TypeName = "timestamp without time zone")]
    public DateTime? Createdat { get; set; }

    [ForeignKey("Createdby")]
    [InverseProperty("Stockmovements")]
    public virtual User CreatedbyNavigation { get; set; } = null!;

    [ForeignKey("Productid")]
    [InverseProperty("Stockmovements")]
    public virtual Product Product { get; set; } = null!;
}

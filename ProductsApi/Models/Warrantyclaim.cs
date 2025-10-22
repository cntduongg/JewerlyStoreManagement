using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProductsApi.Models;

[Table("warrantyclaims")]
[Index("Claimcode", Name = "ix_warrantyclaims_code")]
[Index("Status", Name = "ix_warrantyclaims_status")]
[Index("Warrantyid", Name = "ix_warrantyclaims_warranty")]
[Index("Claimcode", Name = "warrantyclaims_claimcode_key", IsUnique = true)]
public partial class Warrantyclaim
{
    [Key]
    [Column("claimid")]
    public int Claimid { get; set; }

    [Column("claimcode")]
    [StringLength(50)]
    public string Claimcode { get; set; } = null!;

    [Column("warrantyid")]
    public int Warrantyid { get; set; }

    [Column("claimdate", TypeName = "timestamp without time zone")]
    public DateTime? Claimdate { get; set; }

    [Column("issuedescription")]
    public string Issuedescription { get; set; } = null!;

    [Column("claimtype")]
    [StringLength(50)]
    public string? Claimtype { get; set; }

    [Column("handledby")]
    public int? Handledby { get; set; }

    [Column("resolutiondescription")]
    public string? Resolutiondescription { get; set; }

    [Column("repaircost")]
    [Precision(18, 2)]
    public decimal? Repaircost { get; set; }

    [Column("status")]
    [StringLength(20)]
    public string? Status { get; set; }

    [Column("completeddate", TypeName = "timestamp without time zone")]
    public DateTime? Completeddate { get; set; }

    [Column("createdat", TypeName = "timestamp without time zone")]
    public DateTime? Createdat { get; set; }

    [Column("updatedat", TypeName = "timestamp without time zone")]
    public DateTime? Updatedat { get; set; }

    [ForeignKey("Handledby")]
    [InverseProperty("Warrantyclaims")]
    public virtual User? HandledbyNavigation { get; set; }

    [ForeignKey("Warrantyid")]
    [InverseProperty("Warrantyclaims")]
    public virtual Warranty Warranty { get; set; } = null!;
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProductsApi.Models;

[Table("counters")]
[Index("Countercode", Name = "counters_countercode_key", IsUnique = true)]
[Index("Countercode", Name = "ix_counters_code")]
[Index("Staffid", Name = "ix_counters_staff")]
public partial class Counter
{
    [Key]
    [Column("counterid")]
    public int Counterid { get; set; }

    [Column("countercode")]
    [StringLength(20)]
    public string Countercode { get; set; } = null!;

    [Column("countername")]
    [StringLength(100)]
    public string Countername { get; set; } = null!;

    [Column("location")]
    [StringLength(200)]
    public string? Location { get; set; }

    [Column("staffid")]
    public int? Staffid { get; set; }

    [Column("isactive")]
    public bool Isactive { get; set; }

    [Column("isdeleted")]
    public bool Isdeleted { get; set; }

    [Column("createdat", TypeName = "timestamp without time zone")]
    public DateTime? Createdat { get; set; }

    [Column("updatedat", TypeName = "timestamp without time zone")]
    public DateTime? Updatedat { get; set; }

    [InverseProperty("Counter")]
    public virtual ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();

    [InverseProperty("Counter")]
    public virtual ICollection<Report> Reports { get; set; } = new List<Report>();

    [ForeignKey("Staffid")]
    [InverseProperty("Counters")]
    public virtual User? Staff { get; set; }
}

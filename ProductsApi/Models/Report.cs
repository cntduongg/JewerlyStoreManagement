using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProductsApi.Models;

[Table("reports")]
[Index("Reportcode", Name = "ix_reports_code")]
[Index("Startdate", "Enddate", Name = "ix_reports_dates")]
[Index("Reporttype", Name = "ix_reports_type")]
[Index("Reportcode", Name = "reports_reportcode_key", IsUnique = true)]
public partial class Report
{
    [Key]
    [Column("reportid")]
    public int Reportid { get; set; }

    [Column("reportcode")]
    [StringLength(50)]
    public string Reportcode { get; set; } = null!;

    [Column("reportname")]
    [StringLength(200)]
    public string Reportname { get; set; } = null!;

    [Column("reporttype")]
    [StringLength(50)]
    public string Reporttype { get; set; } = null!;

    [Column("startdate")]
    public DateOnly Startdate { get; set; }

    [Column("enddate")]
    public DateOnly Enddate { get; set; }

    [Column("counterid")]
    public int? Counterid { get; set; }

    [Column("staffid")]
    public int? Staffid { get; set; }

    [Column("categoryid")]
    public int? Categoryid { get; set; }

    [Column("reportdata")]
    public string? Reportdata { get; set; }

    [Column("totalrevenue")]
    [Precision(18, 2)]
    public decimal? Totalrevenue { get; set; }

    [Column("totalcost")]
    [Precision(18, 2)]
    public decimal? Totalcost { get; set; }

    [Column("totalprofit")]
    [Precision(18, 2)]
    public decimal? Totalprofit { get; set; }

    [Column("totalinvoices")]
    public int? Totalinvoices { get; set; }

    [Column("totalproductssold")]
    public int? Totalproductssold { get; set; }

    [Column("generatedby")]
    public int Generatedby { get; set; }

    [Column("generatedat", TypeName = "timestamp without time zone")]
    public DateTime? Generatedat { get; set; }

    [ForeignKey("Categoryid")]
    [InverseProperty("Reports")]
    public virtual Category? Category { get; set; }

    [ForeignKey("Counterid")]
    [InverseProperty("Reports")]
    public virtual Counter? Counter { get; set; }

    [ForeignKey("Generatedby")]
    [InverseProperty("ReportGeneratedbyNavigations")]
    public virtual User GeneratedbyNavigation { get; set; } = null!;

    [ForeignKey("Staffid")]
    [InverseProperty("ReportStaffs")]
    public virtual User? Staff { get; set; }
}

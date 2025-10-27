using System;
using System.Collections.Generic;

namespace JewelryService.Models;

public partial class Report
{
    public int Reportid { get; set; }

    public string Reportcode { get; set; } = null!;

    public string Reportname { get; set; } = null!;

    public string Reporttype { get; set; } = null!;

    public DateOnly Startdate { get; set; }

    public DateOnly Enddate { get; set; }

    public int? Counterid { get; set; }

    public int? Staffid { get; set; }

    public int? Categoryid { get; set; }

    public string? Reportdata { get; set; }

    public decimal? Totalrevenue { get; set; }

    public decimal? Totalcost { get; set; }

    public decimal? Totalprofit { get; set; }

    public int? Totalinvoices { get; set; }

    public int? Totalproductssold { get; set; }

    public int Generatedby { get; set; }

    public DateTime? Generatedat { get; set; }

    public virtual Category? Category { get; set; }

    public virtual Counter? Counter { get; set; }

    public virtual User GeneratedbyNavigation { get; set; } = null!;

    public virtual User? Staff { get; set; }
}

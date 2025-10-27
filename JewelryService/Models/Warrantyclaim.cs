using System;
using System.Collections.Generic;

namespace JewelryService.Models;

public partial class Warrantyclaim
{
    public int Claimid { get; set; }

    public string Claimcode { get; set; } = null!;

    public int Warrantyid { get; set; }

    public DateTime? Claimdate { get; set; }

    public string Issuedescription { get; set; } = null!;

    public string? Claimtype { get; set; }

    public int? Handledby { get; set; }

    public string? Resolutiondescription { get; set; }

    public decimal? Repaircost { get; set; }

    public string? Status { get; set; }

    public DateTime? Completeddate { get; set; }

    public DateTime? Createdat { get; set; }

    public DateTime? Updatedat { get; set; }

    public virtual User? HandledbyNavigation { get; set; }

    public virtual Warranty Warranty { get; set; } = null!;
}

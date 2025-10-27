using System;
using System.Collections.Generic;

namespace JewelryService.Models;

public partial class Counter
{
    public int Counterid { get; set; }

    public string Countercode { get; set; } = null!;

    public string Countername { get; set; } = null!;

    public string? Location { get; set; }

    public int? Staffid { get; set; }

    public bool Isactive { get; set; }

    public bool Isdeleted { get; set; }

    public DateTime? Createdat { get; set; }

    public DateTime? Updatedat { get; set; }

    public virtual ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();

    public virtual ICollection<Report> Reports { get; set; } = new List<Report>();

    public virtual User? Staff { get; set; }
}

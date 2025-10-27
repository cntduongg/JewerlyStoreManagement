using System;
using System.Collections.Generic;

namespace JewelryService.Models;

public partial class Systemsetting
{
    public int Settingid { get; set; }

    public string Settingkey { get; set; } = null!;

    public string? Settingvalue { get; set; }

    public string? Datatype { get; set; }

    public string? Category { get; set; }

    public string? Description { get; set; }

    public bool? Iseditable { get; set; }

    public int? Updatedby { get; set; }

    public DateTime? Updatedat { get; set; }

    public virtual User? UpdatedbyNavigation { get; set; }
}

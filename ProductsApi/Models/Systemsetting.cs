using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProductsApi.Models;

[Table("systemsettings")]
[Index("Category", Name = "ix_systemsettings_category")]
[Index("Settingkey", Name = "ix_systemsettings_key")]
[Index("Settingkey", Name = "systemsettings_settingkey_key", IsUnique = true)]
public partial class Systemsetting
{
    [Key]
    [Column("settingid")]
    public int Settingid { get; set; }

    [Column("settingkey")]
    [StringLength(100)]
    public string Settingkey { get; set; } = null!;

    [Column("settingvalue")]
    public string? Settingvalue { get; set; }

    [Column("datatype")]
    [StringLength(20)]
    public string? Datatype { get; set; }

    [Column("category")]
    [StringLength(50)]
    public string? Category { get; set; }

    [Column("description")]
    [StringLength(500)]
    public string? Description { get; set; }

    [Column("iseditable")]
    public bool? Iseditable { get; set; }

    [Column("updatedby")]
    public int? Updatedby { get; set; }

    [Column("updatedat", TypeName = "timestamp without time zone")]
    public DateTime? Updatedat { get; set; }

    [ForeignKey("Updatedby")]
    [InverseProperty("Systemsettings")]
    public virtual User? UpdatedbyNavigation { get; set; }
}

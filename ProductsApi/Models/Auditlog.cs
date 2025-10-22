using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProductsApi.Models;

[Table("auditlogs")]
[Index("Actiondate", Name = "ix_auditlogs_date", AllDescending = true)]
[Index("Tablename", Name = "ix_auditlogs_table")]
[Index("Userid", Name = "ix_auditlogs_user")]
public partial class Auditlog
{
    [Key]
    [Column("logid")]
    public int Logid { get; set; }

    [Column("tablename")]
    [StringLength(50)]
    public string Tablename { get; set; } = null!;

    [Column("recordid")]
    public int Recordid { get; set; }

    [Column("action")]
    [StringLength(20)]
    public string Action { get; set; } = null!;

    [Column("oldvalues")]
    public string? Oldvalues { get; set; }

    [Column("newvalues")]
    public string? Newvalues { get; set; }

    [Column("changedfields")]
    [StringLength(500)]
    public string? Changedfields { get; set; }

    [Column("userid")]
    public int Userid { get; set; }

    [Column("username")]
    [StringLength(50)]
    public string? Username { get; set; }

    [Column("userrole")]
    [StringLength(20)]
    public string? Userrole { get; set; }

    [Column("ipaddress")]
    [StringLength(50)]
    public string? Ipaddress { get; set; }

    [Column("useragent")]
    [StringLength(500)]
    public string? Useragent { get; set; }

    [Column("actiondate", TypeName = "timestamp without time zone")]
    public DateTime? Actiondate { get; set; }

    [ForeignKey("Userid")]
    [InverseProperty("Auditlogs")]
    public virtual User User { get; set; } = null!;
}

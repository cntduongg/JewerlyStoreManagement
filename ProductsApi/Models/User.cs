using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProductsApi.Models;

[Table("users")]
[Index("Role", Name = "ix_users_role")]
[Index("Username", Name = "ix_users_username")]
[Index("Email", Name = "users_email_key", IsUnique = true)]
[Index("Username", Name = "users_username_key", IsUnique = true)]
public partial class User
{
    [Key]
    [Column("userid")]
    public int Userid { get; set; }

    [Column("username")]
    [StringLength(50)]
    public string Username { get; set; } = null!;

    [Column("passwordhash")]
    [StringLength(255)]
    public string Passwordhash { get; set; } = null!;

    [Column("fullname")]
    [StringLength(100)]
    public string Fullname { get; set; } = null!;

    [Column("email")]
    [StringLength(100)]
    public string? Email { get; set; }

    [Column("phone")]
    [StringLength(20)]
    public string? Phone { get; set; }

    [Column("role")]
    [StringLength(20)]
    public string Role { get; set; } = null!;

    [Column("isactive")]
    public bool Isactive { get; set; }

    [Column("isdeleted")]
    public bool Isdeleted { get; set; }

    [Column("createdat", TypeName = "timestamp without time zone")]
    public DateTime? Createdat { get; set; }

    [Column("updatedat", TypeName = "timestamp without time zone")]
    public DateTime? Updatedat { get; set; }

    [InverseProperty("User")]
    public virtual ICollection<Auditlog> Auditlogs { get; set; } = new List<Auditlog>();

    [InverseProperty("Staff")]
    public virtual ICollection<Counter> Counters { get; set; } = new List<Counter>();

    [InverseProperty("UpdatedbyNavigation")]
    public virtual ICollection<Goldprice> Goldprices { get; set; } = new List<Goldprice>();

    [InverseProperty("Staff")]
    public virtual ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();

    [InverseProperty("User")]
    public virtual ICollection<Notification> Notifications { get; set; } = new List<Notification>();

    [InverseProperty("CreatedbyNavigation")]
    public virtual ICollection<Promotion> Promotions { get; set; } = new List<Promotion>();

    [InverseProperty("GeneratedbyNavigation")]
    public virtual ICollection<Report> ReportGeneratedbyNavigations { get; set; } = new List<Report>();

    [InverseProperty("Staff")]
    public virtual ICollection<Report> ReportStaffs { get; set; } = new List<Report>();

    [InverseProperty("CreatedbyNavigation")]
    public virtual ICollection<Stockmovement> Stockmovements { get; set; } = new List<Stockmovement>();

    [InverseProperty("UpdatedbyNavigation")]
    public virtual ICollection<Systemsetting> Systemsettings { get; set; } = new List<Systemsetting>();

    [InverseProperty("HandledbyNavigation")]
    public virtual ICollection<Warrantyclaim> Warrantyclaims { get; set; } = new List<Warrantyclaim>();
}

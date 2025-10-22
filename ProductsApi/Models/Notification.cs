using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProductsApi.Models;

[Table("notifications")]
[Index("Isread", Name = "ix_notifications_read")]
[Index("Notificationtype", Name = "ix_notifications_type")]
[Index("Userid", Name = "ix_notifications_user")]
public partial class Notification
{
    [Key]
    [Column("notificationid")]
    public int Notificationid { get; set; }

    [Column("notificationtype")]
    [StringLength(50)]
    public string Notificationtype { get; set; } = null!;

    [Column("title")]
    [StringLength(200)]
    public string Title { get; set; } = null!;

    [Column("message")]
    [StringLength(1000)]
    public string Message { get; set; } = null!;

    [Column("userid")]
    public int? Userid { get; set; }

    [Column("referencetype")]
    [StringLength(50)]
    public string? Referencetype { get; set; }

    [Column("referenceid")]
    public int? Referenceid { get; set; }

    [Column("isread")]
    public bool? Isread { get; set; }

    [Column("readat", TypeName = "timestamp without time zone")]
    public DateTime? Readat { get; set; }

    [Column("priority")]
    [StringLength(20)]
    public string? Priority { get; set; }

    [Column("createdat", TypeName = "timestamp without time zone")]
    public DateTime? Createdat { get; set; }

    [Column("expiresat", TypeName = "timestamp without time zone")]
    public DateTime? Expiresat { get; set; }

    [ForeignKey("Userid")]
    [InverseProperty("Notifications")]
    public virtual User? User { get; set; }
}

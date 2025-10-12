using System;
using System.Collections.Generic;

namespace ProductsApi.Models;

public partial class Notification
{
    public int Notificationid { get; set; }

    public string Notificationtype { get; set; } = null!;

    public string Title { get; set; } = null!;

    public string Message { get; set; } = null!;

    public int? Userid { get; set; }

    public string? Referencetype { get; set; }

    public int? Referenceid { get; set; }

    public bool? Isread { get; set; }

    public DateTime? Readat { get; set; }

    public string? Priority { get; set; }

    public DateTime? Createdat { get; set; }

    public DateTime? Expiresat { get; set; }

    public virtual User? User { get; set; }
}

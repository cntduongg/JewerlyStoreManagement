using System;
using System.Collections.Generic;

namespace JewelryService.Models;

public partial class User
{
    public int Userid { get; set; }

    public string Username { get; set; } = null!;

    public string Passwordhash { get; set; } = null!;

    public string Fullname { get; set; } = null!;

    public string? Email { get; set; }

    public string? Phone { get; set; }

    public string Role { get; set; } = null!;

    public bool Isactive { get; set; }

    public bool Isdeleted { get; set; }

    public DateTime? Createdat { get; set; }

    public DateTime? Updatedat { get; set; }

    public virtual ICollection<Auditlog> Auditlogs { get; set; } = new List<Auditlog>();

    public virtual ICollection<Counter> Counters { get; set; } = new List<Counter>();

    public virtual ICollection<Goldprice> Goldprices { get; set; } = new List<Goldprice>();

    public virtual ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();

    public virtual ICollection<Notification> Notifications { get; set; } = new List<Notification>();

    public virtual ICollection<Promotion> Promotions { get; set; } = new List<Promotion>();

    public virtual ICollection<Report> ReportGeneratedbyNavigations { get; set; } = new List<Report>();

    public virtual ICollection<Report> ReportStaffs { get; set; } = new List<Report>();

    public virtual ICollection<Stockmovement> Stockmovements { get; set; } = new List<Stockmovement>();

    public virtual ICollection<Systemsetting> Systemsettings { get; set; } = new List<Systemsetting>();

    public virtual ICollection<Warrantyclaim> Warrantyclaims { get; set; } = new List<Warrantyclaim>();
}

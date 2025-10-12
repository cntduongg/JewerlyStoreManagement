using System;
using System.Collections.Generic;

namespace ProductsApi.Models;

public partial class Auditlog
{
    public int logid { get; set; }

    public string Tablename { get; set; } = null!;

    public int Recordid { get; set; }

    public string Action { get; set; } = null!;

    public string? Oldvalues { get; set; }

    public string? Newvalues { get; set; }

    public string? Changedfields { get; set; }

    public int Userid { get; set; }

    public string? Username { get; set; }

    public string? Userrole { get; set; }

    public string? Ipaddress { get; set; }

    public string? Useragent { get; set; }

    public DateTime? Actiondate { get; set; }

    public virtual User User { get; set; } = null!;
}

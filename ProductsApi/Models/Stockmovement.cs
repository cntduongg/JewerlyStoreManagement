using System;
using System.Collections.Generic;

namespace ProductsApi.Models;

public partial class Stockmovement
{
    public int Movementid { get; set; }

    public int Productid { get; set; }

    public string Movementtype { get; set; } = null!;

    public int Quantity { get; set; }

    public string? Referencetype { get; set; }

    public int? Referenceid { get; set; }

    public int Quantitybefore { get; set; }

    public int Quantityafter { get; set; }

    public string? Reason { get; set; }

    public int Createdby { get; set; }

    public DateTime? Createdat { get; set; }

    public virtual User CreatedbyNavigation { get; set; } = null!;

    public virtual Product Product { get; set; } = null!;
}

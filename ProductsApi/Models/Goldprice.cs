using System;
using System.Collections.Generic;

namespace ProductsApi.Models;

public partial class Goldprice
{
    public int Goldpriceid { get; set; }

    public DateOnly Pricedate { get; set; }

    public string Goldtype { get; set; } = null!;

    public decimal Buyprice { get; set; }

    public decimal Sellprice { get; set; }

    public decimal? Worldgoldprice { get; set; }

    public decimal? Exchangerate { get; set; }

    public int Updatedby { get; set; }

    public DateTime? Createdat { get; set; }

    public DateTime? Updatedat { get; set; }

    public virtual User UpdatedbyNavigation { get; set; } = null!;
}

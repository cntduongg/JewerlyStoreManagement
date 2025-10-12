using System;
using System.Collections.Generic;

namespace ProductsApi.Models;

public partial class Productimage
{
    public int Imageid { get; set; }

    public int Productid { get; set; }

    public string Imageurl { get; set; } = null!;

    public string? Imagetype { get; set; }

    public int? Displayorder { get; set; }

    public string? Alttext { get; set; }

    public bool Isdeleted { get; set; }

    public DateTime? Uploadedat { get; set; }

    public virtual Product Product { get; set; } = null!;
}

using System;
using System.Collections.Generic;

namespace ProductsApi.Models;

public partial class Product
{
    public int Productid { get; set; }

    public string Productcode { get; set; } = null!;

    public string Productname { get; set; } = null!;

    public int Categoryid { get; set; }

    public string? Model { get; set; }

    public string Material { get; set; } = null!;

    public decimal? Goldweight { get; set; }

    public decimal? Gemweight { get; set; }

    public decimal? Totalweight { get; set; }

    public decimal? Goldprice { get; set; }

    public decimal? Laborcost { get; set; }

    public decimal? Stoneprice { get; set; }

    public decimal? Costprice { get; set; }

    public decimal Sellingprice { get; set; }

    public bool? Hasgem { get; set; }

    public string? Gemtype { get; set; }

    public string? Gemquality { get; set; }

    public string? Gemcolor { get; set; }

    public string? Gemcut { get; set; }

    public int Stockquantity { get; set; }

    public int? Minstocklevel { get; set; }

    public int? Maxstocklevel { get; set; }

    public string? Mainimageurl { get; set; }

    public string? Shortdescription { get; set; }

    public string? Detaildescription { get; set; }

    public string? Tags { get; set; }

    public bool? Isactive { get; set; }

    public bool Isdeleted { get; set; }

    public DateTime? Createdat { get; set; }

    public DateTime? Updatedat { get; set; }

    public virtual Category Category { get; set; } = null!;

    public virtual ICollection<Invoicedetail> Invoicedetails { get; set; } = new List<Invoicedetail>();

    public virtual ICollection<Productimage> Productimages { get; set; } = new List<Productimage>();

    public virtual ICollection<Stockmovement> Stockmovements { get; set; } = new List<Stockmovement>();

    public virtual ICollection<Warranty> Warranties { get; set; } = new List<Warranty>();
}

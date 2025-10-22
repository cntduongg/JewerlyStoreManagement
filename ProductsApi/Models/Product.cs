using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProductsApi.Models;

[Table("products")]
[Index("Categoryid", Name = "ix_products_category")]
[Index("Productcode", Name = "ix_products_code")]
[Index("Material", Name = "ix_products_material")]
[Index("Stockquantity", Name = "ix_products_stock")]
[Index("Productcode", Name = "products_productcode_key", IsUnique = true)]
public partial class Product
{
    [Key]
    [Column("productid")]
    public int Productid { get; set; }

    [Column("productcode")]
    [StringLength(50)]
    public string Productcode { get; set; } = null!;

    [Column("productname")]
    [StringLength(200)]
    public string Productname { get; set; } = null!;

    [Column("categoryid")]
    public int Categoryid { get; set; }

    [Column("model")]
    [StringLength(50)]
    public string? Model { get; set; }

    [Column("material")]
    [StringLength(50)]
    public string Material { get; set; } = null!;

    [Column("goldweight")]
    [Precision(10, 3)]
    public decimal? Goldweight { get; set; }

    [Column("gemweight")]
    [Precision(10, 3)]
    public decimal? Gemweight { get; set; }

    [Column("totalweight")]
    [Precision(10, 3)]
    public decimal? Totalweight { get; set; }

    [Column("goldprice")]
    [Precision(18, 2)]
    public decimal? Goldprice { get; set; }

    [Column("laborcost")]
    [Precision(18, 2)]
    public decimal? Laborcost { get; set; }

    [Column("stoneprice")]
    [Precision(18, 2)]
    public decimal? Stoneprice { get; set; }

    [Column("costprice")]
    [Precision(18, 2)]
    public decimal? Costprice { get; set; }

    [Column("sellingprice")]
    [Precision(18, 2)]
    public decimal Sellingprice { get; set; }

    [Column("hasgem")]
    public bool? Hasgem { get; set; }

    [Column("gemtype")]
    [StringLength(50)]
    public string? Gemtype { get; set; }

    [Column("gemquality")]
    [StringLength(50)]
    public string? Gemquality { get; set; }

    [Column("gemcolor")]
    [StringLength(50)]
    public string? Gemcolor { get; set; }

    [Column("gemcut")]
    [StringLength(50)]
    public string? Gemcut { get; set; }

    [Column("stockquantity")]
    public int Stockquantity { get; set; }

    [Column("minstocklevel")]
    public int? Minstocklevel { get; set; }

    [Column("maxstocklevel")]
    public int? Maxstocklevel { get; set; }

    [Column("imageurl")]
    [StringLength(500)]
    public string? Imageurl { get; set; }

    [Column("shortdescription")]
    [StringLength(500)]
    public string? Shortdescription { get; set; }

    [Column("detaildescription")]
    public string? Detaildescription { get; set; }

    [Column("tags")]
    [StringLength(500)]
    public string? Tags { get; set; }

    [Column("isactive")]
    public bool? Isactive { get; set; }

    [Column("isdeleted")]
    public bool Isdeleted { get; set; }

    [Column("createdat", TypeName = "timestamp without time zone")]
    public DateTime? Createdat { get; set; }

    [Column("updatedat", TypeName = "timestamp without time zone")]
    public DateTime? Updatedat { get; set; }

    [ForeignKey("Categoryid")]
    [InverseProperty("Products")]
    public virtual Category Category { get; set; } = null!;

    [InverseProperty("Product")]
    public virtual ICollection<Invoicedetail> Invoicedetails { get; set; } = new List<Invoicedetail>();

    [InverseProperty("Product")]
    public virtual ICollection<Stockmovement> Stockmovements { get; set; } = new List<Stockmovement>();

    [InverseProperty("Product")]
    public virtual ICollection<Warranty> Warranties { get; set; } = new List<Warranty>();
}

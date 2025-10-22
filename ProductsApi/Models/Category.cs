using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProductsApi.Models;

[Table("categories")]
[Index("Categorycode", Name = "categories_categorycode_key", IsUnique = true)]
[Index("Categorycode", Name = "ix_categories_code")]
public partial class Category
{
    [Key]
    [Column("categoryid")]
    public int Categoryid { get; set; }

    [Column("categorycode")]
    [StringLength(20)]
    public string Categorycode { get; set; } = null!;

    [Column("categoryname")]
    [StringLength(100)]
    public string Categoryname { get; set; } = null!;

    [Column("description")]
    [StringLength(500)]
    public string? Description { get; set; }

    [Column("displayorder")]
    public int? Displayorder { get; set; }

    [Column("isactive")]
    public bool Isactive { get; set; }

    [Column("createdat", TypeName = "timestamp without time zone")]
    public DateTime? Createdat { get; set; }

    [Column("updatedat", TypeName = "timestamp without time zone")]
    public DateTime? Updatedat { get; set; }

    [InverseProperty("Category")]
    public virtual ICollection<Product> Products { get; set; } = new List<Product>();

    [InverseProperty("Category")]
    public virtual ICollection<Report> Reports { get; set; } = new List<Report>();
}

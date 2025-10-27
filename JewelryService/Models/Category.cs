using System;
using System.Collections.Generic;

namespace JewelryService.Models;

public partial class Category
{
    public int Categoryid { get; set; }

    public string Categorycode { get; set; } = null!;

    public string Categoryname { get; set; } = null!;

    public string? Description { get; set; }

    public int? Displayorder { get; set; }

    public bool Isactive { get; set; }

    public DateTime? Createdat { get; set; }

    public DateTime? Updatedat { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();

    public virtual ICollection<Report> Reports { get; set; } = new List<Report>();
}

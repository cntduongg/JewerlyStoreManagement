using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProductsApi.Models;

[Table("customers")]
[Index("Customercode", Name = "customers_customercode_key", IsUnique = true)]
[Index("Customercode", Name = "ix_customers_code")]
public partial class Customer
{
    [Key]
    [Column("customerid")]
    public int Customerid { get; set; }

    [Column("customercode")]
    [StringLength(20)]
    public string? Customercode { get; set; }

    [Column("fullname")]
    [StringLength(100)]
    public string Fullname { get; set; } = null!;

    [Column("phone")]
    [StringLength(20)]
    public string Phone { get; set; } = null!;

    [Column("email")]
    [StringLength(100)]
    public string? Email { get; set; }

    [Column("address")]
    [StringLength(500)]
    public string? Address { get; set; }

    [Column("dateofbirth")]
    public DateOnly? Dateofbirth { get; set; }

    [Column("gender")]
    [StringLength(10)]
    public string? Gender { get; set; }

    [Column("totalpurchases")]
    [Precision(18, 2)]
    public decimal? Totalpurchases { get; set; }

    [Column("totaltransactions")]
    public int? Totaltransactions { get; set; }

    [Column("lastpurchasedate", TypeName = "timestamp without time zone")]
    public DateTime? Lastpurchasedate { get; set; }

    [Column("notes")]
    public string? Notes { get; set; }

    [Column("isactive")]
    public bool Isactive { get; set; }

    [Column("isdeleted")]
    public bool Isdeleted { get; set; }

    [Column("createdat", TypeName = "timestamp without time zone")]
    public DateTime? Createdat { get; set; }

    [Column("updatedat", TypeName = "timestamp without time zone")]
    public DateTime? Updatedat { get; set; }

    [InverseProperty("Customer")]
    public virtual ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();

    [InverseProperty("Customer")]
    public virtual ICollection<Warranty> Warranties { get; set; } = new List<Warranty>();
}

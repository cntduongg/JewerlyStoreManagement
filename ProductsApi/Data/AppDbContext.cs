using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using ProductsApi.Models;

namespace ProductsApi.Data;

public partial class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Auditlog> Auditlogs { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Counter> Counters { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Goldprice> Goldprices { get; set; }

    public virtual DbSet<Invoice> Invoices { get; set; }

    public virtual DbSet<Invoicedetail> Invoicedetails { get; set; }

    public virtual DbSet<Notification> Notifications { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<Promotion> Promotions { get; set; }

    public virtual DbSet<Report> Reports { get; set; }

    public virtual DbSet<Stockmovement> Stockmovements { get; set; }

    public virtual DbSet<Systemsetting> Systemsettings { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<Warranty> Warranties { get; set; }

    public virtual DbSet<Warrantyclaim> Warrantyclaims { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=jewelrystore;Username=postgres;Password=123456");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Auditlog>(entity =>
        {
            entity.HasKey(e => e.Logid).HasName("auditlogs_pkey");

            entity.Property(e => e.Actiondate).HasDefaultValueSql("CURRENT_TIMESTAMP");

            entity.HasOne(d => d.User).WithMany(p => p.Auditlogs)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("auditlogs_userid_fkey");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.Categoryid).HasName("categories_pkey");

            entity.Property(e => e.Createdat).HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.Property(e => e.Displayorder).HasDefaultValue(0);
            entity.Property(e => e.Isactive).HasDefaultValue(true);
            entity.Property(e => e.Updatedat).HasDefaultValueSql("CURRENT_TIMESTAMP");
        });

        modelBuilder.Entity<Counter>(entity =>
        {
            entity.HasKey(e => e.Counterid).HasName("counters_pkey");

            entity.Property(e => e.Createdat).HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.Property(e => e.Isactive).HasDefaultValue(true);
            entity.Property(e => e.Isdeleted).HasDefaultValue(false);
            entity.Property(e => e.Updatedat).HasDefaultValueSql("CURRENT_TIMESTAMP");

            entity.HasOne(d => d.Staff).WithMany(p => p.Counters).HasConstraintName("counters_staffid_fkey");
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.Customerid).HasName("customers_pkey");

            entity.HasIndex(e => e.Phone, "ix_customers_phone")
                .IsUnique()
                .HasFilter("(isdeleted = false)");

            entity.Property(e => e.Createdat).HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.Property(e => e.Isactive).HasDefaultValue(true);
            entity.Property(e => e.Isdeleted).HasDefaultValue(false);
            entity.Property(e => e.Totalpurchases).HasDefaultValueSql("0");
            entity.Property(e => e.Totaltransactions).HasDefaultValue(0);
            entity.Property(e => e.Updatedat).HasDefaultValueSql("CURRENT_TIMESTAMP");
        });

        modelBuilder.Entity<Goldprice>(entity =>
        {
            entity.HasKey(e => e.Goldpriceid).HasName("goldprices_pkey");

            entity.Property(e => e.Createdat).HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.Property(e => e.Goldtype).HasDefaultValueSql("'24K'::character varying");
            entity.Property(e => e.Updatedat).HasDefaultValueSql("CURRENT_TIMESTAMP");

            entity.HasOne(d => d.UpdatedbyNavigation).WithMany(p => p.Goldprices)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("goldprices_updatedby_fkey");
        });

        modelBuilder.Entity<Invoice>(entity =>
        {
            entity.HasKey(e => e.Invoiceid).HasName("invoices_pkey");

            entity.Property(e => e.Createdat).HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.Property(e => e.Discountamount).HasDefaultValueSql("0");
            entity.Property(e => e.Invoicedate).HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.Property(e => e.Invoicestatus).HasDefaultValueSql("'Completed'::character varying");
            entity.Property(e => e.Isdeleted).HasDefaultValue(false);
            entity.Property(e => e.Paidamount).HasDefaultValueSql("0");
            entity.Property(e => e.Paymentmethod).HasDefaultValueSql("'Cash'::character varying");
            entity.Property(e => e.Paymentstatus).HasDefaultValueSql("'Paid'::character varying");
            entity.Property(e => e.Remainingamount).HasDefaultValueSql("0");
            entity.Property(e => e.Taxamount).HasDefaultValueSql("0");
            entity.Property(e => e.Taxrate).HasDefaultValueSql("0");
            entity.Property(e => e.Updatedat).HasDefaultValueSql("CURRENT_TIMESTAMP");

            entity.HasOne(d => d.Counter).WithMany(p => p.Invoices).HasConstraintName("invoices_counterid_fkey");

            entity.HasOne(d => d.Customer).WithMany(p => p.Invoices).HasConstraintName("invoices_customerid_fkey");

            entity.HasOne(d => d.Promotion).WithMany(p => p.Invoices).HasConstraintName("invoices_promotionid_fkey");

            entity.HasOne(d => d.Staff).WithMany(p => p.Invoices)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("invoices_staffid_fkey");
        });

        modelBuilder.Entity<Invoicedetail>(entity =>
        {
            entity.HasKey(e => e.Invoicedetailid).HasName("invoicedetails_pkey");

            entity.Property(e => e.Discountamount).HasDefaultValueSql("0");
            entity.Property(e => e.Discountpercent).HasDefaultValueSql("0");
            entity.Property(e => e.Gemweight).HasDefaultValueSql("0");
            entity.Property(e => e.Goldprice).HasDefaultValueSql("0");
            entity.Property(e => e.Goldweight).HasDefaultValueSql("0");
            entity.Property(e => e.Hasgem).HasDefaultValue(false);
            entity.Property(e => e.Laborcost).HasDefaultValueSql("0");
            entity.Property(e => e.Stoneprice).HasDefaultValueSql("0");

            entity.HasOne(d => d.Invoice).WithMany(p => p.Invoicedetails).HasConstraintName("invoicedetails_invoiceid_fkey");

            entity.HasOne(d => d.Product).WithMany(p => p.Invoicedetails)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("invoicedetails_productid_fkey");
        });

        modelBuilder.Entity<Notification>(entity =>
        {
            entity.HasKey(e => e.Notificationid).HasName("notifications_pkey");

            entity.Property(e => e.Createdat).HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.Property(e => e.Isread).HasDefaultValue(false);
            entity.Property(e => e.Priority).HasDefaultValueSql("'Normal'::character varying");

            entity.HasOne(d => d.User).WithMany(p => p.Notifications).HasConstraintName("notifications_userid_fkey");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.Productid).HasName("products_pkey");

            entity.Property(e => e.Costprice).HasDefaultValueSql("0");
            entity.Property(e => e.Createdat).HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.Property(e => e.Gemweight).HasDefaultValueSql("0");
            entity.Property(e => e.Goldprice).HasDefaultValueSql("0");
            entity.Property(e => e.Goldweight).HasDefaultValueSql("0");
            entity.Property(e => e.Hasgem).HasDefaultValue(false);
            entity.Property(e => e.Isactive).HasDefaultValue(true);
            entity.Property(e => e.Isdeleted).HasDefaultValue(false);
            entity.Property(e => e.Laborcost).HasDefaultValueSql("0");
            entity.Property(e => e.Maxstocklevel).HasDefaultValue(100);
            entity.Property(e => e.Minstocklevel).HasDefaultValue(5);
            entity.Property(e => e.Stockquantity).HasDefaultValue(0);
            entity.Property(e => e.Stoneprice).HasDefaultValueSql("0");
            entity.Property(e => e.Totalweight).HasDefaultValueSql("0");
            entity.Property(e => e.Updatedat).HasDefaultValueSql("CURRENT_TIMESTAMP");

            entity.HasOne(d => d.Category).WithMany(p => p.Products)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("products_categoryid_fkey");
        });

        modelBuilder.Entity<Promotion>(entity =>
        {
            entity.HasKey(e => e.Promotionid).HasName("promotions_pkey");

            entity.Property(e => e.Createdat).HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.Property(e => e.Isactive).HasDefaultValue(true);
            entity.Property(e => e.Isdeleted).HasDefaultValue(false);
            entity.Property(e => e.Minpurchaseamount).HasDefaultValueSql("0");
            entity.Property(e => e.Updatedat).HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.Property(e => e.Usagecount).HasDefaultValue(0);
            entity.Property(e => e.Usagelimitpercustomer).HasDefaultValue(1);

            entity.HasOne(d => d.CreatedbyNavigation).WithMany(p => p.Promotions)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("promotions_createdby_fkey");
        });

        modelBuilder.Entity<Report>(entity =>
        {
            entity.HasKey(e => e.Reportid).HasName("reports_pkey");

            entity.Property(e => e.Generatedat).HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.Property(e => e.Totalcost).HasDefaultValueSql("0");
            entity.Property(e => e.Totalinvoices).HasDefaultValue(0);
            entity.Property(e => e.Totalproductssold).HasDefaultValue(0);
            entity.Property(e => e.Totalprofit).HasDefaultValueSql("0");
            entity.Property(e => e.Totalrevenue).HasDefaultValueSql("0");

            entity.HasOne(d => d.Category).WithMany(p => p.Reports).HasConstraintName("reports_categoryid_fkey");

            entity.HasOne(d => d.Counter).WithMany(p => p.Reports).HasConstraintName("reports_counterid_fkey");

            entity.HasOne(d => d.GeneratedbyNavigation).WithMany(p => p.ReportGeneratedbyNavigations)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("reports_generatedby_fkey");

            entity.HasOne(d => d.Staff).WithMany(p => p.ReportStaffs).HasConstraintName("reports_staffid_fkey");
        });

        modelBuilder.Entity<Stockmovement>(entity =>
        {
            entity.HasKey(e => e.Movementid).HasName("stockmovements_pkey");

            entity.Property(e => e.Createdat).HasDefaultValueSql("CURRENT_TIMESTAMP");

            entity.HasOne(d => d.CreatedbyNavigation).WithMany(p => p.Stockmovements)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("stockmovements_createdby_fkey");

            entity.HasOne(d => d.Product).WithMany(p => p.Stockmovements)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("stockmovements_productid_fkey");
        });

        modelBuilder.Entity<Systemsetting>(entity =>
        {
            entity.HasKey(e => e.Settingid).HasName("systemsettings_pkey");

            entity.Property(e => e.Datatype).HasDefaultValueSql("'String'::character varying");
            entity.Property(e => e.Iseditable).HasDefaultValue(true);
            entity.Property(e => e.Updatedat).HasDefaultValueSql("CURRENT_TIMESTAMP");

            entity.HasOne(d => d.UpdatedbyNavigation).WithMany(p => p.Systemsettings).HasConstraintName("systemsettings_updatedby_fkey");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Userid).HasName("users_pkey");

            entity.Property(e => e.Createdat).HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.Property(e => e.Isactive).HasDefaultValue(true);
            entity.Property(e => e.Isdeleted).HasDefaultValue(false);
            entity.Property(e => e.Updatedat).HasDefaultValueSql("CURRENT_TIMESTAMP");
        });

        modelBuilder.Entity<Warranty>(entity =>
        {
            entity.HasKey(e => e.Warrantyid).HasName("warranties_pkey");

            entity.Property(e => e.Claimcount).HasDefaultValue(0);
            entity.Property(e => e.Createdat).HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.Property(e => e.Status).HasDefaultValueSql("'Active'::character varying");
            entity.Property(e => e.Updatedat).HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.Property(e => e.Warrantytype).HasDefaultValueSql("'Standard'::character varying");

            entity.HasOne(d => d.Customer).WithMany(p => p.Warranties)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("warranties_customerid_fkey");

            entity.HasOne(d => d.Invoicedetail).WithMany(p => p.Warranties)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("warranties_invoicedetailid_fkey");

            entity.HasOne(d => d.Product).WithMany(p => p.Warranties)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("warranties_productid_fkey");
        });

        modelBuilder.Entity<Warrantyclaim>(entity =>
        {
            entity.HasKey(e => e.Claimid).HasName("warrantyclaims_pkey");

            entity.Property(e => e.Claimdate).HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.Property(e => e.Createdat).HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.Property(e => e.Repaircost).HasDefaultValueSql("0");
            entity.Property(e => e.Status).HasDefaultValueSql("'Pending'::character varying");
            entity.Property(e => e.Updatedat).HasDefaultValueSql("CURRENT_TIMESTAMP");

            entity.HasOne(d => d.HandledbyNavigation).WithMany(p => p.Warrantyclaims).HasConstraintName("warrantyclaims_handledby_fkey");

            entity.HasOne(d => d.Warranty).WithMany(p => p.Warrantyclaims)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("warrantyclaims_warrantyid_fkey");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

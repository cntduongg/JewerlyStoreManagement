using System;
using System.Collections.Generic;
using JewelryService.Models;
using Microsoft.EntityFrameworkCore;

namespace JewelryService.Data;

public partial class JewelryStoreContext : DbContext
{
    public JewelryStoreContext()
    {
    }

    public JewelryStoreContext(DbContextOptions<JewelryStoreContext> options)
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

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Auditlog>(entity =>
        {
            entity.HasKey(e => e.Logid).HasName("auditlogs_pkey");

            entity.ToTable("auditlogs");

            entity.HasIndex(e => e.Actiondate, "ix_auditlogs_date").IsDescending();

            entity.HasIndex(e => e.Tablename, "ix_auditlogs_table");

            entity.HasIndex(e => e.Userid, "ix_auditlogs_user");

            entity.Property(e => e.Logid).HasColumnName("logid");
            entity.Property(e => e.Action)
                .HasMaxLength(20)
                .HasColumnName("action");
            entity.Property(e => e.Actiondate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("actiondate");
            entity.Property(e => e.Changedfields)
                .HasMaxLength(500)
                .HasColumnName("changedfields");
            entity.Property(e => e.Ipaddress)
                .HasMaxLength(50)
                .HasColumnName("ipaddress");
            entity.Property(e => e.Newvalues).HasColumnName("newvalues");
            entity.Property(e => e.Oldvalues).HasColumnName("oldvalues");
            entity.Property(e => e.Recordid).HasColumnName("recordid");
            entity.Property(e => e.Tablename)
                .HasMaxLength(50)
                .HasColumnName("tablename");
            entity.Property(e => e.Useragent)
                .HasMaxLength(500)
                .HasColumnName("useragent");
            entity.Property(e => e.Userid).HasColumnName("userid");
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .HasColumnName("username");
            entity.Property(e => e.Userrole)
                .HasMaxLength(20)
                .HasColumnName("userrole");

            entity.HasOne(d => d.User).WithMany(p => p.Auditlogs)
                .HasForeignKey(d => d.Userid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("auditlogs_userid_fkey");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.Categoryid).HasName("categories_pkey");

            entity.ToTable("categories");

            entity.HasIndex(e => e.Categorycode, "categories_categorycode_key").IsUnique();

            entity.HasIndex(e => e.Categorycode, "ix_categories_code");

            entity.Property(e => e.Categoryid).HasColumnName("categoryid");
            entity.Property(e => e.Categorycode)
                .HasMaxLength(20)
                .HasColumnName("categorycode");
            entity.Property(e => e.Categoryname)
                .HasMaxLength(100)
                .HasColumnName("categoryname");
            entity.Property(e => e.Createdat)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("createdat");
            entity.Property(e => e.Description)
                .HasMaxLength(500)
                .HasColumnName("description");
            entity.Property(e => e.Displayorder)
                .HasDefaultValue(0)
                .HasColumnName("displayorder");
            entity.Property(e => e.Isactive)
                .HasDefaultValue(true)
                .HasColumnName("isactive");
            entity.Property(e => e.Updatedat)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updatedat");
        });

        modelBuilder.Entity<Counter>(entity =>
        {
            entity.HasKey(e => e.Counterid).HasName("counters_pkey");

            entity.ToTable("counters");

            entity.HasIndex(e => e.Countercode, "counters_countercode_key").IsUnique();

            entity.HasIndex(e => e.Countercode, "ix_counters_code");

            entity.HasIndex(e => e.Staffid, "ix_counters_staff");

            entity.Property(e => e.Counterid).HasColumnName("counterid");
            entity.Property(e => e.Countercode)
                .HasMaxLength(20)
                .HasColumnName("countercode");
            entity.Property(e => e.Countername)
                .HasMaxLength(100)
                .HasColumnName("countername");
            entity.Property(e => e.Createdat)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("createdat");
            entity.Property(e => e.Isactive)
                .HasDefaultValue(true)
                .HasColumnName("isactive");
            entity.Property(e => e.Isdeleted)
                .HasDefaultValue(false)
                .HasColumnName("isdeleted");
            entity.Property(e => e.Location)
                .HasMaxLength(200)
                .HasColumnName("location");
            entity.Property(e => e.Staffid).HasColumnName("staffid");
            entity.Property(e => e.Updatedat)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updatedat");

            entity.HasOne(d => d.Staff).WithMany(p => p.Counters)
                .HasForeignKey(d => d.Staffid)
                .HasConstraintName("counters_staffid_fkey");
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.Customerid).HasName("customers_pkey");

            entity.ToTable("customers");

            entity.HasIndex(e => e.Customercode, "customers_customercode_key").IsUnique();

            entity.HasIndex(e => e.Customercode, "ix_customers_code");

            entity.HasIndex(e => e.Phone, "ix_customers_phone")
                .IsUnique()
                .HasFilter("(isdeleted = false)");

            entity.Property(e => e.Customerid).HasColumnName("customerid");
            entity.Property(e => e.Address)
                .HasMaxLength(500)
                .HasColumnName("address");
            entity.Property(e => e.Createdat)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("createdat");
            entity.Property(e => e.Customercode)
                .HasMaxLength(20)
                .HasColumnName("customercode");
            entity.Property(e => e.Dateofbirth).HasColumnName("dateofbirth");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("email");
            entity.Property(e => e.Fullname)
                .HasMaxLength(100)
                .HasColumnName("fullname");
            entity.Property(e => e.Gender)
                .HasMaxLength(10)
                .HasColumnName("gender");
            entity.Property(e => e.Isactive)
                .HasDefaultValue(true)
                .HasColumnName("isactive");
            entity.Property(e => e.Isdeleted)
                .HasDefaultValue(false)
                .HasColumnName("isdeleted");
            entity.Property(e => e.Lastpurchasedate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("lastpurchasedate");
            entity.Property(e => e.Notes).HasColumnName("notes");
            entity.Property(e => e.Phone)
                .HasMaxLength(20)
                .HasColumnName("phone");
            entity.Property(e => e.Totalpurchases)
                .HasPrecision(18, 2)
                .HasDefaultValueSql("0")
                .HasColumnName("totalpurchases");
            entity.Property(e => e.Totaltransactions)
                .HasDefaultValue(0)
                .HasColumnName("totaltransactions");
            entity.Property(e => e.Updatedat)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updatedat");
        });

        modelBuilder.Entity<Goldprice>(entity =>
        {
            entity.HasKey(e => e.Goldpriceid).HasName("goldprices_pkey");

            entity.ToTable("goldprices");

            entity.HasIndex(e => e.Pricedate, "ix_goldprices_date").IsDescending();

            entity.HasIndex(e => e.Goldtype, "ix_goldprices_type");

            entity.HasIndex(e => new { e.Pricedate, e.Goldtype }, "uq_goldprice_date_type").IsUnique();

            entity.Property(e => e.Goldpriceid).HasColumnName("goldpriceid");
            entity.Property(e => e.Buyprice)
                .HasPrecision(18, 2)
                .HasColumnName("buyprice");
            entity.Property(e => e.Createdat)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("createdat");
            entity.Property(e => e.Exchangerate)
                .HasPrecision(18, 4)
                .HasColumnName("exchangerate");
            entity.Property(e => e.Goldtype)
                .HasMaxLength(50)
                .HasDefaultValueSql("'24K'::character varying")
                .HasColumnName("goldtype");
            entity.Property(e => e.Pricedate).HasColumnName("pricedate");
            entity.Property(e => e.Sellprice)
                .HasPrecision(18, 2)
                .HasColumnName("sellprice");
            entity.Property(e => e.Updatedat)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updatedat");
            entity.Property(e => e.Updatedby).HasColumnName("updatedby");
            entity.Property(e => e.Worldgoldprice)
                .HasPrecision(18, 2)
                .HasColumnName("worldgoldprice");

            entity.HasOne(d => d.UpdatedbyNavigation).WithMany(p => p.Goldprices)
                .HasForeignKey(d => d.Updatedby)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("goldprices_updatedby_fkey");
        });

        modelBuilder.Entity<Invoice>(entity =>
        {
            entity.HasKey(e => e.Invoiceid).HasName("invoices_pkey");

            entity.ToTable("invoices");

            entity.HasIndex(e => e.Invoicecode, "invoices_invoicecode_key").IsUnique();

            entity.HasIndex(e => e.Invoicecode, "ix_invoices_code");

            entity.HasIndex(e => e.Customerid, "ix_invoices_customer");

            entity.HasIndex(e => e.Invoicedate, "ix_invoices_date").IsDescending();

            entity.HasIndex(e => e.Staffid, "ix_invoices_staff");

            entity.HasIndex(e => e.Invoicestatus, "ix_invoices_status");

            entity.HasIndex(e => e.Invoicetype, "ix_invoices_type");

            entity.Property(e => e.Invoiceid).HasColumnName("invoiceid");
            entity.Property(e => e.Counterid).HasColumnName("counterid");
            entity.Property(e => e.Createdat)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("createdat");
            entity.Property(e => e.Customerid).HasColumnName("customerid");
            entity.Property(e => e.Customernotes)
                .HasMaxLength(1000)
                .HasColumnName("customernotes");
            entity.Property(e => e.Discountamount)
                .HasPrecision(18, 2)
                .HasDefaultValueSql("0")
                .HasColumnName("discountamount");
            entity.Property(e => e.Internalnotes)
                .HasMaxLength(1000)
                .HasColumnName("internalnotes");
            entity.Property(e => e.Invoicecode)
                .HasMaxLength(50)
                .HasColumnName("invoicecode");
            entity.Property(e => e.Invoicedate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("invoicedate");
            entity.Property(e => e.Invoicestatus)
                .HasMaxLength(20)
                .HasDefaultValueSql("'Completed'::character varying")
                .HasColumnName("invoicestatus");
            entity.Property(e => e.Invoicetype)
                .HasMaxLength(10)
                .HasColumnName("invoicetype");
            entity.Property(e => e.Isdeleted)
                .HasDefaultValue(false)
                .HasColumnName("isdeleted");
            entity.Property(e => e.Paidamount)
                .HasPrecision(18, 2)
                .HasDefaultValueSql("0")
                .HasColumnName("paidamount");
            entity.Property(e => e.Paymentmethod)
                .HasMaxLength(50)
                .HasDefaultValueSql("'Cash'::character varying")
                .HasColumnName("paymentmethod");
            entity.Property(e => e.Paymentstatus)
                .HasMaxLength(20)
                .HasDefaultValueSql("'Paid'::character varying")
                .HasColumnName("paymentstatus");
            entity.Property(e => e.Promotioncode)
                .HasMaxLength(50)
                .HasColumnName("promotioncode");
            entity.Property(e => e.Promotionid).HasColumnName("promotionid");
            entity.Property(e => e.Remainingamount)
                .HasPrecision(18, 2)
                .HasDefaultValueSql("0")
                .HasColumnName("remainingamount");
            entity.Property(e => e.Staffid).HasColumnName("staffid");
            entity.Property(e => e.Subtotal)
                .HasPrecision(18, 2)
                .HasColumnName("subtotal");
            entity.Property(e => e.Taxamount)
                .HasPrecision(18, 2)
                .HasDefaultValueSql("0")
                .HasColumnName("taxamount");
            entity.Property(e => e.Taxrate)
                .HasPrecision(5, 2)
                .HasDefaultValueSql("0")
                .HasColumnName("taxrate");
            entity.Property(e => e.Totalamount)
                .HasPrecision(18, 2)
                .HasColumnName("totalamount");
            entity.Property(e => e.Updatedat)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updatedat");

            entity.HasOne(d => d.Counter).WithMany(p => p.Invoices)
                .HasForeignKey(d => d.Counterid)
                .HasConstraintName("invoices_counterid_fkey");

            entity.HasOne(d => d.Customer).WithMany(p => p.Invoices)
                .HasForeignKey(d => d.Customerid)
                .HasConstraintName("invoices_customerid_fkey");

            entity.HasOne(d => d.Promotion).WithMany(p => p.Invoices)
                .HasForeignKey(d => d.Promotionid)
                .HasConstraintName("invoices_promotionid_fkey");

            entity.HasOne(d => d.Staff).WithMany(p => p.Invoices)
                .HasForeignKey(d => d.Staffid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("invoices_staffid_fkey");
        });

        modelBuilder.Entity<Invoicedetail>(entity =>
        {
            entity.HasKey(e => e.Invoicedetailid).HasName("invoicedetails_pkey");

            entity.ToTable("invoicedetails");

            entity.HasIndex(e => e.Invoiceid, "ix_invoicedetails_invoice");

            entity.HasIndex(e => e.Productid, "ix_invoicedetails_product");

            entity.Property(e => e.Invoicedetailid).HasColumnName("invoicedetailid");
            entity.Property(e => e.Discountamount)
                .HasPrecision(18, 2)
                .HasDefaultValueSql("0")
                .HasColumnName("discountamount");
            entity.Property(e => e.Discountpercent)
                .HasPrecision(5, 2)
                .HasDefaultValueSql("0")
                .HasColumnName("discountpercent");
            entity.Property(e => e.Gemweight)
                .HasPrecision(10, 3)
                .HasDefaultValueSql("0")
                .HasColumnName("gemweight");
            entity.Property(e => e.Goldprice)
                .HasPrecision(18, 2)
                .HasDefaultValueSql("0")
                .HasColumnName("goldprice");
            entity.Property(e => e.Goldweight)
                .HasPrecision(10, 3)
                .HasDefaultValueSql("0")
                .HasColumnName("goldweight");
            entity.Property(e => e.Hasgem)
                .HasDefaultValue(false)
                .HasColumnName("hasgem");
            entity.Property(e => e.Invoiceid).HasColumnName("invoiceid");
            entity.Property(e => e.Laborcost)
                .HasPrecision(18, 2)
                .HasDefaultValueSql("0")
                .HasColumnName("laborcost");
            entity.Property(e => e.Linetotal)
                .HasPrecision(18, 2)
                .HasColumnName("linetotal");
            entity.Property(e => e.Material)
                .HasMaxLength(50)
                .HasColumnName("material");
            entity.Property(e => e.Notes)
                .HasMaxLength(500)
                .HasColumnName("notes");
            entity.Property(e => e.Productcode)
                .HasMaxLength(50)
                .HasColumnName("productcode");
            entity.Property(e => e.Productid).HasColumnName("productid");
            entity.Property(e => e.Productname)
                .HasMaxLength(200)
                .HasColumnName("productname");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
            entity.Property(e => e.Stoneprice)
                .HasPrecision(18, 2)
                .HasDefaultValueSql("0")
                .HasColumnName("stoneprice");
            entity.Property(e => e.Unitprice)
                .HasPrecision(18, 2)
                .HasColumnName("unitprice");

            entity.HasOne(d => d.Invoice).WithMany(p => p.Invoicedetails)
                .HasForeignKey(d => d.Invoiceid)
                .HasConstraintName("invoicedetails_invoiceid_fkey");

            entity.HasOne(d => d.Product).WithMany(p => p.Invoicedetails)
                .HasForeignKey(d => d.Productid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("invoicedetails_productid_fkey");
        });

        modelBuilder.Entity<Notification>(entity =>
        {
            entity.HasKey(e => e.Notificationid).HasName("notifications_pkey");

            entity.ToTable("notifications");

            entity.HasIndex(e => e.Isread, "ix_notifications_read");

            entity.HasIndex(e => e.Notificationtype, "ix_notifications_type");

            entity.HasIndex(e => e.Userid, "ix_notifications_user");

            entity.Property(e => e.Notificationid).HasColumnName("notificationid");
            entity.Property(e => e.Createdat)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("createdat");
            entity.Property(e => e.Expiresat)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("expiresat");
            entity.Property(e => e.Isread)
                .HasDefaultValue(false)
                .HasColumnName("isread");
            entity.Property(e => e.Message)
                .HasMaxLength(1000)
                .HasColumnName("message");
            entity.Property(e => e.Notificationtype)
                .HasMaxLength(50)
                .HasColumnName("notificationtype");
            entity.Property(e => e.Priority)
                .HasMaxLength(20)
                .HasDefaultValueSql("'Normal'::character varying")
                .HasColumnName("priority");
            entity.Property(e => e.Readat)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("readat");
            entity.Property(e => e.Referenceid).HasColumnName("referenceid");
            entity.Property(e => e.Referencetype)
                .HasMaxLength(50)
                .HasColumnName("referencetype");
            entity.Property(e => e.Title)
                .HasMaxLength(200)
                .HasColumnName("title");
            entity.Property(e => e.Userid).HasColumnName("userid");

            entity.HasOne(d => d.User).WithMany(p => p.Notifications)
                .HasForeignKey(d => d.Userid)
                .HasConstraintName("notifications_userid_fkey");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.Productid).HasName("products_pkey");

            entity.ToTable("products");

            entity.HasIndex(e => e.Categoryid, "ix_products_category");

            entity.HasIndex(e => e.Productcode, "ix_products_code");

            entity.HasIndex(e => e.Material, "ix_products_material");

            entity.HasIndex(e => e.Stockquantity, "ix_products_stock");

            entity.HasIndex(e => e.Productcode, "products_productcode_key").IsUnique();

            entity.Property(e => e.Productid).HasColumnName("productid");
            entity.Property(e => e.Categoryid).HasColumnName("categoryid");
            entity.Property(e => e.Costprice)
                .HasPrecision(18, 2)
                .HasDefaultValueSql("0")
                .HasColumnName("costprice");
            entity.Property(e => e.Createdat)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("createdat");
            entity.Property(e => e.Detaildescription).HasColumnName("detaildescription");
            entity.Property(e => e.Gemcolor)
                .HasMaxLength(50)
                .HasColumnName("gemcolor");
            entity.Property(e => e.Gemcut)
                .HasMaxLength(50)
                .HasColumnName("gemcut");
            entity.Property(e => e.Gemquality)
                .HasMaxLength(50)
                .HasColumnName("gemquality");
            entity.Property(e => e.Gemtype)
                .HasMaxLength(50)
                .HasColumnName("gemtype");
            entity.Property(e => e.Gemweight)
                .HasPrecision(10, 3)
                .HasDefaultValueSql("0")
                .HasColumnName("gemweight");
            entity.Property(e => e.Goldprice)
                .HasPrecision(18, 2)
                .HasDefaultValueSql("0")
                .HasColumnName("goldprice");
            entity.Property(e => e.Goldweight)
                .HasPrecision(10, 3)
                .HasDefaultValueSql("0")
                .HasColumnName("goldweight");
            entity.Property(e => e.Hasgem)
                .HasDefaultValue(false)
                .HasColumnName("hasgem");
            entity.Property(e => e.Imageurl)
                .HasMaxLength(500)
                .HasColumnName("imageurl");
            entity.Property(e => e.Isactive)
                .HasDefaultValue(true)
                .HasColumnName("isactive");
            entity.Property(e => e.Isdeleted)
                .HasDefaultValue(false)
                .HasColumnName("isdeleted");
            entity.Property(e => e.Laborcost)
                .HasPrecision(18, 2)
                .HasDefaultValueSql("0")
                .HasColumnName("laborcost");
            entity.Property(e => e.Material)
                .HasMaxLength(50)
                .HasColumnName("material");
            entity.Property(e => e.Maxstocklevel)
                .HasDefaultValue(100)
                .HasColumnName("maxstocklevel");
            entity.Property(e => e.Minstocklevel)
                .HasDefaultValue(5)
                .HasColumnName("minstocklevel");
            entity.Property(e => e.Model)
                .HasMaxLength(50)
                .HasColumnName("model");
            entity.Property(e => e.Productcode)
                .HasMaxLength(50)
                .HasColumnName("productcode");
            entity.Property(e => e.Productname)
                .HasMaxLength(200)
                .HasColumnName("productname");
            entity.Property(e => e.Sellingprice)
                .HasPrecision(18, 2)
                .HasColumnName("sellingprice");
            entity.Property(e => e.Shortdescription)
                .HasMaxLength(500)
                .HasColumnName("shortdescription");
            entity.Property(e => e.Stockquantity)
                .HasDefaultValue(0)
                .HasColumnName("stockquantity");
            entity.Property(e => e.Stoneprice)
                .HasPrecision(18, 2)
                .HasDefaultValueSql("0")
                .HasColumnName("stoneprice");
            entity.Property(e => e.Tags)
                .HasMaxLength(500)
                .HasColumnName("tags");
            entity.Property(e => e.Totalweight)
                .HasPrecision(10, 3)
                .HasDefaultValueSql("0")
                .HasColumnName("totalweight");
            entity.Property(e => e.Updatedat)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updatedat");

            entity.HasOne(d => d.Category).WithMany(p => p.Products)
                .HasForeignKey(d => d.Categoryid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("products_categoryid_fkey");
        });

        modelBuilder.Entity<Promotion>(entity =>
        {
            entity.HasKey(e => e.Promotionid).HasName("promotions_pkey");

            entity.ToTable("promotions");

            entity.HasIndex(e => e.Promotioncode, "ix_promotions_code");

            entity.HasIndex(e => new { e.Startdate, e.Enddate }, "ix_promotions_dates");

            entity.HasIndex(e => e.Promotioncode, "promotions_promotioncode_key").IsUnique();

            entity.Property(e => e.Promotionid).HasColumnName("promotionid");
            entity.Property(e => e.Applicablecategories)
                .HasMaxLength(500)
                .HasColumnName("applicablecategories");
            entity.Property(e => e.Applicableproducts)
                .HasMaxLength(500)
                .HasColumnName("applicableproducts");
            entity.Property(e => e.Createdat)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("createdat");
            entity.Property(e => e.Createdby).HasColumnName("createdby");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Discounttype)
                .HasMaxLength(20)
                .HasColumnName("discounttype");
            entity.Property(e => e.Discountvalue)
                .HasPrecision(18, 2)
                .HasColumnName("discountvalue");
            entity.Property(e => e.Enddate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("enddate");
            entity.Property(e => e.Isactive)
                .HasDefaultValue(true)
                .HasColumnName("isactive");
            entity.Property(e => e.Isdeleted)
                .HasDefaultValue(false)
                .HasColumnName("isdeleted");
            entity.Property(e => e.Maxdiscountamount)
                .HasPrecision(18, 2)
                .HasColumnName("maxdiscountamount");
            entity.Property(e => e.Minpurchaseamount)
                .HasPrecision(18, 2)
                .HasDefaultValueSql("0")
                .HasColumnName("minpurchaseamount");
            entity.Property(e => e.Promotioncode)
                .HasMaxLength(50)
                .HasColumnName("promotioncode");
            entity.Property(e => e.Promotionname)
                .HasMaxLength(200)
                .HasColumnName("promotionname");
            entity.Property(e => e.Startdate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("startdate");
            entity.Property(e => e.Updatedat)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updatedat");
            entity.Property(e => e.Usagecount)
                .HasDefaultValue(0)
                .HasColumnName("usagecount");
            entity.Property(e => e.Usagelimit).HasColumnName("usagelimit");
            entity.Property(e => e.Usagelimitpercustomer)
                .HasDefaultValue(1)
                .HasColumnName("usagelimitpercustomer");

            entity.HasOne(d => d.CreatedbyNavigation).WithMany(p => p.Promotions)
                .HasForeignKey(d => d.Createdby)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("promotions_createdby_fkey");
        });

        modelBuilder.Entity<Report>(entity =>
        {
            entity.HasKey(e => e.Reportid).HasName("reports_pkey");

            entity.ToTable("reports");

            entity.HasIndex(e => e.Reportcode, "ix_reports_code");

            entity.HasIndex(e => new { e.Startdate, e.Enddate }, "ix_reports_dates");

            entity.HasIndex(e => e.Reporttype, "ix_reports_type");

            entity.HasIndex(e => e.Reportcode, "reports_reportcode_key").IsUnique();

            entity.Property(e => e.Reportid).HasColumnName("reportid");
            entity.Property(e => e.Categoryid).HasColumnName("categoryid");
            entity.Property(e => e.Counterid).HasColumnName("counterid");
            entity.Property(e => e.Enddate).HasColumnName("enddate");
            entity.Property(e => e.Generatedat)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("generatedat");
            entity.Property(e => e.Generatedby).HasColumnName("generatedby");
            entity.Property(e => e.Reportcode)
                .HasMaxLength(50)
                .HasColumnName("reportcode");
            entity.Property(e => e.Reportdata).HasColumnName("reportdata");
            entity.Property(e => e.Reportname)
                .HasMaxLength(200)
                .HasColumnName("reportname");
            entity.Property(e => e.Reporttype)
                .HasMaxLength(50)
                .HasColumnName("reporttype");
            entity.Property(e => e.Staffid).HasColumnName("staffid");
            entity.Property(e => e.Startdate).HasColumnName("startdate");
            entity.Property(e => e.Totalcost)
                .HasPrecision(18, 2)
                .HasDefaultValueSql("0")
                .HasColumnName("totalcost");
            entity.Property(e => e.Totalinvoices)
                .HasDefaultValue(0)
                .HasColumnName("totalinvoices");
            entity.Property(e => e.Totalproductssold)
                .HasDefaultValue(0)
                .HasColumnName("totalproductssold");
            entity.Property(e => e.Totalprofit)
                .HasPrecision(18, 2)
                .HasDefaultValueSql("0")
                .HasColumnName("totalprofit");
            entity.Property(e => e.Totalrevenue)
                .HasPrecision(18, 2)
                .HasDefaultValueSql("0")
                .HasColumnName("totalrevenue");

            entity.HasOne(d => d.Category).WithMany(p => p.Reports)
                .HasForeignKey(d => d.Categoryid)
                .HasConstraintName("reports_categoryid_fkey");

            entity.HasOne(d => d.Counter).WithMany(p => p.Reports)
                .HasForeignKey(d => d.Counterid)
                .HasConstraintName("reports_counterid_fkey");

            entity.HasOne(d => d.GeneratedbyNavigation).WithMany(p => p.ReportGeneratedbyNavigations)
                .HasForeignKey(d => d.Generatedby)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("reports_generatedby_fkey");

            entity.HasOne(d => d.Staff).WithMany(p => p.ReportStaffs)
                .HasForeignKey(d => d.Staffid)
                .HasConstraintName("reports_staffid_fkey");
        });

        modelBuilder.Entity<Stockmovement>(entity =>
        {
            entity.HasKey(e => e.Movementid).HasName("stockmovements_pkey");

            entity.ToTable("stockmovements");

            entity.HasIndex(e => e.Createdat, "ix_stockmovements_date").IsDescending();

            entity.HasIndex(e => e.Productid, "ix_stockmovements_product");

            entity.HasIndex(e => e.Movementtype, "ix_stockmovements_type");

            entity.Property(e => e.Movementid).HasColumnName("movementid");
            entity.Property(e => e.Createdat)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("createdat");
            entity.Property(e => e.Createdby).HasColumnName("createdby");
            entity.Property(e => e.Movementtype)
                .HasMaxLength(20)
                .HasColumnName("movementtype");
            entity.Property(e => e.Productid).HasColumnName("productid");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
            entity.Property(e => e.Quantityafter).HasColumnName("quantityafter");
            entity.Property(e => e.Quantitybefore).HasColumnName("quantitybefore");
            entity.Property(e => e.Reason)
                .HasMaxLength(500)
                .HasColumnName("reason");
            entity.Property(e => e.Referenceid).HasColumnName("referenceid");
            entity.Property(e => e.Referencetype)
                .HasMaxLength(20)
                .HasColumnName("referencetype");

            entity.HasOne(d => d.CreatedbyNavigation).WithMany(p => p.Stockmovements)
                .HasForeignKey(d => d.Createdby)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("stockmovements_createdby_fkey");

            entity.HasOne(d => d.Product).WithMany(p => p.Stockmovements)
                .HasForeignKey(d => d.Productid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("stockmovements_productid_fkey");
        });

        modelBuilder.Entity<Systemsetting>(entity =>
        {
            entity.HasKey(e => e.Settingid).HasName("systemsettings_pkey");

            entity.ToTable("systemsettings");

            entity.HasIndex(e => e.Category, "ix_systemsettings_category");

            entity.HasIndex(e => e.Settingkey, "ix_systemsettings_key");

            entity.HasIndex(e => e.Settingkey, "systemsettings_settingkey_key").IsUnique();

            entity.Property(e => e.Settingid).HasColumnName("settingid");
            entity.Property(e => e.Category)
                .HasMaxLength(50)
                .HasColumnName("category");
            entity.Property(e => e.Datatype)
                .HasMaxLength(20)
                .HasDefaultValueSql("'String'::character varying")
                .HasColumnName("datatype");
            entity.Property(e => e.Description)
                .HasMaxLength(500)
                .HasColumnName("description");
            entity.Property(e => e.Iseditable)
                .HasDefaultValue(true)
                .HasColumnName("iseditable");
            entity.Property(e => e.Settingkey)
                .HasMaxLength(100)
                .HasColumnName("settingkey");
            entity.Property(e => e.Settingvalue).HasColumnName("settingvalue");
            entity.Property(e => e.Updatedat)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updatedat");
            entity.Property(e => e.Updatedby).HasColumnName("updatedby");

            entity.HasOne(d => d.UpdatedbyNavigation).WithMany(p => p.Systemsettings)
                .HasForeignKey(d => d.Updatedby)
                .HasConstraintName("systemsettings_updatedby_fkey");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Userid).HasName("users_pkey");

            entity.ToTable("users");

            entity.HasIndex(e => e.Role, "ix_users_role");

            entity.HasIndex(e => e.Username, "ix_users_username");

            entity.HasIndex(e => e.Email, "users_email_key").IsUnique();

            entity.HasIndex(e => e.Username, "users_username_key").IsUnique();

            entity.Property(e => e.Userid).HasColumnName("userid");
            entity.Property(e => e.Createdat)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("createdat");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("email");
            entity.Property(e => e.Fullname)
                .HasMaxLength(100)
                .HasColumnName("fullname");
            entity.Property(e => e.Isactive)
                .HasDefaultValue(true)
                .HasColumnName("isactive");
            entity.Property(e => e.Isdeleted)
                .HasDefaultValue(false)
                .HasColumnName("isdeleted");
            entity.Property(e => e.Passwordhash)
                .HasMaxLength(255)
                .HasColumnName("passwordhash");
            entity.Property(e => e.Phone)
                .HasMaxLength(20)
                .HasColumnName("phone");
            entity.Property(e => e.Role)
                .HasMaxLength(20)
                .HasColumnName("role");
            entity.Property(e => e.Updatedat)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updatedat");
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .HasColumnName("username");
        });

        modelBuilder.Entity<Warranty>(entity =>
        {
            entity.HasKey(e => e.Warrantyid).HasName("warranties_pkey");

            entity.ToTable("warranties");

            entity.HasIndex(e => e.Warrantycode, "ix_warranties_code");

            entity.HasIndex(e => e.Customerid, "ix_warranties_customer");

            entity.HasIndex(e => e.Invoicedetailid, "ix_warranties_invoicedetail");

            entity.HasIndex(e => e.Status, "ix_warranties_status");

            entity.HasIndex(e => e.Warrantycode, "warranties_warrantycode_key").IsUnique();

            entity.Property(e => e.Warrantyid).HasColumnName("warrantyid");
            entity.Property(e => e.Claimcount)
                .HasDefaultValue(0)
                .HasColumnName("claimcount");
            entity.Property(e => e.Coveragedescription).HasColumnName("coveragedescription");
            entity.Property(e => e.Createdat)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("createdat");
            entity.Property(e => e.Customerid).HasColumnName("customerid");
            entity.Property(e => e.Enddate).HasColumnName("enddate");
            entity.Property(e => e.Exclusions).HasColumnName("exclusions");
            entity.Property(e => e.Invoicedetailid).HasColumnName("invoicedetailid");
            entity.Property(e => e.Lastclaimdate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("lastclaimdate");
            entity.Property(e => e.Notes)
                .HasMaxLength(1000)
                .HasColumnName("notes");
            entity.Property(e => e.Productid).HasColumnName("productid");
            entity.Property(e => e.Startdate).HasColumnName("startdate");
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .HasDefaultValueSql("'Active'::character varying")
                .HasColumnName("status");
            entity.Property(e => e.Terms).HasColumnName("terms");
            entity.Property(e => e.Updatedat)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updatedat");
            entity.Property(e => e.Warrantycode)
                .HasMaxLength(50)
                .HasColumnName("warrantycode");
            entity.Property(e => e.Warrantyperiodmonths).HasColumnName("warrantyperiodmonths");
            entity.Property(e => e.Warrantytype)
                .HasMaxLength(50)
                .HasDefaultValueSql("'Standard'::character varying")
                .HasColumnName("warrantytype");

            entity.HasOne(d => d.Customer).WithMany(p => p.Warranties)
                .HasForeignKey(d => d.Customerid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("warranties_customerid_fkey");

            entity.HasOne(d => d.Invoicedetail).WithMany(p => p.Warranties)
                .HasForeignKey(d => d.Invoicedetailid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("warranties_invoicedetailid_fkey");

            entity.HasOne(d => d.Product).WithMany(p => p.Warranties)
                .HasForeignKey(d => d.Productid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("warranties_productid_fkey");
        });

        modelBuilder.Entity<Warrantyclaim>(entity =>
        {
            entity.HasKey(e => e.Claimid).HasName("warrantyclaims_pkey");

            entity.ToTable("warrantyclaims");

            entity.HasIndex(e => e.Claimcode, "ix_warrantyclaims_code");

            entity.HasIndex(e => e.Status, "ix_warrantyclaims_status");

            entity.HasIndex(e => e.Warrantyid, "ix_warrantyclaims_warranty");

            entity.HasIndex(e => e.Claimcode, "warrantyclaims_claimcode_key").IsUnique();

            entity.Property(e => e.Claimid).HasColumnName("claimid");
            entity.Property(e => e.Claimcode)
                .HasMaxLength(50)
                .HasColumnName("claimcode");
            entity.Property(e => e.Claimdate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("claimdate");
            entity.Property(e => e.Claimtype)
                .HasMaxLength(50)
                .HasColumnName("claimtype");
            entity.Property(e => e.Completeddate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("completeddate");
            entity.Property(e => e.Createdat)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("createdat");
            entity.Property(e => e.Handledby).HasColumnName("handledby");
            entity.Property(e => e.Issuedescription).HasColumnName("issuedescription");
            entity.Property(e => e.Repaircost)
                .HasPrecision(18, 2)
                .HasDefaultValueSql("0")
                .HasColumnName("repaircost");
            entity.Property(e => e.Resolutiondescription).HasColumnName("resolutiondescription");
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .HasDefaultValueSql("'Pending'::character varying")
                .HasColumnName("status");
            entity.Property(e => e.Updatedat)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updatedat");
            entity.Property(e => e.Warrantyid).HasColumnName("warrantyid");

            entity.HasOne(d => d.HandledbyNavigation).WithMany(p => p.Warrantyclaims)
                .HasForeignKey(d => d.Handledby)
                .HasConstraintName("warrantyclaims_handledby_fkey");

            entity.HasOne(d => d.Warranty).WithMany(p => p.Warrantyclaims)
                .HasForeignKey(d => d.Warrantyid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("warrantyclaims_warrantyid_fkey");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

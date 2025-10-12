using Microsoft.EntityFrameworkCore;
using ProductsApi.Models;

namespace ProductsApi.Data
{
    public class AppDb : DbContext
    {
        public AppDb(DbContextOptions<AppDb> options) : base(options)
        {
        }

        public DbSet<Product> products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Report> Reports { get; set; }
        public DbSet<Counter> Counters { get; set; }
        public DbSet<Auditlog> Auditlogs { get; set; }
        public DbSet<Productimage> Productimages { get; set; }
        public DbSet<Warrantyclaim> Warrantyclaims { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // XÁC ĐỊNH KHÓA CHÍNH CHO TẤT CẢ ENTITIES

            modelBuilder.Entity<Product>().HasKey(e => e.Productid);
            modelBuilder.Entity<Category>().HasKey(e => e.Categoryid);
            modelBuilder.Entity<User>().HasKey(e => e.Userid);
            modelBuilder.Entity<Report>().HasKey(e => e.Reportid);
            modelBuilder.Entity<Counter>().HasKey(e => e.Counterid);
            modelBuilder.Entity<Auditlog>().HasKey(e => e.logid); // QUAN TRỌNG
            modelBuilder.Entity<Productimage>().HasKey(e => e.Imageid);
            modelBuilder.Entity<Warrantyclaim>().HasKey(e => e.Claimid);
            modelBuilder.Entity<Stockmovement>().HasKey(e => e.Movementid);
            modelBuilder.Entity<Systemsetting>().HasKey(e => e.Settingid);


            // CẤU HÌNH QUAN HỆ

            // Report relationships
            modelBuilder.Entity<Report>()
                .HasOne(r => r.GeneratedbyNavigation)
                .WithMany(u => u.ReportGeneratedbyNavigations)
                .HasForeignKey(r => r.Generatedby);

            modelBuilder.Entity<Report>()
                .HasOne(r => r.Staff)
                .WithMany(u => u.ReportStaffs)
                .HasForeignKey(r => r.Staffid);

            modelBuilder.Entity<Report>()
                .HasOne(r => r.Category)
                .WithMany(c => c.Reports)
                .HasForeignKey(r => r.Categoryid);

            modelBuilder.Entity<Report>()
                .HasOne(r => r.Counter)
                .WithMany(c => c.Reports)
                .HasForeignKey(r => r.Counterid);

            // Product relationships
            modelBuilder.Entity<Product>()
                .HasOne(p => p.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.Categoryid);

            // Counter relationships
            modelBuilder.Entity<Counter>()
                .HasOne(c => c.Staff)
                .WithMany(u => u.Counters)
                .HasForeignKey(c => c.Staffid);

            // Auditlog relationships
            modelBuilder.Entity<Auditlog>()
                .HasOne(a => a.User)
                .WithMany(u => u.Auditlogs)
                .HasForeignKey(a => a.Userid);

            // Productimage relationships
            modelBuilder.Entity<Productimage>()
                .HasOne(pi => pi.Product)
                .WithMany(p => p.Productimages)
                .HasForeignKey(pi => pi.Productid);

            // Warrantyclaim relationships
            modelBuilder.Entity<Warrantyclaim>()
                .HasOne(w => w.HandledbyNavigation)
                .WithMany(u => u.Warrantyclaims)
                .HasForeignKey(w => w.Handledby);
        }
    }
}
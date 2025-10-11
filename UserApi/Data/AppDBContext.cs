using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using UserApi.Models.Entities;

namespace UserApi.Data
{
    public class AppDBContext : DbContext
    { 
        public AppDBContext(DbContextOptions<AppDBContext> options) :base(options) {

        }
        public DbSet<User> Users { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Cấu hình tất cả DateTime properties thành UTC cho PostgreSQL
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                foreach (var property in entityType.GetProperties())
                {
                    if (property.ClrType == typeof(DateTime) || property.ClrType == typeof(DateTime?))
                    {
                        // Tạo value converter để chuyển đổi DateTime sang UTC
                        property.SetValueConverter(
                            new ValueConverter<DateTime, DateTime>(
                                v => v.ToUniversalTime(), // Convert to UTC khi save vào database
                                v => DateTime.SpecifyKind(v, DateTimeKind.Utc) // Đảm bảo là UTC khi read từ database
                            ));
                    }
                }
            }
        }
    }
    }

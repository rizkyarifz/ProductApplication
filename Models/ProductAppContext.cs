using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using ProductApplication.Models.Product;

namespace ProductApplication.Models
{
    public partial class ProductAppContext : DbContext
    {
        public ProductAppContext()
        {
        }

        public ProductAppContext(DbContextOptions<ProductAppContext> options)
            : base(options)
        {
        }

        public virtual DbSet<ProductModel> Product { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductModel>(entity =>
            {
                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd();
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}


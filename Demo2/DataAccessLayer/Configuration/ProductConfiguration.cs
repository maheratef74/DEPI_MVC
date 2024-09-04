using DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Configuration
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Product");

            builder.HasKey(p => p.Id);

            builder.Property(o => o.Id)
                    .ValueGeneratedNever(); // don't use identity

            builder.Property(p => p.Name)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(p => p.Price)
                   .HasColumnType("decimal(18,2)")
                   .IsRequired();

            builder.Property(p => p.Image)
                   .HasMaxLength(250);

            builder.Property(p => p.Description)
                   .HasColumnType("nvarchar(max)");

            builder.HasOne(p => p.Department)
                   .WithMany(d => d.Products)
                   .HasForeignKey(p => p.DepartmentId);

            builder.HasMany(p => p.OrderProducts)
                   .WithOne(op => op.Product)
                   .HasForeignKey(op => op.ProductId);
        }
    }
}

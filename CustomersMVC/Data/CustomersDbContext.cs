using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace CustomersMVC.Data;

public partial class CustomersDbContext : DbContext
{
    public CustomersDbContext()
    {
    }

    public CustomersDbContext(DbContextOptions<CustomersDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__CUSTOMER__3214EC2711C404C8");

            entity.ToTable("CUSTOMERS");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .HasColumnName("EMAIL");
            entity.Property(e => e.Firstname)
                .HasMaxLength(50)
                .HasColumnName("FIRSTNAME");
            entity.Property(e => e.Lastname)
                .HasMaxLength(50)
                .HasColumnName("LASTNAME");
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .HasColumnName("PASSWORD");
            entity.Property(e => e.Username)
                .HasMaxLength(255)
                .HasColumnName("USERNAME");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => new { e.Orderid, e.Customerid, e.Productid }).HasName("PK__ORDERS__7C3764E0F5E3391E");

            entity.ToTable("ORDERS");

            entity.Property(e => e.Orderid)
                .ValueGeneratedOnAdd()
                .HasColumnName("ORDERID");
            entity.Property(e => e.Customerid).HasColumnName("CUSTOMERID");
            entity.Property(e => e.Productid).HasColumnName("PRODUCTID");
            entity.Property(e => e.Orderdate)
                .HasColumnType("datetime")
                .HasColumnName("ORDERDATE");
            entity.Property(e => e.Productqty).HasColumnName("PRODUCTQTY");

            entity.HasOne(d => d.Customer).WithMany(p => p.Orders)
                .HasForeignKey(d => d.Customerid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CUSTOMER");

            entity.HasOne(d => d.Product).WithMany(p => p.Orders)
                .HasForeignKey(d => d.Productid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PRODUCT");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__PRODUCTS__3214EC27BDBFCABC");

            entity.ToTable("PRODUCTS");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .HasColumnName("DESCRIPTION");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("NAME");
            entity.Property(e => e.Price)
                .HasColumnType("numeric(5, 2)")
                .HasColumnName("PRICE");
            entity.Property(e => e.Quantity).HasColumnName("QUANTITY");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

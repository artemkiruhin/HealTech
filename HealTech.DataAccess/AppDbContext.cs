using HealTech.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace HealTech.DataAccess;

public class AppDbContext : DbContext
{
    public DbSet<User> Users { get; set; } = null!;
    public DbSet<Customer> Customers { get; set; } = null!;
    public DbSet<Employee> Employees { get; set; } = null!;
    public DbSet<Order> Orders { get; set; } = null!;
    public DbSet<Product> Products { get; set; } = null!;
    public DbSet<ProductCategory> ProductCategories { get; set; } = null!;

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    // If you want to keep the old method of configuration, you can use this
    //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //{
    //    if (!optionsBuilder.IsConfigured)
    //    {
    //        optionsBuilder.UseNpgsql("Server=localhost;Database=healtech_db;Username=postgres;Password=root");
    //    }
    //}


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // User configuration (base class)
        modelBuilder.Entity<User>(options =>
        {
            options.ToTable("Users");
            options.HasKey(x => x.Id);
            options.Property(x => x.FirstName).IsRequired().HasMaxLength(100);
            options.Property(x => x.Surname).IsRequired().HasMaxLength(100);
            options.Property(x => x.Username).IsRequired().HasMaxLength(50);
            options.Property(x => x.PasswordHash).IsRequired();
            options.Property(x => x.Role).IsRequired().HasMaxLength(20);
            options.HasIndex(x => x.Username).IsUnique();
        });

        // Customer-specific properties
        modelBuilder.Entity<Customer>(options =>
        {
            options.ToTable("Customers");
            options.Property(x => x.TaxNumber).IsRequired().HasMaxLength(20);
            options.Property(x => x.Email).IsRequired().HasMaxLength(100);
            options.Property(x => x.Phone).IsRequired().HasMaxLength(20);
            options.Property(x => x.Address).IsRequired().HasMaxLength(200);
            options.Property(x => x.Registered).IsRequired();
            options.HasIndex(x => x.Email).IsUnique();

            options.HasMany(x => x.Orders)
                .WithOne(x => x.Customer)
                .HasForeignKey(x => x.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        // Employee-specific properties
        modelBuilder.Entity<Employee>(options =>
        {
            options.ToTable("Employees");
            options.Property(x => x.Hired).IsRequired();
            options.Property(x => x.IsAdmin).IsRequired();
            options.Property(x => x.Salary)
                .IsRequired()
                .HasColumnType("numeric(18,2)"); // PostgreSQL uses 'numeric' instead of 'decimal'
        });

        // Product Category configuration
        modelBuilder.Entity<ProductCategory>(options =>
        {
            options.ToTable("ProductCategories");
            options.HasKey(x => x.Id);
            options.Property(x => x.Name).IsRequired().HasMaxLength(100);
            options.HasIndex(x => x.Name).IsUnique();

            options.HasMany(x => x.Products)
                .WithOne(x => x.Category)
                .HasForeignKey(x => x.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        // Product configuration
        modelBuilder.Entity<Product>(options =>
        {
            options.ToTable("Products");
            options.HasKey(x => x.Id);
            options.Property(x => x.Name).IsRequired().HasMaxLength(200);

            options.HasMany(x => x.Orders)
                .WithOne(x => x.Product)
                .HasForeignKey(x => x.ProductId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        // Order configuration
        modelBuilder.Entity<Order>(options =>
        {
            options.ToTable("Orders");
            options.HasKey(x => x.Id);

            options.HasOne(x => x.Customer)
                .WithMany(x => x.Orders)
                .HasForeignKey(x => x.CustomerId);
            options.HasOne(x => x.Product)
                .WithMany(x => x.Orders)
                .HasForeignKey(x => x.ProductId);
        });
    }
}
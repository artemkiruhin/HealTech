using HealTech.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace HealTech.DataAccess;

public class AppDbContext : DbContext
{
    public DbSet<User> Users { get; set; } = null!;
    public DbSet<Customer> Customers { get; set; } = null!;
    public DbSet<Employee> Employees { get; set; } = null!;
    public DbSet<Order> Orders { get; set; } = null!;
    public DbSet<OrderDetail> OrderDetails { get; set; } = null!;
    public DbSet<Product> Products { get; set; } = null!;
    public DbSet<ProductCategory> ProductCategories { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("");

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

            // Discriminator для разделения типов пользователей
            options.HasDiscriminator(x => x.Role)
                .HasValue<Customer>(nameof(UserRole.Customer))
                .HasValue<Employee>(nameof(UserRole.Employee));
        });

        // Customer configuration
        modelBuilder.Entity<Customer>(options =>
        {
            options.ToTable("Customers");
            
            options.Property(x => x.TaxNumber).IsRequired().HasMaxLength(20);
            options.Property(x => x.Email).IsRequired().HasMaxLength(100);
            options.Property(x => x.Phone).IsRequired().HasMaxLength(20);
            options.Property(x => x.Address).IsRequired().HasMaxLength(200);
            options.Property(x => x.Registered).IsRequired();

            // Индекс для быстрого поиска по email
            options.HasIndex(x => x.Email).IsUnique();
            
            options.HasMany(x => x.Orders)
                .WithOne(x => x.Customer)
                .HasForeignKey(x => x.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        // Employee configuration
        modelBuilder.Entity<Employee>(options =>
        {
            options.ToTable("Employees");
            
            options.Property(x => x.Hired).IsRequired();
            options.Property(x => x.IsAdmin).IsRequired();
            options.Property(x => x.Salary).IsRequired()
                .HasColumnType("decimal(18,2)");
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
            
            options.HasMany(x => x.OrderDetails)
                .WithOne(x => x.Product)
                .HasForeignKey(x => x.ProductId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        // Order configuration
        modelBuilder.Entity<Order>(options =>
        {
            options.ToTable("Orders");
            options.HasKey(x => x.Id);
            
            options.HasMany(x => x.OrderDetails)
                .WithOne(x => x.Order)
                .HasForeignKey(x => x.OrderId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        // OrderDetail configuration
        modelBuilder.Entity<OrderDetail>(options =>
        {
            options.ToTable("OrderDetails");
            options.HasKey(x => x.Id);
            
            // Добавляем составной индекс для оптимизации запросов
            options.HasIndex(x => new { x.OrderId, x.ProductId });
        });
    }
}
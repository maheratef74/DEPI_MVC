using DataAccessLayer.Configuration;
using DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;


namespace DataAccessLayer.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }
        // 🚩🚩 Naming Convention (1) : Table_Name = DBSet_Property_Name
        public DbSet<Product> Products { get; set; }  // ➡️➡️ maps to table (Products) by convention
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderProduct> OrderProducts { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Customer> Customers { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder
                    .UseSqlServer("Server = KARIM-ALI\\MSSQLSERVER01; Database= EcommerceSystem ; Integrated Security = True; TrustServerCertificate = True;");
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // 🚩🚩 Configuration using Fluent API

            //modelBuilder.Entity<Product>()
            //    .ToTable("Products");

            //modelBuilder.Entity<Product>()
            //    .HasMany(p => OrderProducts)
            //    .WithOne(op => op.Product)
            //    .HasForeignKey(op => op.ProductId);


            //modelBuilder.Entity<Product>()
            //    .HasOne(p => p.Department)
            //    .WithMany(d => d.Products)
            //    .HasForeignKey(p => p.DepartmentId);

            //modelBuilder.Entity<Product>()
            //    .HasKey(p => p.Id);

            //modelBuilder.Entity<Product>()
            //    .Property(p => p.Price)
            //    .HasColumnType("decimal(18,2)");

            //modelBuilder.Entity<OrderProduct>()
            //    .HasKey(op => new { op.OrderId, op.ProductId });


            // Individual Call
            //==============================

            //modelBuilder.ApplyConfiguration(new CustomerConfiguration());
            //modelBuilder.ApplyConfiguration(new ProductConfiguration());
            //modelBuilder.ApplyConfiguration(new OrderConfiguration());
            //modelBuilder.ApplyConfiguration(new OrderProductConfiguration());
            //modelBuilder.ApplyConfiguration(new DepartmentConfiguration());


            //new DepartmentConfiguration().Configure(modelBuilder.Entity<Department>());
            //new CustomerConfiguration().Configure(modelBuilder.Entity<Customer>());
            //new OrderConfiguration().Configure(modelBuilder.Entity<Order>());
            //new OrderProductConfiguration().Configure(modelBuilder.Entity<OrderProduct>());
            //new ProductConfiguration().Configure(modelBuilder.Entity<Product>());

            //// Call from Assembly
            ////================================
            modelBuilder.ApplyConfigurationsFromAssembly
            (
                typeof(ApplicationDbContext).Assembly
            );


            // Code First
            // Database ========> C#

            // Sql ==> View , Function , Stored Proc

            // Stored Prod  call  Function & View
            // Function     call  View

            // From my App Code
            // Call Function or Stored Prod , send parameters

        }
    }
}

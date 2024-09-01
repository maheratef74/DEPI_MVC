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
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder
                    .UseSqlServer("Server = KARIM-ALI\\MSSQLSERVER01; Database= EcommerceSystem ; Integrated Security = True; TrustServerCertificate = True;");
            }
        }
    }
}

using BusinessLayer.Services;
using DataAccessLayer.Context;
using DataAccessLayer.Repositories;
using Microsoft.EntityFrameworkCore;

namespace PresentationLayer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            #region Configuration  Startup.cs >> Configure()
            var builder = WebApplication.CreateBuilder(args);

            //var connectionString = builder.Configuration.GetSection("ConnectionStrings.EcommerceSystem");
            var connectionString = builder.Configuration.GetConnectionString("EcommerceSystem");

            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            {
                options
                    .UseSqlServer(connectionString)
                    .LogTo(Console.WriteLine, LogLevel.Information);
            });

            // Register service in container
            builder.Services.AddScoped<IProductRepository, ProductRepository>();
            builder.Services.AddScoped<IProductService, ProductService>();
            //builder.Services.AddScoped<IProductService, ProductService2>();
            builder.Services.AddScoped<IFileService, FileService>();
            // Add services to the container.
            builder.Services.AddControllersWithViews();

            var app = builder.Build();
            #endregion

            #region PipeLine
            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            // Route
            // localhost:7845 / Controller_Name / Action_Name
            // localhost:7845 / Home / Index
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            //             / Product       /  Details     /  1
            app.Run(); 
            #endregion
        }
    }
}

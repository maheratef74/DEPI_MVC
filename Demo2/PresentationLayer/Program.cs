using BusinessLayer.Services;
using DataAccessLayer.Context;
using DataAccessLayer.Repositories;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using PresentationLayer.Controllers;

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
            }); // ServiceLifetime.Transient

            // Register service in container
            builder.Services.AddScoped<IProductRepository, ProductRepository>();
            builder.Services.AddScoped<IOrderRepository, OrderRepository>();
            builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
            builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();


            builder.Services.AddScoped<IProductService, ProductService>();
            builder.Services.AddScoped<ICustomerService, CustomerService>();
            builder.Services.AddScoped<IOrderService, OrderService>();
            builder.Services.AddScoped<IFileService, FileService>();

            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.ExpireTimeSpan = TimeSpan.FromDays(1);
                    options.LoginPath = "/Account/Login";
                    options.AccessDeniedPath = "/Account/NotAuthorized";
                });


            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy(UserRole.Admin, policy =>
                {
                    policy.RequireRole(UserRole.Admin);
                });

                options.AddPolicy(UserRole.Customer, policy =>
                {
                    policy.RequireRole(UserRole.Customer);
                });

                options.AddPolicy(UserRole.Buyer, policy =>
                {
                    policy.RequireRole(UserRole.Buyer);
                });

                options.AddPolicy("AdminOrBuyer", policy =>
                {
                    policy.RequireRole(UserRole.Admin, UserRole.Buyer);
                });
            });
            // 🚩🚩 If you are using more than way of authentication you can here specify which is the default
            //builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme) 
            //    .AddCookie()
            //    .AddOAuth();

            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(20); // Configure the lifetime of a session
            });

            // Add services to the container.
            builder.Services.AddControllersWithViews(); // .AddSessionStateTempDataProvider()

            var app = builder.Build();
            #endregion

            #region PipeLine

            // My Middlewares

            //app.Use(async (context, next) =>
            //{
            //    // logic to be executed in the first direction
            //    await context.Response.WriteAsync("Middleware 1 \n");

            //    await next.Invoke();

            //    // logic to be executed in the second direction
            //    await context.Response.WriteAsync("Middleware 1_1 \n");

            //});

            //app.Use(async (context, next) =>
            //{
            //    // logic to be executed in the first direction
            //    await context.Response.WriteAsync("Middleware 2 \n");

            //    await next.Invoke();

            //    // logic to be executed in the second direction
            //    await context.Response.WriteAsync("Middleware 2_2 \n");

            //});

            //app.Use(async (context, next) =>
            //{
            //    // logic to be executed in the first direction
            //    await context.Response.WriteAsync("Middleware 3 \n");

            //    await next.Invoke();

            //    // logic to be executed in the second direction
            //    await context.Response.WriteAsync("Middleware 3_3 \n");

            //});


            //app.Run(async (context) =>
            //{
            //    await context.Response.WriteAsync("Terminate \n");
            //});


            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication(); // reads user information from cookie coming in request  ===fills===>  HttpContext.User

            // 🚩🚩  is a middleware component in the ASP.NET Core request pipeline that:
            // ✅ Intercepts every request and checks if an authentication cookie is present.
            // ✅ If the cookie is present, it decrypts the cookie and reconstructs the user's identity (claims).
            // ✅ It attaches this identity to the HttpContext.User property, making the user's information (claims) available throughout the application.
            app.UseAuthorization();

            app.UseSession();

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

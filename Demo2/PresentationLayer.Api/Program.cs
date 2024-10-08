
using BusinessLayer.Services;
using DataAccessLayer.Context;
using DataAccessLayer.Entities;
using DataAccessLayer.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PresentationLayer.Api.Middlewares;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.OpenApi.Models;
using System.Text;
using DataAccessLayer.Repositories.GenericProduct;
using Serilog;
using Serilog.Sinks.MSSqlServer;
using DataAccessLayer.UnitOfWork;
using PresentationLayer.Api.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Swashbuckle.AspNetCore.SwaggerGen;
using Microsoft.Extensions.Caching.Distributed;

namespace PresentationLayer.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            #region Services Registration ( Configuration )
            var builder = WebApplication.CreateBuilder(args);

            builder.Host.UseSerilog((context, services, configuration) =>
            {
                configuration
                .ReadFrom.Configuration(context.Configuration)
                .Enrich.FromLogContext();
            });
            // Add services to the container.

            var myPolicy = "MyPolicy";
            builder.Services.AddCors(options =>
            {
                options.AddPolicy(name: myPolicy, policy =>
                {
                    policy
                        //.WithOrigins("http://127.0.0.1:5500/", "http://localhost:4200")
                        //.WithMethods("Get", "Post")
                        .AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                });
            });

            builder.Services.AddScoped<IProductRepository, ProductRepository>();
            builder.Services.AddScoped<IOrderRepository, OrderRepository>();
            builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            builder.Services.AddScoped<IGenericProductRepository, GenericProductRepository>();
            builder.Services.AddScoped<ICustomerRepository  , CustomerRepository>();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

            // Filters
            builder.Services.AddScoped<IPWhiteListAuthorizationFilter>();
            builder.Services.AddScoped<LogExecutionTimeFilter>();
            builder.Services.AddScoped<LogExecutionTimeFilterAsync>();

            builder.Services.AddScoped<RedisCacheResourceFilter>(provider =>
            {
                var cache = provider.GetRequiredService<IDistributedCache>();
                return new RedisCacheResourceFilter(cache, 1);
            });


            builder.Services.AddScoped<IProductService, ProductService>();
            builder.Services.AddScoped<IOrderService, OrderService>();

            var connectionString = builder.Configuration.GetConnectionString("EcommerceSystem");

            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            {
                options
                    .UseSqlServer(connectionString)
                    .LogTo(Console.WriteLine, LogLevel.Information);
            }); // ServiceLifetime.Transient

            builder.Services
                .AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            builder.Services.AddAuthentication(options =>
            {
                // Use jwt bearer token

                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;    // require https
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    // (exp) => expiration
                    ValidateLifetime = true,

                    ValidateIssuer = true,
                    ValidIssuer = builder.Configuration["Jwt:Issuer"],

                    ValidateAudience = true,
                    ValidAudience = builder.Configuration["Jwt:Audience"],

                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Secret"]))
                };
            });

            builder.Services.AddAuthorization();


            builder.Services.AddControllers(options =>
            {
                // Register the custom exception filter globally using dependency injection
                options.Filters.Add(typeof(CustomExceptionFilter)); // Register by type
            });
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options =>
            {

                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "Jwt Authorization Header using Bearer schema",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Scheme = "Bearer"
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });

            });


            builder.Services.AddApiVersioning(options =>
            {
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.DefaultApiVersion = new ApiVersion(1, 0); // v1.0
                options.ReportApiVersions = true; // Add Api Version in headers to response
                options.ApiVersionReader = ApiVersionReader.Combine
                (
                    new QueryStringApiVersionReader("v"), //?v=1  version in query string
                    new HeaderApiVersionReader("api-version"), // version in http header
                    new MediaTypeApiVersionReader("v") // version in media type header
                );
            });

            builder.Services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = builder.Configuration["Redis:Configuration"];
            });

            var app = builder.Build();
            #endregion
            #region PipeLine

            //app.UseMiddleware<GlobalExceptionHandlingMiddleware>();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

           app.UseCors(myPolicy);

            app.UseAuthentication();

            app.UseAuthorization();


            app.MapControllers();

            app.UseSerilogRequestLogging();

            app.Run(); 
            #endregion
        }
    }
}

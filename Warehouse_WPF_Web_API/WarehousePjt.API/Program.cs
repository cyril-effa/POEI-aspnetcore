using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using WarehousePjt.API.Data;
using WarehousePjt.API.Repositories;
using WarehousePjt.API.Services;
using WarehousePjt.Core.Articles.Interfaces;
using WarehousePjt.Core.Articles.UseCases;
using WarehousePjt.Core.Customers.Interfaces;
using WarehousePjt.Core.Customers.UseCases;
using WarehousePjt.Core.Deliveries.Interfaces;
using WarehousePjt.Core.Deliveries.UseCases;
using WarehousePjt.Core.Interfaces;
using WarehousePjt.Core.Orders.Interfaces;
using WarehousePjt.Core.Orders.UseCases;
using WarehousePjt.Core.Warehouses.Interfaces;
using WarehousePjt.Warehouses.UseCases;

namespace WarehousePjt.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options => {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = "WarehousePjtApi",
                        ValidAudience = "WarehousePjtCustomers",
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("SuperSecretKey1234567890-Pa$$w0rd"))
                    };
                });

            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new() { Title = "WarehousePjt.API", Version = "v1" });
                c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = Microsoft.OpenApi.Models.ParameterLocation.Header,
                    Description = "Entrez 'Bearer' suivi d'un espace et de votre token JWT.\r\n\r\nExemple : \"Bearer abcdef12345\""
                });
                c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
                {
                    {
                        new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                        {
                            Reference = new Microsoft.OpenApi.Models.OpenApiReference
                            {
                                Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] {}
                    }
                });
            });

            // Add article services
            builder.Services.AddScoped<IArticleRepository, ArticleRepository>()
                            .AddScoped<IArticleUseCase, ArticleUseCase>();

            // Add customer services
            builder.Services.AddScoped<ICustomerRepository, CustomerRepository>()
                            .AddScoped<ICustomerUseCase, CustomerUseCase>();

            // Add order services
            builder.Services.AddScoped<IOrderRepository, OrderRepository>()
                            .AddScoped<IOrderUseCase, OrderUseCase>();

            // Add warehouse services
            builder.Services.AddScoped<IWarehouseRepository, WarehouseRepository>()
                            .AddScoped<IWarehouseUseCase, WarehouseUseCase>();

            // Add delivery services
            builder.Services.AddScoped<IDeliveryRepository, DeliveryRepository>()
                            .AddScoped<IDeliveryUseCase, DeliveryUseCase>();
            builder.Services.AddScoped<IDeliverySlotRepository, DeliverySlotRepository>();

            // Add facture services
            builder.Services.AddScoped<IFactureRepository, FactureRepository>()
                            .AddScoped<IFactureService, FactureService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();



            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}

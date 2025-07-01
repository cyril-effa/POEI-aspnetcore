using ex10bis.Core.Article.Interfaces;
using ex10bis.Core.Article.UseCases;
using ex10bis.Core.Customer.Interfaces;
using ex10bis.Core.Customer.UseCases;
using ex10bis.Core.Delivery.Interfaces;
using ex10bis.Core.Delivery.UseCases;
using ex10bis.Core.Entities;
using ex10bis.Core.Interfaces;
using ex10bis.Core.Order.Interfaces;
using ex10bis.Core.Order.UseCases;
using ex10bis.Core.Warehouse.Interfaces;
using ex10bis.Core.Warehouse.UseCases;
using ex10bis.Infrastructure.Data;
using ex10bis.Infrastructure.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ex10bis.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString, b => b.MigrationsAssembly("ex10bis.Infrastructure")));
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            // Add article services
            builder.Services.AddScoped<IArticleRepository, ArticleRepository>()
                            .AddScoped<ICreateArticleUseCase, CreateArticleUseCase>()
                            .AddScoped<IEditArticleUseCase, EditArticleUseCase>()
                            .AddScoped<IDeleteArticleUseCase, DeleteArticleUseCase>()
                            .AddScoped<IReadArticleUseCase, ReadArticleUseCase>();

            // Add customer services
            builder.Services.AddScoped<ICustomerRepository, CustomerRepository>()
                            .AddScoped<ICreateCustomerUseCase, CreateCustomerUseCase>()
                            .AddScoped<IEditCustomerUseCase, EditCustomerUseCase>()
                            .AddScoped<IDeleteCustomerUseCase, DeleteCustomerUseCase>()
                            .AddScoped<IReadCustomerUseCase, ReadCustomerUseCase>();

            // Add order services
            builder.Services.AddScoped<IOrderRepository, OrderRepository>()
                            .AddScoped<ICreateOrderUseCase, CreateOrderUseCase>()
                            .AddScoped<IEditOrderUseCase, EditOrderUseCase>()
                            .AddScoped<IDeleteOrderUseCase, DeleteOrderUseCase>()
                            .AddScoped<IReadOrderUseCase, ReadOrderUseCase>();

            // Add warehouse services
            builder.Services.AddScoped<IWarehouseRepository, WarehouseRepository>()
                            .AddScoped<ICreateWarehouseUseCase, CreateWarehouseUseCase>()
                            .AddScoped<IEditWarehouseUseCase, EditWarehouseUseCase>()
                            .AddScoped<IDeleteWarehouseUseCase, DeleteWarehouseUseCase>()
                            .AddScoped<IReadWarehouseUseCase, ReadWarehouseUseCase>();

            // Add delivery services
            builder.Services.AddScoped<IDeliveryRepository, DeliveryRepository>()
                            .AddScoped<ICreateDeliveryUseCase, CreateDeliveryUseCase>();
            builder.Services.AddScoped<IDeliverySlotRepository, DeliverySlotRepository>();
            builder.Services.AddScoped<IFactureRepository, FactureRepository>();

            builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();
            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            app.MapRazorPages();

            using (var scope = app.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

                if (!db.DeliverySlot.Any())
                {
                    var slots = new List<DeliverySlot>();
                    var nbDaysToSetup = 15;

                    for (int i = 0; i < nbDaysToSetup; i++)
                    {
                        var deliveryTime = DateTime.Today.AddDays(i).AddHours(8); // Commence au jour +i à 8h00

                        var nbSlotsMorning = 9; // Nombre de créneaux le matin
                        var nbSlotsAfternoon = 8; // Nombre de créneaux l'après-midi
                        var timeBetweenSlots = 30; // Minutes entre les créneaux

                        for (int j = 0; j < nbSlotsMorning; j++)
                        {
                            slots.Add(new DeliverySlot
                            {
                                DateAndTime = deliveryTime,
                                IsAvailable = true
                            });

                            deliveryTime = deliveryTime.AddMinutes(timeBetweenSlots);
                        }

                        deliveryTime = deliveryTime.AddHours(1); // Pause du midi

                        for (int j = 0; j < nbSlotsAfternoon; j++)
                        {
                            slots.Add(new DeliverySlot
                            {
                                DateAndTime = deliveryTime,
                                IsAvailable = true
                            });

                            deliveryTime = deliveryTime.AddMinutes(timeBetweenSlots);
                        }
                    }

                    db.DeliverySlot.AddRange(slots);
                    db.SaveChanges();
                }
            }

            app.Run();
        }
    }
}

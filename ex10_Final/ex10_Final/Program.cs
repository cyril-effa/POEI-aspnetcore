using ex10_Final.Data;
using ex10_Final.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ex10_Final
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

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

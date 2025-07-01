using DemoTrip.Core.Interfaces;
using DemoTrip.Core.UseCases;
using DemoTrip.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<ISearchFlightUseCase, SearchFlightUseCase>();
builder.Services.AddScoped<ISearchHotelUseCase, SearchHotelUseCase>();
builder.Services.AddScoped<ISearchTripUseCase, SearchTripUseCase>();
builder.Services.AddTransient<IFlightRepository>((services) => new CSVFligthRepository("C:\\Users\\CYRIL.RASTEL\\source\\repos\\POEI\\Ex09_Conception_Architecture\\DemoTrip\\DemoTrip.Infrastructure\\flights_with_names.csv"));
builder.Services.AddTransient<IHotelRepository>((services) => new CSVHotelRepository("C:\\Users\\CYRIL.RASTEL\\source\\repos\\POEI\\Ex09_Conception_Architecture\\DemoTrip\\DemoTrip.Infrastructure\\hotels.csv"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
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

app.Run();

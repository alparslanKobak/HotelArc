using HotelArc.Business;
using HotelArc.Process.Abstract;
using HotelArc.Process.Concrete;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using System;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<DatabaseContext>(options =>
{
    string connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

    options.UseSqlServer(connectionString);
});

builder.Services.AddSession();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(config =>
    {
        config.Cookie.Name = "User.Cookie";
        config.LoginPath = "/Auth/Login";
        config.AccessDeniedPath = "/Auth/AccessDenied";
      
        config.Cookie.MaxAge = TimeSpan.FromDays(1);

    });


builder.Services.AddTransient(typeof(IService<>), typeof(Service<>));
builder.Services.AddTransient<IAppUserService, AppUserService>();
builder.Services.AddTransient<IRoomService, RoomService>();
builder.Services.AddTransient<IReservationService, ReservationService>();
builder.Services.AddTransient<IRoleService, RoleService>();

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
app.UseSession();
app.UseAuthentication();

app.UseAuthorization();


app.MapControllerRoute(
            name: "admin",
            pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


using (var scope = app.Services.CreateScope())
{

    var dbContext = scope.ServiceProvider.GetRequiredService<DatabaseContext>();

    var db = dbContext.Database;

    if (await db.CanConnectAsync())
    {
        await db.EnsureDeletedAsync();
    }

    if (!await db.CanConnectAsync())
    {
        await db.EnsureCreatedAsync();

        DbSeeder.Seed(dbContext);
    }
}

app.Run();

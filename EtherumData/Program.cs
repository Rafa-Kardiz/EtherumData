using EtherumData.Models;
using EtherumData.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddControllersWithViews();
// se añade la conexion a a mysql
builder.Services.AddDbContext<EthereumDataContext>(options =>
{
    object value = options.UseMySql(builder.Configuration.GetConnectionString("EthereumContext"),
        new MySqlServerVersion(new Version(10, 4, 32))
        );
});

builder.Services.AddScoped<EthereumService>();

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

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TestStore.Data;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<TestStoreContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("TestStoreContext") ?? throw new InvalidOperationException("Connection string 'TestStoreContext' not found.")));
builder.Services.AddSession(options => { options.IdleTimeout = TimeSpan.FromMinutes(1); });

// Add services to the container.
builder.Services.AddControllersWithViews();

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
app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=usersaccounts}/{action=Login}");

app.Run();

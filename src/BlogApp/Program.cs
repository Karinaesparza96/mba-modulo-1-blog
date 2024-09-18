using Microsoft.AspNetCore.Identity;
using BlogCore.Data.Context;
using BlogCore.Configuration;
using BlogApp.Configuration;
using BlogCore.Business.Notificacoes;
using BlogCore.Business.Interfaces;
using BlogCore.Business.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContextConfiguration(builder.Configuration);

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<AppDbContext>();

builder.Services.AddAutoMapperConfiguration();

builder.Services.AddScoped<INotificador, Notificador>();
builder.Services.AddScoped<IPostService, PostService>();
builder.Services.AddScoped<IAutorService, AutorService>();

builder.Services.AddControllersWithViews();
builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
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

app.Run();

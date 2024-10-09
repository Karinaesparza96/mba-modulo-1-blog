using BlogApp.Configurations;
using BlogCore.Data.Seed;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContextConfiguration(builder.Configuration);

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddIdentity();

builder.Services.AddResolveDependencie();

builder.Services.AddControllersWithViews();
builder.Services.AddControllers();

builder.Services.AddAutoMapper(typeof(AutoMappingConfiguration).Assembly);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error");
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

app.UseDbMigrationHelper();

app.Run();

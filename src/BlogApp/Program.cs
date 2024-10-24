using BlogApp.Configurations;
using BlogCore.Data.Seed;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContextConfiguration(builder.Configuration);

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddIdentity();

builder.Services.AddResolveDependencie();

builder.Services.AddControllersWithViews();

builder.Services.AddAutoMapper(typeof(AutoMappingConfiguration).Assembly);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{   
    app.UseDeveloperExceptionPage();
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/erro");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseStatusCodePagesWithReExecute("/erro/{0}");

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.UseDbMigrationHelper();

app.Run();

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using TestApp.Core.Interfaces;
using TestApp.Infrastructure.Data;
using TestApp.Infrastructure.Repositories;
using TestApp.Infrastructure.Services;
using TestApp.Web.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Configure web root path for clean architecture structure
builder.Environment.WebRootPath = Path.Combine(builder.Environment.ContentRootPath, "Web", "wwwroot");

// Add services to the container.
builder.Services.AddControllersWithViews()
    .AddRazorOptions(options =>
    {
        options.ViewLocationFormats.Clear();
        options.ViewLocationFormats.Add("/Web/Views/{1}/{0}.cshtml");
        options.ViewLocationFormats.Add("/Web/Views/Shared/{0}.cshtml");
    })
    .AddRazorRuntimeCompilation();

// Register DbContext with connection string from configuration
builder.Services.AddDbContext<DbAppContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    options.UseSqlServer(connectionString, b => b.MigrationsAssembly("TestApp"));
});

// Register repositories
builder.Services.AddScoped<IContactRepository, ContactRepository>();

// Register services
builder.Services.AddScoped<IContactService, ContactService>();
builder.Services.AddScoped<ICsvParserService, CsvParserService>();

// Add logging
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddDebug();

var app = builder.Build();

// Apply pending migrations automatically at startup
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<DbAppContext>();
    dbContext.Database.Migrate();
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
else
{
    app.UseDeveloperExceptionPage();
}

// Add global exception handling middleware
app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
using SeronDesign.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Render port ayarı
var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";
builder.WebHost.UseUrls($"http://*:{port}");

// Add services to the container.
builder.Services.AddControllersWithViews();

// Configure EF Core; if connection string is missing, DbContext fallback in OnConfiguring is used.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<SeronDesignDbContext>(options =>
{
    if (!string.IsNullOrWhiteSpace(connectionString))
    {
        options.UseSqlServer(connectionString, sqlOpts => sqlOpts.EnableRetryOnFailure());
    }
});

var app = builder.Build();

// Apply pending migrations and seed initial data.
try
{
    using var scope = app.Services.CreateScope();
    var db = scope.ServiceProvider.GetRequiredService<SeronDesignDbContext>();
    db.Database.Migrate();

    if (!db.Categories.Any())
    {
        db.Categories.AddRange(
            new Category { Name = "Default" },
            new Category { Name = "Toys" },
            new Category { Name = "Products" }
        );
        db.SaveChanges();
    }

    if (!db.Toys.Any())
    {
        var toysCatId = db.Categories.First(c => c.Name == "Toys").Id;
        db.Toys.Add(new Toy
        {
            Name = "Fenerbahçe Armalı Araç Aksesuarı",
            Description = "Fenerbahçe armalı araba askı süsü, özel plaka anahtarlık ve Blaziken figürü.",
            Price = 49.90m,
            CategoryId = toysCatId,
            ImageName = "Fenerbahçe armalı araba askı süsü, özel plaka anahtarlık ve Blaziken figürü siparişimiz tamamlan.jpg"
        });
        db.SaveChanges();
    }

    var seronProduct = db.Products.FirstOrDefault(p => p.Name.StartsWith("SERON_DESING"));
    if (seronProduct != null)
    {
        db.Products.Remove(seronProduct);
        db.SaveChanges();
    }

    if (!db.Products.Any(p => !p.Name.StartsWith("SERON_DESING")))
    {
        var prodCatId = db.Categories.First(c => c.Name == "Products").Id;
        db.Products.Add(
            new Product
            {
                Name = "Shelby Ruhlu Parça",
                Description = "Shelby ruhunu yeniden canlandırdık; detaylarda emeğin, çizgilerde karakterin izi var.",
                Price = 149.50m,
                CategoryId = prodCatId,
                ImageName = "Shelby ruhunu yeniden canlandırdık. 🐍Detaylarda emeğin, çizgilerde karakterin izi var.#3dbaskı  (1).jpg"
            }
        );
        db.SaveChanges();
    }
}
catch (Exception ex)
{
    app.Logger.LogError(ex, "Database migration/seed failed at startup.");
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

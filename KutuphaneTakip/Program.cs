using SeronDesign.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// configure EF Core with connection string from configuration and enable transient error retries
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
                       ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.Services.AddDbContext<SeronDesignDbContext>(options =>
    options.UseSqlServer(connectionString, sqlOpts =>
        sqlOpts.EnableRetryOnFailure()));

var app = builder.Build();

// apply any pending migrations and seed initial data
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<SeronDesignDbContext>();
    db.Database.Migrate();

    // seed categories if none exist
    if (!db.Categories.Any())
    {
        db.Categories.AddRange(
            new SeronDesign.Models.Category { Name = "Default" },
            new SeronDesign.Models.Category { Name = "Toys" },
            new SeronDesign.Models.Category { Name = "Products" }
        );
        db.SaveChanges();
    }

    // seed an example toy and products if none exist
    if (!db.Toys.Any())
    {
        var toysCatId = db.Categories.First(c => c.Name == "Toys").Id;
        db.Toys.Add(new SeronDesign.Models.Toy
        {
            Name = "Fenerbahçe Armalı Araç Aksesuarı",
            Description = "Fenerbahçe armalı araba askı süsü, özel plaka anahtarlık ve Blaziken figürü.",
            Price = 49.90m,
            CategoryId = toysCatId,
            ImageName = "Fenerbahçe armalı araba askı süsü, özel plaka anahtarlık ve Blaziken figürü siparişimiz tamamlan.jpg"
        });
        db.SaveChanges();
    }

    // ensure SERON_DESING product is removed if present; we only use its image as logo
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
            new SeronDesign.Models.Product
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

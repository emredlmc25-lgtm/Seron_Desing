using Microsoft.EntityFrameworkCore;

namespace SeronDesign.Models
{
    public class SeronDesignDbContext : DbContext
    {
        // Required by AddDbContext; options come from DI configuration.
        public SeronDesignDbContext(DbContextOptions<SeronDesignDbContext> options)
            : base(options)
        {
        }

        // DbContext is now configured by the DI container in Program.cs. Remove hard‑coded connection logic.
        // Keep OnConfiguring only if you need a fallback when context is used outside of DI.
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                // fallback for design-time tools or direct construction
                optionsBuilder.UseSqlServer("Server=DESKTOP-6FQVN92\\SQLEXPRESS; Database=SeronDesignDb; Integrated Security=true; TrustServerCertificate=true;");
            }
        }

        public DbSet<Toy> Toys { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<CustomDesign> CustomDesigns { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
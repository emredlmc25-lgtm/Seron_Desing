using Microsoft.EntityFrameworkCore;

namespace SeronDesign.Models
{
    public class SeronDesignDbContext : DbContext
    {
        public SeronDesignDbContext(DbContextOptions<SeronDesignDbContext> options)
            : base(options)
        {
        }

        public DbSet<Toy> Toys { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<CustomDesign> CustomDesigns { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
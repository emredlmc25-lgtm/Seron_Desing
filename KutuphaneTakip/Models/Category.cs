namespace SeronDesign.Models
{
    public class Category
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public ICollection<Toy> Toys { get; set; } = new List<Toy>();

        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}

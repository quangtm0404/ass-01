namespace Domains.Entities
{
    public class Category
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = default!;
        public ICollection<Product> Products { get; set; } = default!;
    }
}

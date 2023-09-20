namespace eStoreClient.Models.ProductDTO
{
    public class ProductUpdateDTO : ProductCreateDTO
    {
        public Guid Id { get; set; } = default!;
    }
}

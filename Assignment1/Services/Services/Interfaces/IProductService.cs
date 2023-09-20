using Services.ViewModels.ProductDTO;

namespace Services.Services.Interfaces
{
	public interface IProductService
	{
		ProductDTO GetProductById(Guid id);
		IEnumerable<ProductDTO> Search(string search);
		IEnumerable<ProductDTO> GetProducts();
		ProductDTO CreateProduct(ProductCreateDTO product);
		ProductDTO UpdateProduct(ProductUpdateDTO product);
		bool DeleteProduct(Guid id);
	}
}

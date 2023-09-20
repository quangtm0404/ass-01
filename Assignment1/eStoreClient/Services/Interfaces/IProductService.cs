using eStoreClient.Models.ProductDTO;
using eStoreClient.ViewModels;

namespace eStoreClient.Services.Interfaces
{
    public interface IProductService
    {
        Task<ResponseDTO?> GetAllProductsAsync();
        Task<ResponseDTO?> GetProductByIdAsync(Guid Id);
        Task<ResponseDTO?> CreateProductAsync(ProductCreateDTO createDTO);
        Task<ResponseDTO?> DeleteProductAsync(Guid id);
        Task<ResponseDTO?> UpdateProductAsync(ProductUpdateDTO productDTO);
        Task<ResponseDTO?> SearchProductAsync(string search);
    }
}

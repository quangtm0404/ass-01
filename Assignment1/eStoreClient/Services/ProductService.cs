using eStoreClient.Models.ProductDTO;
using eStoreClient.Services.Interfaces;
using eStoreClient.Utilities;
using eStoreClient.ViewModels;
using static eStoreClient.Utilities.StaticDetail;

namespace eStoreClient.Services
{
	public class ProductService : IProductService
	{
		private readonly IBaseService _baseService;
		public ProductService(IBaseService baseService)
		{
			_baseService = baseService;
		}
		public async Task<ResponseDTO?> CreateProductAsync(ProductCreateDTO createDTO)
		{
			return await _baseService.SendAsync(new RequestDTO
			{
				APIType = APIType.POST,
				URL = $"{StaticDetail.eStoreAPIBase}/api/products",
				Data = createDTO
			});
		}

		public async Task<ResponseDTO?> DeleteProductAsync(Guid id)
		{
			return await _baseService.SendAsync(new RequestDTO
			{
				APIType = APIType.DELETE,
				URL = $"{StaticDetail.eStoreAPIBase}/api/products/{id}"
			});
		}

		public async Task<ResponseDTO?> GetAllProductsAsync()
		{
			return await _baseService.SendAsync(new RequestDTO
			{
				APIType = APIType.GET,
				URL = $"{StaticDetail.eStoreAPIBase}/api/products"
			});
		}

		public async Task<ResponseDTO?> GetProductByIdAsync(Guid Id)
		{
			return await _baseService.SendAsync(new RequestDTO
			{
				APIType = APIType.GET,
				URL = $"{StaticDetail.eStoreAPIBase}/api/products/{Id}"
			});
		}

		public async Task<ResponseDTO?> SearchProductAsync(string search) => await _baseService.SendAsync(new RequestDTO
		{
			APIType = APIType.GET,
			URL = $"{StaticDetail.eStoreAPIBase}/api/products?search={search}"
		});

		public async Task<ResponseDTO?> UpdateProductAsync(ProductUpdateDTO productDTO)
		{
			return await _baseService.SendAsync(new RequestDTO
			{
				APIType = APIType.PUT,
				URL = $"{StaticDetail.eStoreAPIBase}/api/products",
				Data = productDTO
			});
		}
	}
}

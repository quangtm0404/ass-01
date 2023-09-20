using eStoreClient.Services.Interfaces;
using eStoreClient.Utilities;
using eStoreClient.ViewModels;

namespace eStoreClient.Services
{
	public class CategoryService : ICategoryService
	{
		private readonly IBaseService service;
		public CategoryService(IBaseService service)
		{
			this.service = service;
		}

		public async Task<ResponseDTO?> GetCategories()
		{
			return await service.SendAsync(new RequestDTO
			{
				APIType = Utilities.StaticDetail.APIType.GET,
				URL = $"{StaticDetail.eStoreAPIBase}/api/categories"
			});
		}
	}
}

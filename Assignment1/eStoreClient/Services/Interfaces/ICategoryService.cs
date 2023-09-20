using eStoreClient.ViewModels;

namespace eStoreClient.Services.Interfaces
{
	public interface ICategoryService
	{
		Task<ResponseDTO?> GetCategories();
	}
}

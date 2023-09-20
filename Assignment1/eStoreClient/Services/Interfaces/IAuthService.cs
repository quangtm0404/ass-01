using eStoreClient.Models.Auth;
using eStoreClient.ViewModels;

namespace eStoreClient.Services.Interfaces
{
	public interface IAuthService
	{
		public Task<ResponseDTO?> LoginAsync(LoginRequestDTO loginRequestDTO);
		public Task<ResponseDTO?> RegisterAsync(CreateMemberDTO model);
	}
}

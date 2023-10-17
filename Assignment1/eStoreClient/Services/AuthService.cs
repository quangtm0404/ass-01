using eStoreClient.Models.Auth;
using eStoreClient.Services.Interfaces;
using eStoreClient.Utilities;
using eStoreClient.ViewModels;
using static eStoreClient.Utilities.StaticDetail;

namespace eStoreClient.Services
{
	public class AuthService : IAuthService
	{
		private readonly IBaseService _baseService;
		public AuthService(IBaseService baseService)
		{
			_baseService = baseService;
		}
		public async Task<ResponseDTO?> LoginAsync(LoginRequestDTO loginRequestDTO)
		{
			return await _baseService.SendAsync(new RequestDTO
			{
				APIType = APIType.POST,
				Data = loginRequestDTO,
				URL = $"{StaticDetail.eStoreAPIBase}/api/auth"
			}, withBearer: false);
		}

		public async Task<ResponseDTO?> RegisterAsync(CreateMemberDTO model)
		{
			return await _baseService.SendAsync(new RequestDTO
			{
				APIType = APIType.POST,
				Data = model,
				URL = $"{StaticDetail.eStoreAPIBase}/api/members"
			});
		}
	}
}

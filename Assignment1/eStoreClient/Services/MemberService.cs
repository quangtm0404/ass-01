using eStoreClient.Models.Auth;
using eStoreClient.Services.Interfaces;
using eStoreClient.Utilities;
using eStoreClient.ViewModels;

namespace eStoreClient.Services
{
    public class MemberService : IMemberService
    {
        private readonly IBaseService _baseService;
        public MemberService(IBaseService baseService)
        {
            _baseService = baseService;
        }
        public async Task<ResponseDTO?> DeleteMember(Guid id)
        {
            return await _baseService.SendAsync(new RequestDTO
            {
                APIType = Utilities.StaticDetail.APIType.DELETE,
                URL = $"{StaticDetail.eStoreAPIBase}/api/members/{id}"
            });
        }

        public async Task<ResponseDTO?> GetAll()
        => await _baseService.SendAsync(new RequestDTO
        {
            APIType = StaticDetail.APIType.GET,
            URL = $"{StaticDetail.eStoreAPIBase}/api/members"
        });

        public async Task<ResponseDTO?> GetByEmail(string email)
            => await _baseService.SendAsync(new RequestDTO
            {
                APIType = StaticDetail.APIType.GET,
                URL = $"{StaticDetail.eStoreAPIBase}/api/members/profile/{email}"
            });


        public async Task<ResponseDTO?> GetMemberById(Guid id) =>
            await _baseService.SendAsync(new RequestDTO
            {
                APIType = StaticDetail.APIType.GET,
                URL = $"{StaticDetail.eStoreAPIBase}/api/members/{id}"
            });


        public async Task<ResponseDTO?> UpdateMemberProfile(MemberDTO model)
        => await _baseService.SendAsync(new RequestDTO
        {
            APIType = StaticDetail.APIType.PUT,
            URL = $"{StaticDetail.eStoreAPIBase}/api/members",
            Data = model
        });
    }
}

using eStoreClient.Models.Auth;
using eStoreClient.ViewModels;

namespace eStoreClient.Services.Interfaces
{
    public interface IMemberService
    {
        Task<ResponseDTO?> GetAll();
        Task<ResponseDTO?> GetMemberById(Guid id);
        Task<ResponseDTO?> UpdateMemberProfile(MemberDTO model);
        Task<ResponseDTO?> DeleteMember(Guid id);
        Task<ResponseDTO?> GetByEmail(string email);

    }
}

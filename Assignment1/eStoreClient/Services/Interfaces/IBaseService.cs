using eStoreClient.ViewModels;

namespace eStoreClient.Services.Interfaces
{
    public interface IBaseService
    {
        Task<ResponseDTO?> SendAsync(RequestDTO request, bool withBearer = true);
    }
}

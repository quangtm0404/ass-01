using eStoreClient.Services.Interfaces;
using eStoreClient.ViewModels;
using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace eStoreClient.Services
{
    public class BaseService : IBaseService
    {
        // Dung de request API
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ITokenProvider _tokenProvider;
        public BaseService(IHttpClientFactory httpClientFactory, ITokenProvider tokenProvider)
        {
            _httpClientFactory = httpClientFactory;
            _tokenProvider = tokenProvider;
        }
        public async Task<ResponseDTO?> SendAsync(RequestDTO requestDTO, bool withBearer = true)
        {
            try
            {

                HttpClient client = _httpClientFactory.CreateClient("MangoAPI");
                HttpRequestMessage message = new();
                message.Headers.Add("Accept", "application/json");

                // Token
                if (withBearer)
                {
                    var accessToken = _tokenProvider.GetToken();
                    message.Headers.Add("Authorization", $"Bearer {accessToken}");
                }
                message.RequestUri = new Uri(requestDTO.URL);
                if (requestDTO.Data != null)
                {
                    message.Content = new StringContent(JsonConvert.SerializeObject(requestDTO.Data), encoding: Encoding.UTF8, "application/json");
                }

                HttpResponseMessage? apiResponse = null;
                switch (requestDTO.APIType)
                {
                    case Utilities.StaticDetail.APIType.GET:
                        message.Method = HttpMethod.Get;
                        break;
                    case Utilities.StaticDetail.APIType.POST:
                        message.Method = HttpMethod.Post;
                        break;
                    case Utilities.StaticDetail.APIType.PUT:
                        message.Method = HttpMethod.Put;
                        break;
                    case Utilities.StaticDetail.APIType.DELETE:
                        message.Method = HttpMethod.Delete;
                        break;
                    default:
                        message.Method = HttpMethod.Get;
                        break;
                }

                apiResponse = await client.SendAsync(message);

                switch (apiResponse.StatusCode)
                {
                    case HttpStatusCode.NotFound:
                        return new() { IsSuccess = false, Message = "Not found!" };
                    case HttpStatusCode.Forbidden:
                        return new() { IsSuccess = false, Message = "Forbidden" };
                    case HttpStatusCode.Unauthorized:
                        return new() { IsSuccess = false, Message = "Unauthorized" };
                    case HttpStatusCode.InternalServerError:
                        return new() { IsSuccess = false, Message = "Internal Server Error" };
                    case HttpStatusCode.BadRequest:
                        return new() { IsSuccess = false, Message = await apiResponse.Content.ReadAsStringAsync() };
                    case HttpStatusCode.NoContent:
                        return new() { IsSuccess = true, Message = "Success" };
                    default:
                        var apiContent = await apiResponse.Content.ReadAsStringAsync();
                        var apiResponseDTO = JsonConvert.DeserializeObject<ResponseDTO>(apiContent);
                        return apiResponseDTO;


                }
            }
            catch (Exception ex)
            {
                return new()
                {
                    IsSuccess = false,
                    Message = ex.Message,
                };
            }

        }
    }
}

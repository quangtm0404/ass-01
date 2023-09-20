using static eStoreClient.Utilities.StaticDetail;

namespace eStoreClient.ViewModels
{
    public class RequestDTO
    {
        public APIType APIType { get; set; } = APIType.GET;
        public string URL { get; set; } = default!;
        public object Data { get; set; } = default!;
        public string AccessToken { get; set; } = default!;
    }
}

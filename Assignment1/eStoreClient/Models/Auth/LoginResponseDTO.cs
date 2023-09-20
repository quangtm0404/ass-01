namespace eStoreClient.Models.Auth
{
    public class LoginResponseDTO
    {
        public MemberDTO Member { get; set; } = default!;
        public string Token { get; set; } = "";
    }
}

namespace eStoreClient.Models.Auth
{
    public class CreateMemberDTO
    {
        public string Email { get; set; } = default!;
        public string CompanyName { get; set; } = default!;
        public string City { get; set; } = default!;
        public string Country { get; set; } = default!;
        public string Password { get; set; } = default!;
    }
}

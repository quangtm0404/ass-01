namespace eStoreClient.Models.Auth
{
    public class UpdateMemberDTO
        : CreateMemberDTO
    {
        public Guid Id { get; set; } = default!;
        public string RoleName { get; set; } = default!;
    }
}

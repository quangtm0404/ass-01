using Domains.Entities;

namespace Services.ViewModels
{
    public class MemberDTO
    {
        public Guid Id { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string CompanyName { get; set; } = default!;
        public string City { get; set; } = default!;
        public string Country { get; set; } = default!;
        public string Password { get; set; } = default!;
        public string RoleName { get; set; } = default!;
        public ICollection<Order> Orders { get; set; } = default!;
    }

    public class CreateMemberDTO
    {
        public string Email { get; set; } = default!;
        public string CompanyName { get; set; } = default!;
        public string City { get; set; } = default!;
        public string Country { get; set; } = default!;
        public string Password { get; set; } = default!;
    }

    public class UpdateMemberDTO : CreateMemberDTO
    {
        public Guid Id { get; set; } = default!;
    }

    public class LoginMemberDTO
    {
        public string Email { get; set; } = default!;
        public string Password { get; set; } = default!;
    }
}

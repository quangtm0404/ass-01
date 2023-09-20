namespace Domains.Entities;

public class Member
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Email { get; set; } = default!;
    public string CompanyName { get; set; } = default!;
    public string City { get; set; } = default!;
    public string Country { get; set; } = default!;
    public string Password { get; set; } = default!;
    public string RoleName { get; set; } = "MEMBER";
    public ICollection<Order> Orders { get; set; } = default!;

}

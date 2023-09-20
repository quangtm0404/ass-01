using Services.ViewModels;

namespace Services.Services.Interfaces
{
    public interface ITokenGenerator
    {
        string GenerateToken(MemberDTO member);
    }
}

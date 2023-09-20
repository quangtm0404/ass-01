using Services.ViewModels;

namespace Services.Services.Interfaces
{
    public interface IMemberService
    {
        MemberDTO Login(LoginMemberDTO loginDTO);
        IEnumerable<MemberDTO> GetMembers();
        MemberDTO CreateMember(CreateMemberDTO member);
        MemberDTO UpdateMember(UpdateMemberDTO member);
        bool DeleteMember(Guid id);
        MemberDTO GetMember(string email);

    }
}

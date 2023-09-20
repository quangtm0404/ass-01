using AutoMapper;
using Domains.Entities;
using Services.Repositories;
using Services.Services.Interfaces;
using Services.ViewModels;

namespace Services.Services
{
    public class MemberService : IMemberService
    {
        private readonly IMapper _mapper;
        private readonly IMemberRepository _memberRepository;
        private readonly IOrderRepository _orderRepository;
        public MemberService(IMapper mapper, IMemberRepository memberRepository, IOrderRepository orderRepository)
        {
            _mapper = mapper;
            _memberRepository = memberRepository;
            _orderRepository = orderRepository;
        }
        public MemberDTO CreateMember(CreateMemberDTO member)
        {
            if (_memberRepository.Find(x => x.Email == member.Email).Count > 0)
                throw new Exception("Email duplicated!");
            _memberRepository.Insert(_mapper.Map<Member>(member));
            if (_memberRepository.SaveChanges())
                return _mapper.Map<MemberDTO>(_memberRepository.Find(x => x.Email == member.Email).First());
            else throw new Exception("Save changes failed!");
        }

        public bool DeleteMember(Guid id)
        {
            var member = _memberRepository.Find(x => x.Id == id).FirstOrDefault();
            if (member is not null)
            {
                if (_orderRepository.Find(x => x.MemberId == member.Id).FirstOrDefault() is not null)
                {
                    throw new Exception("Member ordered already! Can not delete!");
                }
                _memberRepository.Delete(member);
                return _memberRepository.SaveChanges();
            }
            else
            {
                throw new Exception("Not found!");
            }
        }

        public MemberDTO GetMember(string email) => _mapper.Map<MemberDTO>(_memberRepository.Find(x => x.Email == email, x => x.Orders).First());




        public IEnumerable<MemberDTO> GetMembers()
            => _mapper.Map<IEnumerable<MemberDTO>>(_memberRepository.GetAll(x => x.Orders));


        public MemberDTO Login(LoginMemberDTO loginDTO)
        {
            var result = _memberRepository.Find(x => x.Email == loginDTO.Email && x.Password == loginDTO.Password, x => x.Orders).FirstOrDefault();
            if (result is not null)
            {
                return _mapper.Map<MemberDTO>(result);
            }
            else
            {
                throw new Exception("Login failed! Wrong email or password");
            }

        }

        public MemberDTO UpdateMember(UpdateMemberDTO member)
        {
            _memberRepository.Update(_mapper.Map<Member>(member));
            if (_memberRepository.SaveChanges())
            {
                return _mapper.Map<MemberDTO>(_memberRepository.Find(x => x.Id == member.Id, x => x.Orders).First());
            }
            else
            {
                throw new Exception("Update failed!");
            }
        }
    }
}

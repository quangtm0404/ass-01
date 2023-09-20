using FluentValidation;
using Services.ViewModels;

namespace eStoreAPI.Validations
{
	public class MemberCreateValidation : AbstractValidator<CreateMemberDTO>
	{
		public MemberCreateValidation()
		{
			RuleFor(x => x.Email).EmailAddress();
			RuleFor(x => x.Password).NotEmpty();
		}
	}
}

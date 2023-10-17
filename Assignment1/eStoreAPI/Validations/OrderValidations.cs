using FluentValidation;
using Services.ViewModels.OrderDTO;

namespace eStoreAPI.Validations
{
	public class OrderCreateValidation : AbstractValidator<OrderCreateDTO>
	{
		public OrderCreateValidation()
		{
			RuleFor(x => x.Freight).GreaterThan(0);
			RuleFor(x => x.MemberId).NotNull().NotEmpty();
		}
	}
}

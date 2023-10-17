using FluentValidation;
using Services.ViewModels.ProductDTO;

namespace eStoreAPI.Validations
{
	public class ProductValidation : AbstractValidator<ProductCreateDTO>
	{
		public ProductValidation()
		{
			RuleFor(x => x.UnitsInStock).GreaterThan(0);
			RuleFor(x => x.UnitPrice).GreaterThan(0);
			RuleFor(x => x.Name).NotEmpty();
			RuleFor(x => x.Weight).GreaterThan(0);
		}



	}

}

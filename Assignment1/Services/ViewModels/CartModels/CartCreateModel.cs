namespace Services.ViewModels.CartModels
{
	public class CartCreateModel
	{
		public Guid ProductId { get; set; } = default!;
		public int Quantity { get; set; } = default!;
		public Guid userId { get; set; } = default!;
	}
}

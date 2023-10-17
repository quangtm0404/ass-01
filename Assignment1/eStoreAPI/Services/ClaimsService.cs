using Services.Services.Interfaces;
using System.Security.Claims;

namespace eStoreAPI.Services
{
	public class ClaimsService : IClaimsService
	{
		public ClaimsService(IHttpContextAccessor httpContextAccessor)
		{
			var userId = httpContextAccessor.HttpContext!.User.FindFirstValue(ClaimTypes.NameIdentifier);
			GetCurrentUser = string.IsNullOrEmpty(userId) ? Guid.Empty : Guid.Parse(userId);
		}
		public Guid GetCurrentUser { get; }
	}
}

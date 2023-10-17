using Microsoft.AspNetCore.Mvc;
using Services.Services.Interfaces;
using Services.ViewModels;

namespace eStoreAPI.Controllers
{
	[Route("api")]
	[ApiController]
	public class AuthenticationController : ControllerBase
	{
		private readonly ITokenGenerator _tokenGen;
		private readonly IMemberService _memberService;
		private ResponseDTO response;
		public AuthenticationController(ITokenGenerator tokenGen, IMemberService memberService)
		{
			_tokenGen = tokenGen;
			_memberService = memberService;
			response = new();
		}

		[HttpPost("auth")]
		public IActionResult Login([FromBody] LoginMemberDTO model)
		{
			var result = _memberService.Login(model);
			if (result is not null)
			{
				response.Result = new LoginResponseDTO
				{
					Member = result,
					Token = _tokenGen.GenerateToken(result)
				};
				return Ok(response);
			}
			else throw new Exception("Login failed");
		}

		[HttpPost("members")]
		public IActionResult Register([FromBody] CreateMemberDTO model)
		{
			var result = _memberService.CreateMember(model);
			if (result is not null)
			{
				response.Result = result;
				return Ok(response);
			}
			else
			{
				throw new Exception("Register failed!");
			}
		}


	}
}


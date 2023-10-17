using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Services.Interfaces;
using Services.ViewModels;

namespace eStoreAPI.Controllers
{
	[Route("api/[controller]s")]
	[ApiController]
	public class MemberController : ControllerBase
	{
		private readonly IMemberService _memberService;
		private ResponseDTO _response;
		public MemberController(IMemberService memberService)
		{
			_memberService = memberService;
			_response = new();
		}
		[HttpGet]
		public IActionResult GetAll()
		{
			_response.Result = _memberService.GetMembers();
			return Ok(_response);
		}

		[HttpGet("{Id}")]
		public IActionResult GetById(Guid Id)
		{
			var result = _memberService.GetMembers().FirstOrDefault(x => x.Id == Id);
			if (result is not null)
			{
				_response.Result = result;
				return Ok(_response);
			}
			else throw new Exception("Not found!");
		}

		[Authorize(Roles = "MEMBER")]
		[HttpPut]
		public IActionResult Update(UpdateMemberDTO model)
		{
			var result = _memberService.UpdateMember(model);
			if (result is not null)
			{
				return NoContent();
			}
			else
			{
				throw new Exception("Update member failed!");
			}
		}

		[HttpDelete("{Id}")]
		[Authorize(Roles = "ADMIN")]
		public IActionResult Delete(Guid Id)
		{
			var result = _memberService.DeleteMember(Id);
			if (result)
				return NoContent();
			else throw new Exception("Delete member failed!");
		}

		[HttpGet("profile/{email}")]
		public IActionResult GetByEmail(string email)
		{
			_response.Result = _memberService.GetMember(email);
			return Ok(_response);
		}

	}
}

using eStoreClient.Models.Auth;
using eStoreClient.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace eStoreClient.Controllers
{
	public class MemberController : Controller
	{
		private readonly IMemberService _memberService;
		public MemberController(IMemberService memberService)
		{
			_memberService = memberService;
		}
		[HttpGet]
		public IActionResult Index()
		{

			return View();
		}

		[HttpGet]
		public async Task<IActionResult> Details(Guid id)
		{
			var result = await _memberService.GetMemberById(id);
			if (result is not null && result.IsSuccess)
			{
				return View(JsonConvert.DeserializeObject<MemberDTO>(Convert.ToString(result.Result)!));
			}
			else
			{
				TempData["error"] = "Not found";
				return RedirectToAction(nameof(Index));
			}
		}

		[HttpGet]
		public async Task<IActionResult> Profile(string email)
		{
			var result = await _memberService.GetByEmail(email);
			if (result is not null && result.IsSuccess)
			{
				var member = JsonConvert.DeserializeObject<MemberDTO>(Convert.ToString(result.Result)!);
				return View(new UpdateMemberDTO
				{
					Id = member!.Id,
					City = member.City,
					CompanyName = member.CompanyName,
					Country = member.Country,
					Email = member.Email,
					Password = member.Password,
					RoleName = member.RoleName
				});
			}
			else
			{
				TempData["error"] = "Not found";
				return RedirectToAction(nameof(Index));
			}

		}
		[HttpPost]
		public async Task<IActionResult> Profile(MemberDTO member)
		{
			var result = await _memberService.UpdateMemberProfile(member);
			if (result is not null && result.IsSuccess)
			{
				TempData["success"] = "Update your profile successfully!";
				return RedirectToAction(nameof(Profile), new { email = member.Email });
			}
			else
			{
				TempData["error"] = result!.Message;
				return View(member);
			}
		}

		[HttpGet]
		public async Task<IActionResult> Delete(Guid id)
		{
			var result = await _memberService.GetMemberById(id);
			if (result is not null && result.IsSuccess)
			{
				return View(JsonConvert.DeserializeObject<MemberDTO>(Convert.ToString(result.Result)!));
			}
			else
			{
				TempData["error"] = "Not found";
				return RedirectToAction(nameof(Index));
			}
		}

		[HttpPost]
		public async Task<IActionResult> Delete(MemberDTO member)
		{
			var result = await _memberService.DeleteMember(member.Id);
			if (result is not null && result.IsSuccess)
			{
				TempData["success"] = "Delete member successfully!";
				return RedirectToAction(nameof(Index));
			}
			else
			{
				TempData["error"] = "Delete member failed!";
				return RedirectToAction(nameof(Delete), new { id = member.Id });

			}

		}
	}
}

using eStoreClient.Models.Auth;
using eStoreClient.Services.Interfaces;
using eStoreClient.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace eStoreClient.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;
        private readonly ITokenProvider _tokenProvider;
        public AuthController(IAuthService _authService, ITokenProvider tokenProvider)
        {
            this._authService = _authService;
            _tokenProvider = tokenProvider;
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View(new CreateMemberDTO());
        }
        [HttpPost]
        public async Task<IActionResult> Register(CreateMemberDTO model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            else
            {
                var result = await _authService.RegisterAsync(model);
                if (result is not null && result.IsSuccess)
                {
                    TempData["success"] = "Register successfully!";
                    return RedirectToAction(nameof(Login));
                }
                else
                {
                    return View(model);
                }
            }
        }
        [HttpGet]
        public IActionResult Login()
        {
            LoginRequestDTO loginRequestDTO = new();
            return View(loginRequestDTO);
        }
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            _tokenProvider.ClearToken();
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginRequestDTO model)
        {
            ResponseDTO? response = await _authService.LoginAsync(model);
            if (response is not null && response.IsSuccess)
            {
                LoginResponseDTO? responseDTO = JsonConvert.DeserializeObject<LoginResponseDTO>(Convert.ToString(response.Result)!);
                await SignInUser(responseDTO!);
                _tokenProvider.SetToken(responseDTO!.Token);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("CustomError", response!.Message);
                return View(model);
            }
        }

        private async Task SignInUser(LoginResponseDTO model)
        {
            // Get Token, Read Token, And assign claims into identity
            var handler = new JwtSecurityTokenHandler();
            var jwt = handler.ReadJwtToken(model.Token);
            var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
            identity.AddClaim(new Claim(JwtRegisteredClaimNames.Email, jwt.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Email)!.Value));
            identity.AddClaim(new Claim(JwtRegisteredClaimNames.Sub, jwt.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Sub)!.Value));
            identity.AddClaim(new Claim(JwtRegisteredClaimNames.Name, jwt.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Email)!.Value));
            identity.AddClaim(new Claim(ClaimTypes.Role, jwt.Claims.FirstOrDefault(x => x.Type == "role")!.Value));
            var principal = new ClaimsPrincipal(identity);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
        }
    }
}

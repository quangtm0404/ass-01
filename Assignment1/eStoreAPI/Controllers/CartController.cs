using Microsoft.AspNetCore.Mvc;
using Services.ViewModels;

namespace eStoreAPI.Controllers;
[ApiController]
[Route("api/[controller]")]
public class CartController : ControllerBase
{
    private readonly ResponseDTO _response;
    public CartController()
    {
        _response = new();
    }

    
}
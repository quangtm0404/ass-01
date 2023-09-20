using Microsoft.AspNetCore.Mvc;
using Services.Services.Interfaces;
using Services.ViewModels;

namespace eStoreAPI.Controllers
{
    [Route("api/categories")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        private ResponseDTO response;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
            response = new();
        }
        [HttpGet]
        public IActionResult Get()
        {
            response.Result = _categoryService.GetCategories();
            return Ok(response);
        }
    }
}

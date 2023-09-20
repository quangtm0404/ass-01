using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Services.Interfaces;
using Services.ViewModels;
using Services.ViewModels.ProductDTO;

namespace eStoreAPI.Controllers
{
    [Route("api/[controller]s")]
    [ApiController]
    [Authorize(Roles = "ADMIN")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private ResponseDTO response;
        public ProductController(IProductService productService)
        {
            _productService = productService;
            response = new();
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            var result = _productService.GetProducts();
            if (result is not null && result!.Count() > 0)
            {
                response.Result = result;
                return Ok(response);
            }
            else throw new Exception("Not have any product");

        }
        [HttpGet("search/{search}")]
        public IActionResult Search(string search)
        {

            var result = _productService.Search(search);
            response.Result = result;
            return Ok(response);
        }
        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            var result = _productService.GetProductById(id);
            if (result is not null)
            {
                response.Result = result;
                return Ok(response);
            }
            else
            {
                throw new Exception("Not found");
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody] ProductCreateDTO model)
        {

            var result = _productService.CreateProduct(model);
            if (result is not null)
            {
                return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
            }
            else throw new Exception("Create Product Failed!");
        }

        [HttpPut]
        public IActionResult Put([FromBody] ProductUpdateDTO model)
        {
            var result = _productService.UpdateProduct(model);
            if (result is not null)
            {
                return NoContent();
            }
            else
            {
                throw new Exception("Update failed!");
            }

        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {

            var result = _productService.DeleteProduct(id);
            if (result)
            {
                return NoContent();
            }
            else
            {
                throw new Exception("Delete Product Failed!");
            }
        }
    }
}

using Domains.Entities;
using eStoreClient.Models.ProductDTO;
using eStoreClient.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

namespace eStoreClient.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        public ProductController(IProductService _productService, ICategoryService categoryService)
        {
            this._productService = _productService;
            _categoryService = categoryService;
        }
        public async Task<IActionResult> Index()
        {
            var result = await _productService.GetAllProductsAsync();
            if (result is not null && result.IsSuccess)
            {
                return View(JsonConvert.DeserializeObject<IEnumerable<ProductDTO>>(Convert.ToString(result.Result)!));
            }
            else if (result!.Message == "Not have any product")
            {
                TempData["error"] = "Not have any product, must create!";
                return RedirectToAction(nameof(Create));
            }
            else
            {
                TempData["error"] = result!.Message;
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var result = JsonConvert.DeserializeObject<IEnumerable<Category>>(Convert.ToString((await _categoryService.GetCategories())!.Result)!);

            ViewData["CategoryId"] = new SelectList(result, "Id", "Name");
            return View(new ProductCreateDTO());
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductCreateDTO model)
        {
            if (!ModelState.IsValid)
            {
                var result = JsonConvert.DeserializeObject<IEnumerable<Category>>(Convert.ToString((await _categoryService.GetCategories())!.Result)!);
                ViewData["CategoryId"] = new SelectList(result, "CategoryId", "CategoryName");
                TempData["error"] = "Validation failed!";
                return View(model);
            }
            else
            {
                var result = await _productService.CreateProductAsync(model);
                if (result is not null && result.IsSuccess)
                {

                    TempData["success"] = "Create successfully!";

                    return RedirectToAction(nameof(Index), "Product");


                }
                else
                {
                    var category = JsonConvert.DeserializeObject<IEnumerable<Category>>(Convert.ToString((await _categoryService.GetCategories())!.Result)!);
                    ViewData["CategoryId"] = new SelectList(category, "CategoryId", "CategoryName");
                    TempData["error"] = "Create failed!";
                    return View(model);
                }
            }
        }

        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            var result = await _productService.GetProductByIdAsync(id);
            if (result is not null && result.IsSuccess)
            {
                return View(JsonConvert.DeserializeObject<ProductDTO>(Convert.ToString(result.Result)!));
            }
            else
            {
                TempData["error"] = "Not found";
                return RedirectToAction(nameof(Index), "Product");
            }
        }

        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _productService.GetProductByIdAsync(id);
            if (result is not null && result.IsSuccess)
            {
                return View(JsonConvert.DeserializeObject<ProductDTO>(Convert.ToString(result.Result)!));
            }
            else
            {
                TempData["error"] = "Not found";
                return RedirectToAction(nameof(Index), "Product");
            }
        }
        [HttpPost]
        public async Task<IActionResult> Delete(ProductDTO product)
        {
            var result = await _productService.DeleteProductAsync(product.Id);
            if (result is not null && result.IsSuccess)
            {
                TempData["success"] = "Delete Successfully!";

            }
            else
            {
                TempData["error"] = "Delete failed!";

            }
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public async Task<IActionResult> Update(Guid id)
        {
            var result = await _productService.GetProductByIdAsync(id);
            if (result is not null && result.IsSuccess)
            {
                var productDTO = JsonConvert.DeserializeObject<ProductDTO>(Convert.ToString(result.Result)!);
                var product = new ProductUpdateDTO
                {
                    Id = productDTO!.Id,
                    Name = productDTO.Name,
                    UnitPrice = productDTO.UnitPrice,
                    CategoryId = productDTO.Category.Id,
                    UnitsInStock = productDTO.UnitsInStock,
                    Weight = productDTO.Weight
                };
                var categories = JsonConvert.DeserializeObject<IEnumerable<Category>>(Convert.ToString((await _categoryService.GetCategories())!.Result)!);
                ViewData["CategoryId"] = new SelectList(categories, "Id", "Name");

                return View(product);
            }
            else
            {
                TempData["error"] = "Not found";
                return RedirectToAction("Index", "Product");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Update(ProductUpdateDTO model)
        {
            if (!ModelState.IsValid)
            {
                var categories = JsonConvert.DeserializeObject<IEnumerable<Category>>(Convert.ToString((await _categoryService.GetCategories())!.Result)!);
                ViewData["CategoryId"] = new SelectList(categories, "Id", "Name");
                return View(model);
            }
            var result = await _productService.UpdateProductAsync(model);
            if (result is not null && result.IsSuccess)
            {
                TempData["success"] = "Update successfully!";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                TempData["error"] = "Update failed!";
                var categories = JsonConvert.DeserializeObject<IEnumerable<Category>>(Convert.ToString((await _categoryService.GetCategories())!.Result)!);
                ViewData["CategoryId"] = new SelectList(categories, "Id", "Name");
                return View(model);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Search(string search)
        {
            var result = await _productService.SearchProductAsync(search);
            if (result is not null && result.IsSuccess)
            {
                var prodList = JsonConvert.DeserializeObject<IEnumerable<ProductDTO>>(Convert.ToString(result.Result)!);
                return View(nameof(Index), prodList);
            }
            else
            {
                TempData["error"] = "Not found any product by your keyword";
                return RedirectToAction(nameof(Index));
            }

        }



    }
}

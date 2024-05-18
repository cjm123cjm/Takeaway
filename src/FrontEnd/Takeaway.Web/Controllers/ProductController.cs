using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Takeaway.Web.Models;
using Takeaway.Web.Service.IService;

namespace Takeaway.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }
        public async Task<IActionResult> ProductIndex()
        {
            var response = await _productService.GetProductsAsync();
            List<ProductDto>? list = new List<ProductDto>();
            if (response != null && response.IsSuccess)
            {
                list = JsonConvert.DeserializeObject<List<ProductDto>>(Convert.ToString(response.Result));
            }
            else
            {
                TempData["error"] = response?.Message;
            }
            return View(list);
        }

        [HttpGet]
        public IActionResult ProductCreate()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ProductCreate(ProductDto productDto)
        {
            if (ModelState.IsValid)
            {
                var response = await _productService.CreateProductAsync(productDto);
                if (response != null && response.IsSuccess)
                {
                    TempData["success"] = "添加成功";
                    return RedirectToAction("ProductIndex");
                }
                else
                {
                    TempData["error"] = "添加失败";
                }
            }
            return View(productDto);
        }

        [HttpGet]
        public async Task<IActionResult> ProductEdit(string id)
        {
            var response = await _productService.GetProductAsync(id);

            ProductDto? productDto = new ProductDto();
            if (response != null && response.IsSuccess)
            {
                productDto = JsonConvert.DeserializeObject<ProductDto>(Convert.ToString(response.Result));
            }
            else
            {
                TempData["error"] = response?.Message;
            }
            return View(productDto);
        }

        [HttpPost]
        public async Task<IActionResult> ProductEdit(ProductDto productDto)
        {
            if (ModelState.IsValid)
            {
                var response = await _productService.UpdateProductAsync(productDto);
                if (response != null && response.IsSuccess)
                {
                    TempData["success"] = "修改成功";
                    return RedirectToAction("ProductIndex");
                }
                else
                {
                    TempData["error"] = "修改失败";
                }
            }
            return View(productDto);
        }

        [HttpGet]
        public async Task<IActionResult> ProductDelete(string id)
        {
            var response = await _productService.GetProductAsync(id);

            ProductDto? productDto = new ProductDto();
            if (response != null && response.IsSuccess)
            {
                productDto = JsonConvert.DeserializeObject<ProductDto>(Convert.ToString(response.Result));
            }
            else
            {
                TempData["error"] = response?.Message;
            }
            return View(productDto);
        }

        [HttpPost]
        public async Task<IActionResult> ProductDelete(ProductDto product)
        {
            var response = await _productService.DeleteProductAsync(product.ProductId);

            if (response != null && response.IsSuccess)
            {
                TempData["success"] = "删除成功";
                return RedirectToAction("ProductIndex");
            }
            else
            {
                TempData["error"] = response?.Message;
            }
            return View(product);
        }
    }
}

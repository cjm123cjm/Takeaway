using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Takeaway.Web.Models;
using Takeaway.Web.Service.IService;

namespace Takeaway.Web.Controllers
{
    public class HomeController : Controller
	{
		private readonly IProductService _productService;

        public HomeController(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<IActionResult> Index()
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
        [Authorize]
        public async Task<IActionResult> Detail(string id)
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
    }
}
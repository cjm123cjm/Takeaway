using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using Takeaway.Web.Models;
using Takeaway.Web.Models.Cart;
using Takeaway.Web.Service.IService;

namespace Takeaway.Web.Controllers
{
    public class HomeController : Controller
	{
		private readonly IProductService _productService;
        private readonly ICartService _cartService;

        public HomeController(IProductService productService, ICartService cartService)
        {
            _productService = productService;
            _cartService = cartService;
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

        [HttpPost]
        [Authorize]
        [ActionName("Detail")]
        public async Task<IActionResult> Detail(ProductDto productDto)
        {
            string userId = User.Claims.Where(t => t.Type == JwtRegisteredClaimNames.Sub).FirstOrDefault()?.Value;
            CardHeaderDto cardHeaderDto = new CardHeaderDto(userId);

            CardDetailsDto cardDetailsDto = new CardDetailsDto
            {
                ProductDto = productDto,
                ProductId = productDto.ProductId,
                Quantity = productDto.Count
            };

            cardHeaderDto.CardDetails.Add(cardDetailsDto);

            var response = await _cartService.UpdateBasketAsync(cardHeaderDto);

            if (response != null && response.IsSuccess)
            {
                TempData["success"] = "添加购物车成功";
                return RedirectToAction("Index");
            }
            else
            {
                TempData["error"] = response?.Message;
            }
            return View(productDto);
        }
    }
}
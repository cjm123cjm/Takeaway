using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using Takeaway.Web.Models.Cart;
using Takeaway.Web.Service.IService;

namespace Takeaway.Web.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        public async Task<IActionResult> CartIndex()
        {
            string userId = User.Claims.Where(t => t.Type == JwtRegisteredClaimNames.Sub).FirstOrDefault()?.Value;
            var response = await _cartService.GetBasketAsync(userId);
            CardHeaderDto cardHeaderDto = null;
            if (response != null && response.IsSuccess)
            {
                cardHeaderDto = JsonConvert.DeserializeObject<CardHeaderDto>(Convert.ToString(response.Result));
            }
            else
            {
                cardHeaderDto = new CardHeaderDto(userId);
            }
            return View(cardHeaderDto);
        }

        [HttpPost]
        public async Task<IActionResult> ApplyCoupon(CardHeaderDto cardHeaderDto)
        {
            var response = await _cartService.ApplyCouponAsync(cardHeaderDto);
            if (response != null && response.IsSuccess)
            {
                TempData["success"] = "申请折扣成功";
                return RedirectToAction("CartIndex");
            }
            return View(cardHeaderDto);
        }

        [HttpPost]
        public async Task<IActionResult> RemoveCoupon(CardHeaderDto cardHeaderDto)
        {
            cardHeaderDto.CouponCode = "";
            var response = await _cartService.ApplyCouponAsync(cardHeaderDto);
            if (response != null && response.IsSuccess)
            {
                TempData["success"] = "申请移除成功";
                return RedirectToAction("CartIndex");
            }
            return View(cardHeaderDto);
        }

        [HttpPost]
        public async Task<IActionResult> Remove(string productId)
        {
            RemoveProduct removeProduct = new RemoveProduct
            {
                UserId = User.Claims.FirstOrDefault(t => t.Type == JwtRegisteredClaimNames.Sub)?.Value,
                ProductId = productId
            };
            var response = await _cartService.RemoveBasketAsync(removeProduct);
            if (response != null && response.IsSuccess)
            {
                TempData["success"] = "商品移除成功";
                return RedirectToAction("CartIndex");
            }
            else
            {
                TempData["error"] = "商品移除失败";
                return View();
            }
        }
    }
}

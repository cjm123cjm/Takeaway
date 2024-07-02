using Takeaway.Web.Models;
using Takeaway.Web.Models.Cart;
using Takeaway.Web.Service.IService;
using Takeaway.Web.Utility;

namespace Takeaway.Web.Service
{
    public class CartService : ICartService
    {
        private readonly IBaseService _baseService;

        public CartService(IBaseService baseService)
        {
            _baseService = baseService ?? throw new ArgumentNullException(nameof(baseService));
        }

        public async Task<ResponseDto?> ApplyCouponAsync(CardHeaderDto cart)
        {
            return await _baseService.SendAsync(new RequestDto
            {
                ApiType = SD.ApiType.POST,
                Data = cart,
                Url = SD.ShoppingCartAPIBase + "/api/cart/ApplyCoupon"
            });
        }

        public async Task<ResponseDto?> ClearCartAsync(string userId)
        {
            return await _baseService.SendAsync(new RequestDto
            {
                ApiType = SD.ApiType.DELETE,
                Url = SD.ShoppingCartAPIBase + "/api/cart/" + userId
            });
        }

        public async Task<ResponseDto?> GetBasketAsync(string userId)
        {
            return await _baseService.SendAsync(new RequestDto
            {
                ApiType = SD.ApiType.GET,
                Url = SD.ShoppingCartAPIBase + "/api/cart/" + userId
            });
        }

        public async Task<ResponseDto?> RemoveBasketAsync(RemoveProduct removeProduct)
        {
            return await _baseService.SendAsync(new RequestDto
            {
                ApiType = SD.ApiType.POST,
                Data = removeProduct,
                Url = SD.ShoppingCartAPIBase + "/api/cart/RemoveBasket"
            });
        }

        public async Task<ResponseDto?> RemoveCouponAsync(CardHeaderDto cart)
        {
            return await _baseService.SendAsync(new RequestDto
            {
                ApiType = SD.ApiType.POST,
                Data = cart,
                Url = SD.ShoppingCartAPIBase + "/api/cart/RemoveCoupon"
            });
        }

        public async Task<ResponseDto?> UpdateBasketAsync(CardHeaderDto cart)
        {
            return await _baseService.SendAsync(new RequestDto
            {
                ApiType = SD.ApiType.POST,
                Data = cart,
                Url = SD.ShoppingCartAPIBase + "/api/cart/UpdateBasket"
            });
        }
    }
}

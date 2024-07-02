using Takeaway.Web.Models;
using Takeaway.Web.Models.Cart;

namespace Takeaway.Web.Service.IService
{
    public interface ICartService
    {
        /// <summary>
        /// 获取购物车
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<ResponseDto?> GetBasketAsync(string userId);

        /// <summary>
        /// 添加购物车
        /// </summary>
        /// <param name="cart"></param>
        /// <returns></returns>
        Task<ResponseDto?> UpdateBasketAsync(CardHeaderDto cart);

        /// <summary>
        /// 移除商品
        /// </summary>
        /// <param name="cart"></param>
        /// <returns></returns>
        Task<ResponseDto?> RemoveBasketAsync(RemoveProduct removeProduct);

        /// <summary>
        /// 清空购物车
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<ResponseDto?> ClearCartAsync(string userId);

        /// <summary>
        /// 添加优惠卷
        /// </summary>
        /// <returns></returns>
        Task<ResponseDto?> ApplyCouponAsync(CardHeaderDto cart);

        /// <summary>
        /// 移除优惠卷
        /// </summary>
        /// <returns></returns>
        Task<ResponseDto?> RemoveCouponAsync(CardHeaderDto cart);
    }
}

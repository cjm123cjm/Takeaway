using Takeaway.Services.ShoppingCartAPI.Models.Dtos;

namespace Takeaway.Services.ShoppingCartAPI.Repositories
{
    public interface ICartRepository
    {
        /// <summary>
        /// 获取购物车
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<CardHeaderDto> GetBasketAsync(string userId);

        /// <summary>
        /// 添加购物车
        /// </summary>
        /// <param name="cart"></param>
        /// <returns></returns>
        Task UpdateBasketAsync(CardHeaderDto cart);

        /// <summary>
        /// 移除商品
        /// </summary>
        /// <param name="cart"></param>
        /// <returns></returns>
        Task RemoveBasketAsync(RemoveProduct removeProduct);

        /// <summary>
        /// 清空购物车
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task ClearCartAsync(string userId);

        /// <summary>
        /// 添加优惠卷
        /// </summary>
        /// <returns></returns>
        Task<bool> ApplyCouponAsync(CardHeaderDto cart);

        /// <summary>
        /// 移除优惠卷
        /// </summary>
        /// <returns></returns>
        Task<bool> RemoveCouponAsync(CardHeaderDto cart);
    }
}

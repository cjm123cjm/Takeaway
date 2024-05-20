using Takeaway.Services.ShoppingCartAPI.Models.Dtos;

namespace Takeaway.Services.ShoppingCartAPI.Repositories
{
    public interface ICartRepository
    {
        Task<CardHeaderDto> GetBasketAsync(int userId);
        Task<CardHeaderDto> UpdateBasketAsync(CardHeaderDto cart);
        Task<CardHeaderDto> RemoveBasketAsync(CardHeaderDto cart);
        Task ClearCartAsync(int userId);
    }
}

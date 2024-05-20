using AutoMapper;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using Takeaway.Services.ShoppingCartAPI.Models;
using Takeaway.Services.ShoppingCartAPI.Models.Dtos;

namespace Takeaway.Services.ShoppingCartAPI.Repositories
{
    public class CartRepository : ICartRepository
    {
        private readonly IDistributedCache _redisCache;
        private readonly IMapper _mapper;
        public CartRepository(IDistributedCache redisCache, IMapper mapper)
        {
            _redisCache = redisCache;
            _mapper = mapper;
        }
        public async Task<CardHeaderDto> GetBasketAsync(int userId)
        {
            var cardHeader = await _redisCache.GetStringAsync(userId.ToString());
            
            if (string.IsNullOrEmpty(cardHeader))
                return new CardHeaderDto(userId);

            return JsonConvert.DeserializeObject<CardHeaderDto>(cardHeader)!;
        }
        public async Task<CardHeaderDto> UpdateBasketAsync(CardHeaderDto cart)
        {
            var modelCart = await _redisCache.GetStringAsync(cart.UserId.ToString());
            var card = _mapper.Map<CardHeader>(cart);
            if (string.IsNullOrEmpty(modelCart))
            {
                await _redisCache.SetStringAsync(card.UserId.ToString(), JsonConvert.SerializeObject(card));
            }
            else
            {
                var dto = JsonConvert.DeserializeObject<CardHeader>(modelCart)!;
                var updateModel = card.CardDetails.First();
                if (updateModel != null)
                {
                    var any = dto.CardDetails.FirstOrDefault(t => t.ProductId == updateModel.ProductId);
                    if (any == null)
                        dto.CardDetails.Add(updateModel);
                    else
                        any.Quantity += updateModel.Quantity;
                }

                await _redisCache.SetStringAsync(card.UserId.ToString(), JsonConvert.SerializeObject(dto));
            }

            return await GetBasketAsync(cart.UserId);
        }
        public async Task<CardHeaderDto> RemoveBasketAsync(CardHeaderDto cart)
        {
            var modelCart = await _redisCache.GetStringAsync(cart.UserId.ToString());
            var card = _mapper.Map<CardHeader>(cart);
            if (!string.IsNullOrEmpty(modelCart))
            {
                var dto = JsonConvert.DeserializeObject<CardHeader>(modelCart)!;
                var updateModel = card.CardDetails.First();
                if (updateModel != null)
                {
                    var any = dto.CardDetails.FirstOrDefault(t => t.ProductId == updateModel.ProductId);
                    if (any == null)
                        dto.CardDetails.Remove(updateModel);
                }

                await _redisCache.SetStringAsync(card.UserId.ToString(), JsonConvert.SerializeObject(dto));
            }

            return await GetBasketAsync(cart.UserId);
        }
        public async Task ClearCartAsync(int userId)
        {
            await _redisCache.RemoveAsync(userId.ToString());
        }
    }
}

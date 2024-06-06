using AutoMapper;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using Takeaway.Services.ShoppingCartAPI.Models;
using Takeaway.Services.ShoppingCartAPI.Models.Dtos;
using Takeaway.Services.ShoppingCartAPI.Protos;
using static Grpc.Core.Metadata;

namespace Takeaway.Services.ShoppingCartAPI.Repositories
{
    public class CartRepository : ICartRepository
    {
        private readonly IDistributedCache _redisCache;
        private readonly IMapper _mapper;
        private readonly ProductProtoService.ProductProtoServiceClient _productProtoServiceClient;
        private readonly CouponProtoService.CouponProtoServiceClient _couponProtoServiceClient;
        public CartRepository(
            IDistributedCache redisCache,
            IMapper mapper,
            ProductProtoService.ProductProtoServiceClient productProtoServiceClient,
            CouponProtoService.CouponProtoServiceClient couponProtoServiceClient)
        {
            _redisCache = redisCache;
            _mapper = mapper;
            _productProtoServiceClient = productProtoServiceClient;
            _couponProtoServiceClient = couponProtoServiceClient;
        }

        /// <summary>
        /// 获取购物车信息
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<CardHeaderDto> GetBasketAsync(string userId)
        {
            var cardHeader = await _redisCache.GetStringAsync(userId);

            if (string.IsNullOrEmpty(cardHeader))
                return new CardHeaderDto(userId);

            var cardHeaderModel = JsonConvert.DeserializeObject<CardHeader>(cardHeader)!;
            var cardHeaderDto = _mapper.Map<CardHeaderDto>(cardHeaderModel);

            //获取产品信息
            var productIds = cardHeaderDto.CardDetails.Select(t => t.ProductId).ToList();
            ProductRequest productRequest = new ProductRequest();
            productRequest.ProductIds.AddRange(productIds);
            var productResponse = await _productProtoServiceClient.GetProductsAsync(productRequest);
            if (productResponse.IsSuccess)
            {
                var productDtos = _mapper.Map<List<ProductDto>>(productResponse.ProductDto);
                foreach (var item in cardHeaderDto.CardDetails)
                {
                    item.ProductDto = productDtos.FirstOrDefault(t => t.ProductId == item.ProductId);
                    cardHeaderDto.CartTotal += (item.Quantity * item.ProductDto.Price);
                }
            }

            //获取优惠卷信息
            var couponResponse = await _couponProtoServiceClient.GetCouponDtoAsync(new CouponRequest { CouponCode = cardHeaderDto.CouponCode });
            if (couponResponse.IsSuccess)
            {
                var coupon = _mapper.Map<CouponDto>(couponResponse.CouponDto);
                if (coupon.MinAmount < cardHeaderDto.CartTotal)
                {
                    cardHeaderDto.CartTotal -= decimal.Parse(coupon.DiscountAmount.ToString());
                    cardHeaderDto.DisCount = coupon.DiscountAmount;
                }
            }

            return cardHeaderDto;
        }

        /// <summary>
        /// 添加购物车
        /// </summary>
        /// <param name="cart"></param>
        /// <returns></returns>
        public async Task<CardHeaderDto> UpdateBasketAsync(CardHeaderDto cart)
        {
            var modelCart = await _redisCache.GetStringAsync(cart.UserId.ToString());
            if (string.IsNullOrEmpty(modelCart))
            {
                var entity = _mapper.Map<CardHeader>(cart);
                entity.CardDetails.Add(_mapper.Map<CardDetails>(cart.CardDetails.First()));

                //保存
                await _redisCache.SetStringAsync(cart.UserId.ToString(), JsonConvert.SerializeObject(entity));
            }
            else
            {
                var entity = JsonConvert.DeserializeObject<CardHeader>(modelCart);
                //判断有没有添加的产品
                var detail = entity.CardDetails.FirstOrDefault(t => t.ProductId == cart.CardDetails.First().ProductId);
                if (detail == null)
                {
                    entity.CardDetails.Add(_mapper.Map<CardDetails>(cart.CardDetails.First()));
                }
                else
                {
                    detail.Quantity += cart.CardDetails.First().Quantity;
                }

                //保存
                await _redisCache.SetStringAsync(cart.UserId.ToString(), JsonConvert.SerializeObject(entity));
            }

            return await GetBasketAsync(cart.UserId);
        }

        /// <summary>
        /// 移除商品(整个移除)
        /// </summary>
        /// <param name="cart"></param>
        /// <returns></returns>
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

        /// <summary>
        /// 清空购物车
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task ClearCartAsync(string userId)
        {
            await _redisCache.RemoveAsync(userId);
        }

        /// <summary>
        /// 添加优惠卷
        /// </summary>
        /// <returns></returns>
        public async Task<bool> ApplyCouponAsync(CardHeaderDto cart)
        {
            var cardHeader = await _redisCache.GetStringAsync(cart.UserId);

            if (string.IsNullOrEmpty(cardHeader))
                return false;

            var cardHeaderModel = JsonConvert.DeserializeObject<CardHeader>(cardHeader)!;

            cardHeaderModel.CouponCode = cart.CouponCode;

            await _redisCache.SetStringAsync(cart.UserId.ToString(), JsonConvert.SerializeObject(cardHeaderModel));

            return true;
        }

        /// <summary>
        /// 移除优惠卷
        /// </summary>
        /// <returns></returns>
        public async Task<bool> RemoveCouponAsync(CardHeaderDto cart)
        {
            var cardHeader = await _redisCache.GetStringAsync(cart.UserId);

            if (string.IsNullOrEmpty(cardHeader))
                return false;

            var cardHeaderModel = JsonConvert.DeserializeObject<CardHeader>(cardHeader)!;

            cardHeaderModel.CouponCode = "";

            await _redisCache.SetStringAsync(cart.UserId.ToString(), JsonConvert.SerializeObject(cardHeaderModel));

            return true;
        }
    }
}

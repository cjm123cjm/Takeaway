using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Takeaway.Services.ShoppingCartAPI.Models.Dtos;
using Takeaway.Services.ShoppingCartAPI.Repositories;

namespace Takeaway.Services.ShoppingCartAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ShoppingCartController : ControllerBase
    {
        private readonly ICartRepository _cartRepository;
        private readonly ILogger _logger;
        private ResponseDto _responseDto;

        public ShoppingCartController(ICartRepository cartRepository, ILogger logger)
        {
            _cartRepository = cartRepository;
            _responseDto = new ResponseDto();
            _logger = logger;
        }

        [HttpGet]
        public async Task<ResponseDto> GetBasket(string userId)
        {
            try
            {
                var result = await _cartRepository.GetBasketAsync(userId);
                _responseDto.Result = result;
            }
            catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = ex.Message;
                _logger.LogError(ex, "GetBasket({userId})失败", userId);
            }
            return _responseDto;
        }

        [HttpPost("UpdateBasket")]
        public async Task<ResponseDto> UpdateBasket(CardHeaderDto cart)
        {
            try
            {
                var result = await _cartRepository.UpdateBasketAsync(cart);
                _responseDto.Result = result;
            }
            catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = ex.Message;
                _logger.LogError(ex, "UpdateBasket({cart})失败", cart);
            }
            return _responseDto;
        }

        [HttpPost("RemoveBasket")]
        public async Task<ResponseDto> RemoveBasket(CardHeaderDto cart)
        {
            try
            {
                var result = await _cartRepository.RemoveBasketAsync(cart);
                _responseDto.Result = result;
            }
            catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = ex.Message;
                _logger.LogError(ex, "RemoveBasket({cart})失败", cart);
            }
            return _responseDto;
        }

        [HttpDelete]
        public async Task<ResponseDto> ClearCart(string userId)
        {
            try
            {
                await _cartRepository.ClearCartAsync(userId);
                _responseDto.Result = true;
            }
            catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = ex.Message;
                _logger.LogError(ex, "ClearCart({userId})失败", userId);
            }
            return _responseDto;
        }

        [HttpPost("ApplyCoupon")]
        public async Task<ResponseDto> ApplyCoupon(CardHeaderDto cart)
        {
            try
            {
                var result = await _cartRepository.ApplyCouponAsync(cart);
                if (!result)
                {
                    _responseDto.Message = "购物车不存在";
                }
                _responseDto.Result = result;
            }
            catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = ex.Message;
                _logger.LogError(ex, "ApplyCoupon({cart})失败", cart);
            }
            return _responseDto;
        }

        [HttpPost("RemoveCoupon")]
        public async Task<ResponseDto> RemoveCoupon(CardHeaderDto cart)
        {
            try
            {
                var result = await _cartRepository.RemoveCouponAsync(cart);
                if (!result)
                {
                    _responseDto.Message = "购物车不存在";
                }
                _responseDto.Result = result;
            }
            catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = ex.Message;
                _logger.LogError(ex, "RemoveCoupon({cart})失败", cart);
            }
            return _responseDto;
        }
    }
}

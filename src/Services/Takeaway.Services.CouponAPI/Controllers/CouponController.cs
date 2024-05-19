using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Takeaway.Services.CouponAPI.Models.Dtos;
using Takeaway.Services.CouponAPI.Repositories;

namespace Takeaway.Services.CouponAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CouponController : ControllerBase
    {
        private ResponseDto _responseDto;
        private readonly ICouponRepository _couponRepository;
        private readonly ILogger<CouponController> _logger;

        public CouponController(ICouponRepository couponRepository, ILogger<CouponController> logger)
        {
            _responseDto = new ResponseDto();
            _couponRepository = couponRepository;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ResponseDto> GetCoupons()
        {
            try
            {
                var couponDtos = await _couponRepository.GetCouponsAsync();
                _responseDto.Result = couponDtos;
            }
            catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = ex.Message;
                _logger.LogError(ex, "调用 GetCoupons() 出错");
            }
            return _responseDto;
        }

        [HttpGet("{id}")]
        public async Task<ResponseDto> GetCoupon(int id)
        {
            try
            {
                var couponDtos = await _couponRepository.GetCouponAsync(id);
                if (couponDtos == null)
                {
                    _logger.LogInformation("GetCoupon({id}) 不存在", id);
                    _responseDto.Message = "折扣不存在";
                    _responseDto.IsSuccess = false;
                }
                else
                {
                    _responseDto.Result = couponDtos;
                }
            }
            catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = ex.Message;
                _logger.LogError(ex, "调用 GetCoupon({id}) 出错", id);
            }
            return _responseDto;
        }

        [HttpGet("GetCouponByCode/{code}")]
        public async Task<ResponseDto> GetCouponByCode(string code)
        {
            try
            {
                var couponDtos = await _couponRepository.GetCouponByCodeAsync(code);
                if (couponDtos == null)
                {
                    _logger.LogInformation("GetCouponByCode({code}) 不存在", code);
                    _responseDto.Message = "折扣不存在";
                    _responseDto.IsSuccess = false;
                }
                else
                {
                    _responseDto.Result = couponDtos;
                }
            }
            catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = ex.Message;
                _logger.LogError(ex, "调用 GetCouponByCode({code}) 出错", code);
            }
            return _responseDto;
        }

        [HttpPost]
        [Authorize(Roles = "ADMIN")]
        public async Task<ResponseDto> AddCoupon([FromBody] CouponDto coupon)
        {
            try
            {
                var couponDtos = await _couponRepository.AddCouponAsync(coupon);
                if (!couponDtos)
                {
                    _logger.LogError("AddCoupon({coupon}) 添加失败", coupon);
                    _responseDto.Message = "添加失败";
                    _responseDto.IsSuccess = false;
                }
            }
            catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = ex.Message;
                _logger.LogError(ex, "调用 AddCoupon({coupon}) 出错", coupon);
            }
            return _responseDto;
        }

        [HttpPut]
        [Authorize(Roles = "ADMIN")]
        public async Task<ResponseDto> UpdateCoupon([FromBody] CouponDto coupon)
        {
            try
            {
                var couponDtos = await _couponRepository.UpdateCouponAsync(coupon);
                if (!couponDtos)
                {
                    _logger.LogError("UpdateCoupon({coupon}) 更新失败", coupon);
                    _responseDto.Message = "添加失败";
                    _responseDto.IsSuccess = false;
                }
            }
            catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = ex.Message;
                _logger.LogError(ex, "调用 UpdateCoupon({coupon}) 出错", coupon);
            }
            return _responseDto;
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "ADMIN")]
        public async Task<ResponseDto> DeletCoupon(int id)
        {
            try
            {
                var couponDtos = await _couponRepository.DeleteCouponAsync(id);
                if (!couponDtos)
                {
                    _logger.LogError("DeletCoupon({id}) 删除失败", id);
                    _responseDto.Message = "删除失败";
                    _responseDto.IsSuccess = false;
                }
            }
            catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = ex.Message;
                _logger.LogError(ex, "调用 DeletCoupon({id}) 出错", id);
            }
            return _responseDto;
        }
    }
}

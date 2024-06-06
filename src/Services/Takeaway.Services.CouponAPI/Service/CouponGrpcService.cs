﻿using AutoMapper;
using Grpc.Core;
using Takeaway.Services.CouponAPI.Protos;
using Takeaway.Services.CouponAPI.Repositories;

namespace Takeaway.Services.CouponAPI.Service
{
    public class CouponGrpcService : CouponProtoService.CouponProtoServiceBase
    {
        private readonly ICouponRepository _couponRepository;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;

        public CouponGrpcService(ICouponRepository couponRepository, ILogger logger, IMapper mapper)
        {
            _couponRepository = couponRepository;
            _logger = logger;
            _mapper = mapper;
        }
        public override async Task<CouponResponse> GetCouponDto(CouponRequest request, ServerCallContext context)
        {
            CouponResponse couponResponse = new CouponResponse();
            try
            {
                var couponDto = await _couponRepository.GetCouponByCodeAsync(request.CouponCode);

                _logger.LogInformation("根据优惠代码{code}获取优惠数据", request.CouponCode);

                couponResponse.IsSuccess = true;

                var proto = _mapper.Map<CouponProtoDto>(couponDto);

                couponResponse.CouponDto = proto;
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex, "根据优惠代码{code}获取优惠数据失败", request.CouponCode);
                couponResponse.IsSuccess = false;
                couponResponse.Message = "读取失败";
            }

            return couponResponse;
        }
    }
}

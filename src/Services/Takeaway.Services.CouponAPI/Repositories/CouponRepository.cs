using AutoMapper;
using Dapper;
using Npgsql;
using Takeaway.Services.CouponAPI.Models;
using Takeaway.Services.CouponAPI.Models.Dtos;

namespace Takeaway.Services.CouponAPI.Repositories
{
	public class CouponRepository : ICouponRepository
	{
		private readonly IConfiguration _configuration;
		private readonly IMapper _mapper;

		public CouponRepository(
			IConfiguration configuration,
			IMapper mapper
			)
		{
			_configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
			_mapper = mapper;
		}
		public async Task<IEnumerable<CouponDto>> GetCouponsAsync()
		{
			using var conn = new NpgsqlConnection(_configuration.GetConnectionString("CouponConnectionString"));

			var coupon = await conn.QueryAsync<Coupon>
				("SELECT * FROM Coupon");

			return _mapper.Map<IEnumerable<CouponDto>>(coupon);
		}

		public async Task<CouponDto?> GetCouponAsync(int id)
		{
			using var conn = new NpgsqlConnection(_configuration.GetConnectionString("CouponConnectionString"));

			var coupon = await conn.QueryFirstOrDefaultAsync<Coupon?>
				("SELECT * FROM Coupon where CouponId=@Id", new { Id = id });

			return _mapper.Map<CouponDto?>(coupon);
		}

		public async Task<CouponDto?> GetCouponByCodeAsync(string code)
		{
			using var conn = new NpgsqlConnection(_configuration.GetConnectionString("CouponConnectionString"));

			var coupon = await conn.QueryFirstOrDefaultAsync<Coupon?>
				("SELECT * FROM Coupon where lower(CouponCode)=@CouponCode", new { CouponCode = code.ToLower() });

			return _mapper.Map<CouponDto?>(coupon);
		}

		public async Task<bool> AddCouponAsync(CouponDto coupon)
		{
			using var conn = new NpgsqlConnection(_configuration.GetConnectionString("CouponConnectionString"));

			var affected = await conn.ExecuteAsync
				("INSERT INTO Coupon(CouponCode,DiscountAmount,MinAmount) VALUES(@CouponCode,@DiscountAmount,@MinAmount)",
				new { CouponCode = coupon.CouponCode, DiscountAmount = coupon.DiscountAmount, MinAmount = coupon.MinAmount });

			if (affected == 0)
				return false;
			return true;
		}

		public async Task<bool> UpdateCouponAsync(CouponDto coupon)
		{
			using var connextion = new NpgsqlConnection(_configuration.GetConnectionString("CouponConnectionString"));

			var affected = await connextion.ExecuteAsync
				("UPDATE Coupon SET CouponCode=@CouponCode,DiscountAmount=@DiscountAmount,MinAmount=@MinAmount Where CouponId=@CouponId",
				new { CouponCode = coupon.CouponCode, DiscountAmount = coupon.DiscountAmount, MinAmount = coupon.MinAmount, CouponId = coupon.CouponId });

			if (affected == 0)
				return false;
			return true;
		}

		public async Task<bool> DeleteCouponAsync(int id)
		{
			using var connextion = new NpgsqlConnection(_configuration.GetConnectionString("CouponConnectionString"));

			var affected = await connextion.ExecuteAsync
				("DELETE from Coupon Where CouponId=@CouponId",
				new { CouponId = id });

			if (affected == 0)
				return false;
			return true;
		}
	}
}

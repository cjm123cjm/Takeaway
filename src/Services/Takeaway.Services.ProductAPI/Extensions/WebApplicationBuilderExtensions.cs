using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Takeaway.Services.ProductAPI.Extensions
{
    public static class WebApplicationBuilderExtensions
    {
        /// <summary>
        /// 授权
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static WebApplicationBuilder AddAppAuthetication(this WebApplicationBuilder builder)
        {
            var secret = builder.Configuration.GetValue<string>("JwtOptions:Secret");
            var issuer = builder.Configuration.GetValue<string>("JwtOptions:Issuer");
            var audience = builder.Configuration.GetValue<string>("JwtOptions:Audience");
            var key = Encoding.ASCII.GetBytes(secret);
            builder.Services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidIssuer = issuer,
                    ValidAudience = audience,
                    ValidateAudience = true
                };
            });
            builder.Services.AddAuthorization();

            return builder;
        }
    }
}

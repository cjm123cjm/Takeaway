using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.OpenApi.Models;
using StackExchange.Redis;
using Takeaway.Services.ShoppingCartAPI.Extensions;
using Takeaway.Services.ShoppingCartAPI.Protos;
using Takeaway.Services.ShoppingCartAPI.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(option =>
{
    option.AddSecurityDefinition(name: "Bearer", securityScheme: new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Description = "Enter the Bearer Authorization string as fllowing: `Bearer Generated-JWT-Token`",
        In = ParameterLocation.Header,
        Scheme = "Bearer"
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference=new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id=JwtBearerDefaults.AuthenticationScheme
                }
            },new string[]{}
        }
    });
});
builder.AddAppAuthetication();
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetValue<string>("CartSetting:ConnectionStrings");
    options.InstanceName = builder.Configuration.GetValue<string>("CartSetting:InstanceName");
    //ConnectionMultiplexer.Connect("ip:¶Ë¿ÚºÅ£¬password=mypassword");
    //var redisLocation = builder.Configuration.GetValue<string>("CartSetting:ConnectionStrings");
    //var redisOptions = ConfigurationOptions.Parse(redisLocation);
    //redisOptions.Password = builder.Configuration.GetValue<string>("CartSetting:Password");//ÄãµÄredisÃÜÂë
});
builder.Services.AddAutoMapper(typeof(Program).Assembly);
builder.Services.AddScoped<ICartRepository, CartRepository>();

//httpclient
builder.Services.AddHttpClient("Product", config =>
{
    config.BaseAddress = new Uri(builder.Configuration["ServiceUrls:ProductAPI"].ToString());
});

//rpc
builder.Services.AddGrpcClient<ProductProtoService.ProductProtoServiceClient>(config =>
{
    config.Address = new Uri("http://localhost:8000");
});
builder.Services.AddHttpContextAccessor();
builder.Services.AddGrpcClient<CouponProtoService.CouponProtoServiceClient>(config =>
{
    config.Address = new Uri("http://localhost:8001");
}).AddCallCredentials(async (context, metadata) =>
{
    var serviceProvider = builder.Services.BuildServiceProvider()!;
    var httpContextAccessor = serviceProvider.GetRequiredService<IHttpContextAccessor>();
    string? token = await httpContextAccessor.HttpContext!.GetTokenAsync("access_token");
    if (!string.IsNullOrWhiteSpace(token))
    {
        metadata.Add("Authorization", $"Bearer {token}");
    }
}).ConfigureChannel(p => p.UnsafeUseInsecureChannelCallCredentials = true);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

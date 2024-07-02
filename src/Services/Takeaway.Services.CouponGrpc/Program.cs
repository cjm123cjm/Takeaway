using Microsoft.AspNetCore.Server.Kestrel.Core;
using Takeaway.Services.CouponGrpc.Extensions;
using Takeaway.Services.CouponGrpc.Repositories;
using Takeaway.Services.CouponGrpc.Service;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddScoped<ICouponRepository, CouponRepository>();

builder.Services.AddGrpc();
builder.WebHost.UseKestrel(option =>
{
    option.ConfigureEndpointDefaults(config =>
    {
        config.Protocols = HttpProtocols.Http2;
    });
});
builder.AddAppAuthetication();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();

app.MapGrpcService<CouponGrpcService>();

app.Run();
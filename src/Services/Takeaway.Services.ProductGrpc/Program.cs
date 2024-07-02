using Microsoft.AspNetCore.Server.Kestrel.Core;
using Takeaway.Services.ProductGrpc.Config;
using Takeaway.Services.ProductGrpc.Data;
using Takeaway.Services.ProductGrpc.Repositories;
using Takeaway.Services.ProductGrpc.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddGrpc();
builder.WebHost.UseKestrel(option =>
{
    option.ConfigureEndpointDefaults(config =>
    {
        config.Protocols = HttpProtocols.Http2;
    });
});
builder.Services.Configure<ProductDatabase>(builder.Configuration.GetSection("ProductDatabase"));
builder.Services.AddAutoMapper(typeof(Program).Assembly);
builder.Services.AddScoped<IProductContext, ProductContext>();
builder.Services.AddScoped<IProductRepository,ProductRepository>();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGrpcService<ProductService>();

app.Run();

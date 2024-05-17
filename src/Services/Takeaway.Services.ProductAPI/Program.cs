using Takeaway.Services.ProductAPI.Config;
using Takeaway.Services.ProductAPI.Data;
using Takeaway.Services.ProductAPI.Extensions;
using Takeaway.Services.ProductAPI.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<ProductDatabase>(builder.Configuration.GetSection("ProductDatabase"));
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.AddAppAuthetication();

builder.Services.AddScoped<IProductContext, ProductContext>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

app.Run();

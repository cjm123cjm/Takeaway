using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Takeaway.Services.AuthAPI.Data;
using Takeaway.Services.AuthAPI.Models;
using Takeaway.Services.AuthAPI.Repositories;
using Takeaway.Services.AuthAPI.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("AuthConnectionString"));
});
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();
builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection("ApiSettings:JwtOptions"));
builder.Services.AddScoped<IAuthRepository, AuthRepository>();
builder.Services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
ApplyMigration();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

void ApplyMigration()
{
    using (var scope = app.Services.CreateScope())
    {
        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        if (context.Database.GetPendingMigrations().Count() > 0)
        {
            context.Database.Migrate();
        }
    }
}
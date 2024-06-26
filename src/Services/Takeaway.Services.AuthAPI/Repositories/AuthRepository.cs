﻿using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Takeaway.Services.AuthAPI.Data;
using Takeaway.Services.AuthAPI.Models;
using Takeaway.Services.AuthAPI.Models.Dtos;
using Takeaway.Services.AuthAPI.Services;

namespace Takeaway.Services.AuthAPI.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly AppDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;

        public AuthRepository(
            AppDbContext db,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IJwtTokenGenerator jwtTokenGenerator)
        {
            _db = db;
            _userManager = userManager;
            _roleManager = roleManager;
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        public async Task<string> RegisterAsync(RegisterRequestDto registerRequestDto)
        {
            ApplicationUser applicationUser = new ApplicationUser
            {
                UserName = registerRequestDto.PhoneNumber,
                Email = registerRequestDto.Email,
                NormalizedEmail = registerRequestDto.Email.ToUpper(),
                Name = registerRequestDto.Name,
                PhoneNumber = registerRequestDto.PhoneNumber
            };
            try
            {
                var result = await _userManager.CreateAsync(applicationUser, registerRequestDto.Password);
                if (result.Succeeded)
                {
                    return "";
                }
                else
                {
                    return result.Errors.FirstOrDefault()!.Description;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<LoginResponseDto> LoginAsync(LoginRequestDto loginRequestDto)
        {
            LoginResponseDto loginResponseDto = new LoginResponseDto();
            var user = await _db.ApplicationUser.FirstOrDefaultAsync(t => t.UserName == loginRequestDto.UserName);
            if (user == null)
            {
                loginResponseDto.UserDto = null;
            }
            else
            {
                var valida = await _userManager.CheckPasswordAsync(user!, loginRequestDto.Password);

                if (!valida)
                {
                    loginResponseDto.UserDto = null;
                }
                else
                {
                    var roles = await _userManager.GetRolesAsync(user);
                    var token = _jwtTokenGenerator.GenerateToken(user, roles);
                    loginResponseDto.UserDto = new UserDto
                    {
                        Id = user.Id,
                        Email = user.Email,
                        Name = user.Name,
                        PhoneNumber = user.PhoneNumber,
                    };
                    loginResponseDto.Token = token;
                }
            }

            return loginResponseDto;
        }

        public async Task<bool> AssigenRoleAsync(string email, string roleName)
        {
            var user = await _db.ApplicationUser.FirstOrDefaultAsync(t => t.Email == email);
            if (user != null)
            {
                var roleExists = await _roleManager.RoleExistsAsync(roleName);
                if (!roleExists)
                {
                    await _roleManager.CreateAsync(new IdentityRole(roleName));
                }
                var result = await _userManager.AddToRoleAsync(user, roleName);
                if (result.Succeeded)
                    return true;
            }
            return false;
        }
    }
}

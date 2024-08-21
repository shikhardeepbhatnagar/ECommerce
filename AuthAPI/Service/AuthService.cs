﻿using AuthAPI.Data;
using AuthAPI.Models;
using AuthAPI.Models.Dto;
using AuthAPI.Service.IService;
using Microsoft.AspNetCore.Identity;

namespace AuthAPI.Service
{
    public class AuthService : IAuthService
    {
        private readonly AppDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly ILogger<AuthService> _logger;

        public AuthService(AppDbContext db, IJwtTokenGenerator jwtTokenGenerator,
            UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, ILogger<AuthService> logger)
        {
            _db = db;
            _jwtTokenGenerator = jwtTokenGenerator;
            _userManager = userManager;
            _roleManager = roleManager;
            _logger = logger;
        }

        public async Task<bool> AssignRole(string email, string roleName)
        {
            try
            {
                _logger.LogInformation("Assigning role starts");
                var user = _db.ApplicationUsers.FirstOrDefault(u => u.Email.ToLower() == email.ToLower());
                if (user != null)
                {
                    if (!_roleManager.RoleExistsAsync(roleName).GetAwaiter().GetResult())
                    {
                        //create role if it does not exist
                        _roleManager.CreateAsync(new IdentityRole(roleName)).GetAwaiter().GetResult();
                    }
                    await _userManager.AddToRoleAsync(user, roleName);
                    return true;
                }
                _logger.LogInformation("Assigning role starts");
                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception in assigining role. " + ex.Message);
                return false;
            }
        }

        public async Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto)
        {
            try
            {
                _logger.LogInformation("Login starts");
                var user = _db.ApplicationUsers.FirstOrDefault(u => u.UserName.ToLower() == loginRequestDto.UserName.ToLower());

                bool isValid = await _userManager.CheckPasswordAsync(user, loginRequestDto.Password);

                if (user == null || isValid == false)
                {
                    return new LoginResponseDto() { User = null, Token = "" };
                }

                //if user was found , Generate JWT Token
                var roles = await _userManager.GetRolesAsync(user);
                var token = _jwtTokenGenerator.GenerateToken(user, roles);

                UserDto userDTO = new()
                {
                    Email = user.Email,
                    ID = user.Id,
                    Name = user.Name,
                    PhoneNumber = user.PhoneNumber
                };

                LoginResponseDto loginResponseDto = new LoginResponseDto()
                {
                    User = userDTO,
                    Token = token
                };

                _logger.LogInformation("Login ends");
                return loginResponseDto;
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception in sign in. " + ex.Message);
                return new LoginResponseDto() { User = null, Token = "" };
            }
        }

        public async Task<string> Register(RegistrationRequestDto registrationRequestDto)
        {
            try
            {
                _logger.LogInformation("Registeration of user starts");
                ApplicationUser user = new()
                {
                    UserName = registrationRequestDto.Email,
                    Email = registrationRequestDto.Email,
                    NormalizedEmail = registrationRequestDto.Email.ToUpper(),
                    Name = registrationRequestDto.Name,
                    PhoneNumber = registrationRequestDto.PhoneNumber
                };


                var result = await _userManager.CreateAsync(user, registrationRequestDto.Password);
                if (result.Succeeded)
                {
                    var userToReturn = _db.ApplicationUsers.First(u => u.UserName == registrationRequestDto.Email);

                    UserDto userDto = new()
                    {
                        Email = userToReturn.Email,
                        ID = userToReturn.Id,
                        Name = userToReturn.Name,
                        PhoneNumber = userToReturn.PhoneNumber
                    };

                    _logger.LogInformation("Registeration of user ends and registeration is successful");
                    return "";

                }
                else
                {
                    _logger.LogInformation("Registeration of user ends and registeration is unsuccessful");
                    return result.Errors.FirstOrDefault().Description;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception in user registeration. " + ex.Message);
                return ex.Message;
            }
        }
    }
}

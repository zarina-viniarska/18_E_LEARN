using _18_E_LEARN.DataAccess.Data.Models.User;
using _18_E_LEARN.DataAccess.Data.ViewModels.User;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _18_E_LEARN.BusinessLogic.Services
{
    public class UserService
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly EmailService _emailService;
        private readonly IMapper _mapper;

        public UserService(EmailService emailService, IConfiguration configuration, IMapper mapper, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
            _configuration = configuration;
            _emailService = emailService;
        }

        public async Task<ServiceResponse> LoginUserAsync(LoginUserVM model)
        {
            AppUser user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                return new ServiceResponse
                {
                    Success = false,
                    Message = "User or password incorrect."
                };
            }

            var result = await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, lockoutOnFailure: true);
            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, false);
                return new ServiceResponse
                {
                    Success = true,
                    Message = "User logged in successfully."
                };
            }

            if (result.IsNotAllowed)
            {
                return new ServiceResponse
                {
                    Success = false,
                    Message = "Confirm your email please."
                };
            }

            if (result.IsLockedOut)
            {
                return new ServiceResponse
                {
                    Success = false,
                    Message = "Your account is locked. Connect with administrator."
                };
            }

            return new ServiceResponse
            {
                Success = false,
                Message = "User or password incorrect."
            };
        }

        public async Task<ServiceResponse> LogoutUserAsync()
        {
            await _signInManager.SignOutAsync();
            return new ServiceResponse
            {
                Success = true,
                Message = "User logged out."
            };
        }

        public async Task<ServiceResponse> RegisterUserAsync(RegisterUserVM model)
        {
            var mappedUser = _mapper.Map<RegisterUserVM, AppUser>(model);
            mappedUser.UserName = model.Email;
            var result = await _userManager.CreateAsync(mappedUser);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(mappedUser, model.Role);
                await SendConfirmationEmailAsync(mappedUser);
                return new ServiceResponse
                {
                    Success = true,
                    Message = "User successfully created.",
                };
            }

            List<IdentityError> errorList = result.Errors.ToList();
            string errors = "";

            foreach (var error in errorList)
            {
                errors = errors + error.Description.ToString();
            }

            return new ServiceResponse
            {
                Success = false,
                Message = errors
            };
        }

        public async Task<ServiceResponse> ConfirmEmailAsync(string userId, string token)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return new ServiceResponse
                {
                    Success = false,
                    Message = "User not found"
                };
            }

            var decodedToken = WebEncoders.Base64UrlDecode(token);
            string normalToken = Encoding.UTF8.GetString(decodedToken);

            var result = await _userManager.ConfirmEmailAsync(user, normalToken);

            if (result.Succeeded)
                return new ServiceResponse
                {
                    Message = "Email confirmed successfully!",
                    Success = true,
                };

            return new ServiceResponse
            {
                Success = false,
                Message = "Email did not confirm",
                Errors = result.Errors.Select(e => e.Description)
            };
        }

        public async Task SendConfirmationEmailAsync(AppUser newUser)
        {
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(newUser);

            var encodedEmailToken = Encoding.UTF8.GetBytes(token);
            var validEmailToken = WebEncoders.Base64UrlEncode(encodedEmailToken);

            string url = $"{_configuration["HostSettings:URL"]}/Admin/ConfirmEmail?userid={newUser.Id}&token={validEmailToken}";

            string emailBody = $"<h1>Confirm your email</h1> <a href='{url}'>Confirm now</a>";
            await _emailService.SendEmailAsync(newUser.Email, "Email confirmation.", emailBody);
        }
    }
}

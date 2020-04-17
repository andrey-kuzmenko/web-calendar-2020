using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using WebCalendar.Common.Contracts;
using WebCalendar.DAL.Models.Entities;
using WebCalendar.Services.Contracts;
using WebCalendar.Services.Models.User;
using Task = System.Threading.Tasks.Task;

namespace WebCalendar.Services.Implementation
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _config;

        public UserService(UserManager<User> userManager, SignInManager<User> signInManager, IMapper mapper, 
            IConfiguration config)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
            _config = config;
        }

        public Task<IEnumerable<UserServiceModel>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<UserServiceModel> GetByIdAsync(Guid id)
        {
            User user = await _userManager.FindByIdAsync(id.ToString());

            UserServiceModel userServiceModel = _mapper.Map<User, UserServiceModel>(user);

            return userServiceModel;
        }

        public Task UpdateAsync(UserEditionServiceModel entity)
        {
            throw new NotImplementedException();
        }

        public Task RemoveAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task RemoveAsync(UserServiceModel entity)
        {
            throw new NotImplementedException();
        }

        public async Task<UserServiceModel> GetByPrincipalAsync(ClaimsPrincipal principal)
        {
            string userId = _userManager.GetUserId(principal);
            
            User user = await _userManager.FindByNameAsync(userId);

            UserServiceModel userServiceModel = _mapper.Map<User, UserServiceModel>(user);

            return userServiceModel;
        }

        public async Task<IdentityResult> RegisterAsync(UserRegisterServiceModel userRegisterServiceModel)
        {
            User user = _mapper.Map<UserRegisterServiceModel, User>(userRegisterServiceModel);
            IdentityResult result = await _userManager.CreateAsync(user, userRegisterServiceModel.Password);
            return result;
        }

        public async Task<UserTokenServiceModel> AuthenticateAsync(UserAuthenticateServiceModel userAuthenticateServiceModel)
        {
            User user = await _userManager.FindByEmailAsync(userAuthenticateServiceModel.Email);

            UserTokenServiceModel userTokenServiceModel = _mapper.Map<User, UserTokenServiceModel>(user);

            SignInResult result = await _signInManager.PasswordSignInAsync(
                user.UserName,
                userAuthenticateServiceModel.Password,
                true,
                false
            );

            string token = GenerateJsonWebToken(user);
            userTokenServiceModel.Token = token;
            
            return userTokenServiceModel;
        }
        
        private string GenerateJsonWebToken(User user)
        {  
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));  
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);  
  
            var token = new JwtSecurityToken(
                _config["Jwt:Issuer"],  
                _config["Jwt:Audience"],  
                new Claim[]
                {
                    new Claim(JwtRegisteredClaimNames.Jti, user.Id.ToString()),
                    new Claim(JwtRegisteredClaimNames.GivenName, user.FirstName),
                    new Claim(JwtRegisteredClaimNames.FamilyName, user.LastName),
                    new Claim(JwtRegisteredClaimNames.Email, user.Email),
                    new Claim(JwtRegisteredClaimNames.Sub, user.UserName), 
                },  
                expires: DateTime.Now.AddMinutes(Double.Parse(_config["Jwt:Lifetime"])),  
                signingCredentials: credentials);  
  
            return new JwtSecurityTokenHandler().WriteToken(token);  
        }
        
        public async Task Logout()
        {
            await _signInManager.SignOutAsync();
        }
    }
}
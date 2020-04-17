using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebCalendar.Common.Contracts;
using WebCalendar.Services.Contracts;
using WebCalendar.Services.Models.User;
using WebCalendar.Web.Models;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace WebCalendar.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public AccountController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegistrationModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            
            UserRegisterServiceModel userRegisterServiceModel = _mapper
                .Map<RegistrationModel, UserRegisterServiceModel>(model);
            IdentityResult result = await _userService.RegisterAsync(userRegisterServiceModel);

            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }

            foreach (IdentityError identityError in result.Errors)
            {
                ModelState.AddModelError(String.Empty, identityError.Description);
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult Login(string returnUrl = null)
        {
            return View(new LoginModel { ReturnUrl = returnUrl });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel model)
        {
            UserLoginServiceModel userLoginServiceModel = _mapper.Map<LoginModel, UserLoginServiceModel>(model);
            SignInResult result = await _userService.LoginAsync(userLoginServiceModel);
            if (result.Succeeded)
            {
                if (!String.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
                {
                    return Redirect(model.ReturnUrl);
                }

                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError(String.Empty, "Incorrect email and / or password");

            return View(model);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _userService.Logout();
            return RedirectToAction("Login", "Account");
        }
    }
}
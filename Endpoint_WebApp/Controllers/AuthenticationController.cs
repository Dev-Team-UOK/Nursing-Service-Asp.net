using System.Security.Claims;
using Endpoint_WebApp.Models.ViewModels.Authentication;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Nursing_Service.Application.Services.Authentication.Command.SignUp;
using Nursing_Service.Application.Services.Users.Queries.SignIn;
using Nursing_Service.Common.Dto.Base;

namespace Endpoint_WebApp.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly ISignUpUserService _signUpUserService;
        private readonly ISignInUserService _signInUserService;

        public AuthenticationController(ISignUpUserService signUp,
            ISignInUserService signIn)
        {
            _signUpUserService = signUp;
            _signInUserService = signIn;
        }

        [Route("/Authentication/")]
        public IActionResult Authentication()
        {
            return View("Authentication");
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(string email, string password)
        {
            var signinResult = await _signInUserService.ExcuteAsync(new SignInUserRequestInfo
            {
                Email = email,
                Password = password
            });

            return Json(signinResult);
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpViewModel req)
        {
            if (User?.Identity?.IsAuthenticated == true)
            {
                return Json(new BaseResultDTO { IsSuccess = false, Message = "جهت ایجاد حساب کاربری جدید؛ ابتدا از حساب کاربری خود خارج شوید سپس مجددا اقدام فرمایید." });
            }

            var signupResult = await _signUpUserService.ExcuteAsync(new SignUpUserRequestInfo
            {
                Email = req.Email,
                Password = req.Password,
                UserName = req.UserName,
                Phone = req.Phone
            });

            return Json(signupResult);
        }

        public async Task<IActionResult> SignOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Index", "Home");
        }
    }
}

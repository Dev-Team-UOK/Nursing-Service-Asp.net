using System.Security.Claims;
using Endpoint_WebApp.Models.ViewModels.Authentication;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Nursing_Service.Application.Services.Authentication.Command.SignUp;
using Nursing_Service.Application.Services.Nurse.Command.Create;
using Nursing_Service.Application.Services.SuperVisor.Command.Create;
using Nursing_Service.Application.Services.Users.Queries.SignIn;
using Nursing_Service.Common.Dto.Base;
using Nursing_Service.Domain.Entities.User;

namespace Endpoint_WebApp.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly ISignUpUserService _signUpUserService;
        private readonly ISignInUserService _signInUserService;
        private readonly ICreateNurseService _createNurseService;
        private readonly ICreateSuperVisor _createSuperVisor;

        public AuthenticationController(ISignUpUserService signUp,
            ISignInUserService signIn,
            ICreateNurseService createNurseService,
            ICreateSuperVisor createSuperVisor)
        {
            _signUpUserService = signUp;
            _signInUserService = signIn;
            _createNurseService = createNurseService;
            _createSuperVisor = createSuperVisor;
        }

        [Route("/Authentication/")]
        public IActionResult Authentication()
        {
            return View("Authentication");
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(string email, string password)
        {
            if (email == "admin" && password == "admin")
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, "admin"),
                    new Claim(ClaimTypes.Role, "Admin")
                };

                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);
                await HttpContext.SignInAsync(principal);

                return Json(new BaseResultDTO<SignInUserResultDto>
                {
                    IsSuccess = true,
                    Message = "مدیر وارد شدید",
                    Data = null
                });
            }
            else
            {
                var signinResult = await _signInUserService.ExcuteAsync(new SignInUserRequestInfo
                {
                    Email = email,
                    Password = password
                });

                if (signinResult.IsSuccess is true)
                {
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, signinResult.Data!.UserName),
                        new Claim(ClaimTypes.Role, signinResult.Data!.Role.ToString()),
                    };

                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var principal = new ClaimsPrincipal(identity);
                    await HttpContext.SignInAsync(principal);
     
                }

                return Json(signinResult);
            }
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpViewModel req)
        {
            if (User?.Identity?.IsAuthenticated == true)
            {
                return Json(new BaseResultDTO { IsSuccess = false, Message = "جهت ایجاد حساب کاربری جدید؛ ابتدا از حساب کاربری خود خارج شوید سپس مجددا اقدام فرمایید." });
            }

            if (req.Role is "operator")
            {
                var signupResult = await _signUpUserService.ExcuteAsync(new SignUpUserRequestInfo
                {
                    Email = req.Email,
                    Password = req.Password,
                    UserName = req.UserName,
                    Phone = req.Phone,
                    FirstName = req.Name,
                    LastName = req.LastName
                });

                if (signupResult.IsSuccess is true)
                {
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, signupResult.Data!.UserName),
                        new Claim(ClaimTypes.Role, signupResult.Data!.Role.ToString()),
                    };

                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var principal = new ClaimsPrincipal(identity);
                    await HttpContext.SignInAsync(principal);
                }

                return Json(signupResult);
            }
            else if (req.Role is "nurse")
            {
                var createNurseResult = await _createNurseService.ExcuteAsync(new CreateNurseRequestInfo
                {
                    Email = req.Email,
                    Password = req.Password,
                    UserName = req.UserName,
                    PhoneNumber = req.Phone,
                    FirstName = req.Name,
                    LastName = req.LastName,
                    NurseNumber = req.NurseNumber,
                    StartWorkingInCompany = DateTime.Now,
                    WorkHistoryInYear = req.NurseWorkYear,
                    DoService = req.NurseDoService,
                });

                if (createNurseResult.IsSuccess is true)
                {
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, req.UserName),
                        new Claim(ClaimTypes.Role, RoleEnum.Nurse.ToString()),
                    };

                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var principal = new ClaimsPrincipal(identity);
                    await HttpContext.SignInAsync(principal);
                }

                return Json(createNurseResult);
            }
            else if (req.Role is "supervisor")
            {
                var createSuperVisorResult = await _createSuperVisor.ExcuteAsync(new CreateSuperVisorRequestInfo
                {
                    Email = req.Email,
                    Password = req.Password,
                    UserName = req.UserName,
                    PhoneNumber = req.Phone,
                    FirstName = req.Name,
                    LastName = req.LastName,
                    Shift =
                        req.Shift == "morning" ? Nursing_Service.Domain.Entities.SuperVisor.Shift.Morning :
                        req.Shift == "evening" ? Nursing_Service.Domain.Entities.SuperVisor.Shift.Evening :
                        Nursing_Service.Domain.Entities.SuperVisor.Shift.Night
                });

                if (createSuperVisorResult.IsSuccess is true)
                {
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, req.UserName),
                        new Claim(ClaimTypes.Role, RoleEnum.SuperVisor.ToString()),
                    };

                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var principal = new ClaimsPrincipal(identity);
                    await HttpContext.SignInAsync(principal);
                }

                return Json(createSuperVisorResult);
            }
            else
                throw new ArgumentOutOfRangeException(nameof(req.Role));
        }

        public async Task<IActionResult> SignOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Index", "Home");
        }
    }
}

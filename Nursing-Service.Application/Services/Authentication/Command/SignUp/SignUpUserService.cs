using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Nursing_Service.Application.Interfaces.Contexts;
using Nursing_Service.Common.Consts.Regex;
using Nursing_Service.Common.Dto.Base;
using Nursing_Service.Common.Extensions;
using Nursing_Service.Domain.Entities.User;

namespace Nursing_Service.Application.Services.Authentication.Command.SignUp
{
    public class SignUpUserService : ISignUpUserService
    {
        private readonly IDataBaseContext _context;

        public SignUpUserService(IDataBaseContext context)
        {
            _context = context;
        }

        public async Task<BaseResultDTO<SignUpUserResultDto>> ExcuteAsync(SignUpUserRequestInfo req)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(req.Email))
                    throw new Exception("Email cant be null.");
                if (string.IsNullOrWhiteSpace(req.Password))
                    throw new Exception("Password cant be null.");
                if (string.IsNullOrWhiteSpace(req.UserName))
                    throw new Exception("UserName cant be null.");
                if (string.IsNullOrWhiteSpace(req.Phone))
                    throw new Exception("Phone cant be null.");

                if (Regex.Match(req.Email, RegexValidations.Email, RegexOptions.IgnoreCase).Success is false)
                    throw new FormatException("ایمیل معتبر نیست.");

                var passHasher = new PasswordHasher();

                var user = new User(
                    userName: req.UserName,
                    email: req.Email,
                    phoneNumber: req.Phone,
                    password: passHasher.HashPassword(req.Password),
                    firstName: null,
                    lastName: null
                );

                await _context.Users.AddAsync(user);
                await _context.SaveChangesAsync();

                return new BaseResultDTO<SignUpUserResultDto>
                {
                    IsSuccess = true,
                    Message = "کاربر با موفقیت ایجاد شد.",
                    Data = new SignUpUserResultDto
                    {
                        Id = user.Id,
                        UserName = user.UserName,
                        Phone = user.PhoneNumber,
                    }
                };
            }
            catch (Exception ex)
            {
                return new BaseResultDTO<SignUpUserResultDto>
                {
                    IsSuccess = false,
                    Message = ex.Message,
                    Data = null
                };
            }
        }
    }
}

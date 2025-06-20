﻿using Microsoft.EntityFrameworkCore;
using Nursing_Service.Application.Interfaces.Contexts;
using Nursing_Service.Common.Dto.Base;
using Nursing_Service.Common.Extensions;
using Nursing_Service.Domain.Entities.User;

namespace Nursing_Service.Application.Services.Users.Queries.SignIn
{
    public class SignInUserService : ISignInUserService
    {
        private readonly IDataBaseContext _context;

        public SignInUserService(IDataBaseContext context)
        {
            _context = context;
        }

        public async Task<BaseResultDTO<SignInUserResultDto>> ExcuteAsync(SignInUserRequestInfo req)
        {
            if (string.IsNullOrWhiteSpace(req.Email) || string.IsNullOrWhiteSpace(req.Password))
                return new BaseResultDTO<SignInUserResultDto>
                {
                    IsSuccess = false,
                    Message = "نام کاربری یا رمز عبور وارد نگردیده است.",
                    Data = null
                };

            var user = await _context.Users.FirstOrDefaultAsync(
                    u => u.Email.Equals(req.Email)
                );

            if (user is null)
                return new BaseResultDTO<SignInUserResultDto>
                {
                    IsSuccess = false,
                    Message = "ایمیل به درستی وارد نشده است.",
                    Data = null
                };

            var passHasher = new PasswordHasher();

            if (passHasher.VerifyPassword(user.Password, req.Password) is false)
                return new BaseResultDTO<SignInUserResultDto>
                {
                    IsSuccess = false,
                    Message = "رمز عبور اشتباه است.",
                    Data = null
                };

            return new BaseResultDTO<SignInUserResultDto>
            {
                IsSuccess = true,
                Message = user.Role == RoleEnum.Operator ? "اپراتور وارد شدید" :
                user.Role == RoleEnum.Nurse ? "پرستار وارد شدید" :
                "سوپر وایزر وارد شدید",
                Data = new SignInUserResultDto
                {
                    Id = user.Id,
                    Phone = user.PhoneNumber,
                    UserName = user.UserName,
                    Role = user.Role
                }
            };
        }
    }
}

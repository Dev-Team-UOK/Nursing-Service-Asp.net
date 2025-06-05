using Microsoft.EntityFrameworkCore;
using Nursing_Service.Application.Interfaces.Contexts;
using Nursing_Service.Common.Dto.Base;
using Nursing_Service.Common.Extensions;

namespace Nursing_Service.Application.Services.Users.Commands.Update
{
    public interface IUpdateUserService
    {
        Task<BaseResultDTO> ExcuteAsync(UpdateUserRequestInfo req);
    }

    public class UpdateUserService : IUpdateUserService
    {
        private IDataBaseContext _context;

        public UpdateUserService(IDataBaseContext context)
        {
            _context = context;
        }

        public async Task<BaseResultDTO> ExcuteAsync(UpdateUserRequestInfo req)
        {
            try
            {
                if (req.Id == 0)
                    throw new NotImplementedException("شناسه کاربر نمیتواند 0 باشد.");

                var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == req.Id);

                if (user is null)
                    throw new NotImplementedException("کاربر با شناسه مورد نظر یافت نشد.");

                var passHaser = new PasswordHasher();

                if (req.UserName is not null)
                    user.UserName = req.UserName;
                if (req.FirstName is not null)
                    user.FirstName = req.FirstName;
                if (req.LastName is not null)
                    user.LastName = req.LastName;
                if (req.Email is not null)
                    user.Email = req.Email;
                if (req.PhoneNumber is not null)
                    user.PhoneNumber = req.PhoneNumber;
                if (req.Password is not null)
                    user.Password = passHaser.HashPassword(req.Password);

                await _context.SaveChangesAsync();

                return new BaseResultDTO
                {
                    IsSuccess = true,
                    Message = "کاربر مورد نظر با موفقیت بروزرسانی شد."
                };
            }
            catch (Exception ex)
            {
                return new BaseResultDTO
                {
                    IsSuccess = false,
                    Message = ex.Message
                };
            }
        }
    }
}

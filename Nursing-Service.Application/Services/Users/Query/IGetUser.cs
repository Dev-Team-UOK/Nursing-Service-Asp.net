using Microsoft.EntityFrameworkCore;
using Nursing_Service.Application.Interfaces.Contexts;
using Nursing_Service.Common.Dto.Base;

namespace Nursing_Service.Application.Services.Users.Query
{
    public interface IGetUserService
    {
        Task<BaseResultDTO<List<GetUserResultDTO>>> ExcuteAsync(ulong? userId = null);
    }

    public class GetUserService : IGetUserService
    {
        private IDataBaseContext _context;

        public GetUserService(IDataBaseContext context)
        {
            _context = context;
        }

        public async Task<BaseResultDTO<List<GetUserResultDTO>>> ExcuteAsync(ulong? userId = null)
        {
            try
            {
                var users = new List<Domain.Entities.User.User>();

                if (userId is not null)
                    users = userId == 0 ?
                        throw new NotImplementedException("شناسه کاربر نمیتواند 0 باشد.")
                        : await _context.Users.Where(u => u.Id == userId && u.IsDeleted == false).ToListAsync();
                else
                    users = await _context.Users.Where(u => u.IsDeleted == false).ToListAsync();

                if (users.Any() is false && userId is not null)
                    throw new NotImplementedException("هیچ کاربری با شناسه مورد نظر یافت نشد.");

                return new BaseResultDTO<List<GetUserResultDTO>>
                {
                    IsSuccess = true,
                    Message = "لیست کاربران با موفقیت برگردانده شد.",
                    Data = users.Select(u => new GetUserResultDTO
                    {
                        Email = u.Email,
                        Password = u.Password,
                        PhoneNumber = u.PhoneNumber,
                        UserName = u.UserName,
                        FirstName = u.FirstName,
                        LastName = u.LastName,
                    }).ToList()
                };
            }
            catch (Exception ex)
            {
                return new BaseResultDTO<List<GetUserResultDTO>>
                {
                    IsSuccess = false,
                    Message = ex.Message,
                    Data = null
                };
            }
        }
    }
}

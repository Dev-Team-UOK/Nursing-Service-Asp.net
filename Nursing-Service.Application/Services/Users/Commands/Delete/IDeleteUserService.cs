using Microsoft.EntityFrameworkCore;
using Nursing_Service.Application.Interfaces.Contexts;
using Nursing_Service.Common.Dto.Base;

namespace Nursing_Service.Application.Services.Users.Commands.Delete
{
    public interface IDeleteUserService
    {
        Task<BaseResultDTO> ExcuteAsync(ulong userId);
    }

    public class DeleteUserService : IDeleteUserService
    {
        private IDataBaseContext _context;

        public DeleteUserService(IDataBaseContext context)
        {
            _context = context;
        }

        public async Task<BaseResultDTO> ExcuteAsync(ulong userId)
        {
            try
            {
                if (userId == 0) 
                    throw new NotImplementedException("شناسه کاربر نمیتواند 0 باشد.");

                var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);

                if (user is null)
                    throw new NotImplementedException("کاربری با شناسه موردنظر یافت نشد.");

                user!.IsDeleted = true;

                await _context.SaveChangesAsync();

                return new BaseResultDTO
                {
                    IsSuccess = true,
                    Message = "کاربر با موفقیت حذف گردید."
                };
            }
            catch (Exception ex)
            {
                return new BaseResultDTO
                {
                    IsSuccess = false,
                    Message = ex.Message,
                };
            }
        }
    }
}

using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Nursing_Service.Application.Interfaces.Contexts;
using Nursing_Service.Common.Dto.Base;

namespace Nursing_Service.Application.Services.Service.Command.Delete
{
    public interface IDeleteService
    {
        Task<BaseResultDTO> ExcuteAsync(ulong serviceId);
    }

    public class DeleteService : IDeleteService
    {
        private readonly IDataBaseContext _context;

        public DeleteService(IDataBaseContext context)
        {
            _context = context;
        }

        public async Task<BaseResultDTO> ExcuteAsync(ulong serviceId)
        {
            try
            {
                var service = await _context.Services.FirstOrDefaultAsync(s => s.Id == serviceId && !s.IsDeleted);
                if (service == null)
                {
                    return new BaseResultDTO
                    {
                        IsSuccess = false,
                        Message = "سرویس مورد نظر یافت نشد."
                    };
                }

                service.IsDeleted = true;
                await _context.SaveChangesAsync();

                return new BaseResultDTO
                {
                    IsSuccess = true,
                    Message = "سرویس با موفقیت حذف شد."
                };
            }
            catch (System.Exception ex)
            {
                return new BaseResultDTO
                {
                    IsSuccess = false,
                    Message = $"خطا در حذف سرویس: {ex.Message}"
                };
            }
        }
    }
}

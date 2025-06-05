using Microsoft.EntityFrameworkCore;
using Nursing_Service.Application.Interfaces.Contexts;
using Nursing_Service.Common.Dto.Base;

namespace Nursing_Service.Application.Services.Nurse.Command.Delete
{
    public interface IDeleteNurseService
    {
        Task<BaseResultDTO> ExcuteAsync(ulong nurseId);
    }

    public class DeleteNurseService : IDeleteNurseService
    {
        private IDataBaseContext _context;

        public DeleteNurseService(IDataBaseContext context)
        {
            _context = context;
        }

        public async Task<BaseResultDTO> ExcuteAsync(ulong nurseId)
        {
            try
            {
                if (nurseId == 0)
                    throw new NotImplementedException("شناسه پرستار نمیتواند 0 باشد.");

                var nurse = await _context.Nurses.FirstOrDefaultAsync(n => n.Id == nurseId);

                if (nurse is null)
                    throw new NotImplementedException("هیچ پرستاری با شناسه موردنظر یافت نشد.");
                    
                nurse!.IsDeleted = true;

                await _context.SaveChangesAsync();

                return new BaseResultDTO
                {
                    IsSuccess = true,
                    Message = "پرستار مورد نظر با موفقیت حذف گردید."
                };
            }
            catch (Exception ex)
            {
                return new BaseResultDTO()
                {
                    IsSuccess = false,
                    Message = ex.Message
                };
            }
        }
    }
}

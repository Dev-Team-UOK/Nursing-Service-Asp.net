using Microsoft.EntityFrameworkCore;
using Nursing_Service.Application.Interfaces.Contexts;
using Nursing_Service.Common.Dto.Base;

namespace Nursing_Service.Application.Services.SuperVisor.Command.Delete
{
    public interface IDeleteSuperVisor
    {
        Task<BaseResultDTO> ExcuteAsync(ulong superVisorId);
    }

    public class DeleteSuperVisor : IDeleteSuperVisor
    {
        private IDataBaseContext _context;

        public DeleteSuperVisor(IDataBaseContext context)
        {
            _context = context;
        }

        public async Task<BaseResultDTO> ExcuteAsync(ulong superVisorId)
        {
            try
            {
                if (superVisorId == 0)
                    throw new NotImplementedException("شناسه سوپروایزور نمیتواند 0 باشد.");

                var superVisor = await _context.SuperVisors.FirstOrDefaultAsync(n => n.Id == superVisorId);

                if (superVisor is null)
                    throw new NotImplementedException("هیچ سوپروایزوری با شناسه موردنظر یافت نشد.");

                superVisor!.IsDeleted = true;

                await _context.SaveChangesAsync();

                return new BaseResultDTO
                {
                    IsSuccess = true,
                    Message = "سوپروایزور مورد نظر با موفقیت حذف گردید."
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

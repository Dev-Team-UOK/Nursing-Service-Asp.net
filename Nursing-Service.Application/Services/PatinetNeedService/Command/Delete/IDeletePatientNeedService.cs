using Microsoft.EntityFrameworkCore;
using Nursing_Service.Application.Interfaces.Contexts;
using Nursing_Service.Common.Dto.Base;

namespace Nursing_Service.Application.Services.PatinetNeedService.Command.DeletePatientNeedService
{
    public interface IDeletePatientNeedService
    {
        Task<BaseResultDTO> ExcuteAsync(ulong patientNeedServiceId);
    }

    public class DeletePatientNeedService : IDeletePatientNeedService
    {
        private IDataBaseContext _context;

        public DeletePatientNeedService(IDataBaseContext context)
        {
            _context = context;
        }

        public async Task<BaseResultDTO> ExcuteAsync(ulong patientNeedServiceId)
        {
            try
            {
                if (patientNeedServiceId is 0)
                    throw new Exception("شناسه درخواست سرویس نمیتواند 0 باشد");

                var patientNeedService = await _context.PatientNeedService.FirstOrDefaultAsync( pns => pns.Id == patientNeedServiceId);

                if (patientNeedService is not null)
                    patientNeedService.IsDeleted = true;
                else
                    throw new Exception("درخواست سرویس با شناسه مورد نظر وجود ندارد.");

                await _context.SaveChangesAsync();

                return new BaseResultDTO
                {
                    IsSuccess = true,
                    Message = "درخواست سرویس با موفقیت حذف گردید."
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

using Microsoft.EntityFrameworkCore;
using Nursing_Service.Application.Interfaces.Contexts;
using Nursing_Service.Common.Dto.Base;

namespace Nursing_Service.Application.Services.PatinetNeedService.Command.UpdatePatientNeedService
{
    public interface IUpdatePatientNeedService
    {
        Task<BaseResultDTO> ExcuteAsync(UpdatePatientNeedServiceRequestInfo req);
    }

    public class UpdatePatientNeedService : IUpdatePatientNeedService
    {
        private IDataBaseContext _context;

        public UpdatePatientNeedService(IDataBaseContext context)
        {
            _context = context;
        }

        public async Task<BaseResultDTO> ExcuteAsync(UpdatePatientNeedServiceRequestInfo req)
        {
            try
            {
                if (req.Id is 0)
                    throw new Exception("شناسه درخواست سرویس نمیتواند 0 باشد.");

                var pns = await _context.PatientNeedService.FirstOrDefaultAsync(pns => pns.Id == req.Id);

                if (pns is null)
                    throw new Exception("هیچ درخواست سرویسی با شناسه مورد نظر یافت نشد");

                if (req.SuperVisorId is not null)
                    pns.SuperVisorId = req.SuperVisorId.Value;
                if (req.ServiceDateTime is not null)
                    pns.ServiceDateTime = req.ServiceDateTime.Value;
                if (req.ServiceId is not null)
                    pns.ServiceId = req.ServiceId.Value;

                pns.UpdatedDateTime = DateTime.Now;

                await _context.SaveChangesAsync();

                return new BaseResultDTO
                { 
                    IsSuccess = true,
                    Message = "درخواست سرویس با موفقیت بروزرسانی شد."
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

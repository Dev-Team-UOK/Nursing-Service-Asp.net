using Microsoft.EntityFrameworkCore;
using Nursing_Service.Application.Interfaces.Contexts;
using Nursing_Service.Common.Dto.Base;
using Nursing_Service.Domain.Entities.Patient;

namespace Nursing_Service.Application.Services.PatinetNeedService.Query.GetPatientNeedServices
{
    public interface IGetPatientNeedServices
    {
        Task<BaseResultDTO<List<GetPatientNeedServiceResultDTO>>> ExcuteAsync(ulong? patientId = null);
    }

    public class GetPatientNeedSevices : IGetPatientNeedServices
    {
        private IDataBaseContext _context;

        public GetPatientNeedSevices(IDataBaseContext context)
        {
            _context = context;
        }

        public async Task<BaseResultDTO<List<GetPatientNeedServiceResultDTO>>> ExcuteAsync(ulong? patientId = null)
        {
            try
            {
                if (patientId is not null && patientId == 0)
                    throw new ArgumentException("شناسه بیمار نمیتواند 0 باشد.");

                List<PatientNeedService> patientNeedServices;

                if (patientId is not null)
                    patientNeedServices = await _context.PatientNeedService.Where(pns => pns.PatientId == patientId && pns.IsDeleted == false).ToListAsync();
                else
                    patientNeedServices = await _context.PatientNeedService.Where(pns => pns.IsDeleted == false).ToListAsync();

                    return new BaseResultDTO<List<GetPatientNeedServiceResultDTO>>
                    {
                        IsSuccess = true,
                        Message = "لیست درخواست‌ سرویس‌هایِ بیمار مورد نظر با موفقیت برگشت داده شد.",
                        Data = patientNeedServices.Select(pns => new GetPatientNeedServiceResultDTO
                        {
                            Id = pns.Id,
                            IsDone = pns.IsDone,
                            IsPast = pns.IsPast,
                            NurseId = pns.NurseId,
                            PatientId = pns.Id,
                            ServiceDateTime = pns.ServiceDateTime,
                            ServiceId = pns.ServiceId,
                            SuperVisorId = pns.SuperVisorId
                        }).ToList()
                    };

            }
            catch (Exception ex)
            {
                return new BaseResultDTO<List<GetPatientNeedServiceResultDTO>>
                {
                    IsSuccess = false,
                    Message = ex.Message,
                    Data = null
                };
            }
        }
    }
}

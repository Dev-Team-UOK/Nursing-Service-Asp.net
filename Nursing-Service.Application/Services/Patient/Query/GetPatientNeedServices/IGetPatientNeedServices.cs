using Nursing_Service.Application.Interfaces.Contexts;
using Nursing_Service.Common.Dto.Base;

namespace Nursing_Service.Application.Services.Patient.Query.GetPatientNeedServices
{
    public interface IGetPatientNeedServices
    {
        Task<BaseResultDTO<List<GetPatientNeedServicesResultDTO>>> ExcuteAsync(ulong patientId);
    }

    public class GetPatientNeedSevices : IGetPatientNeedServices
    {
        private IDataBaseContext _context;

        public GetPatientNeedSevices(IDataBaseContext context)
        {
            _context = context;
        }

        public async Task<BaseResultDTO<List<GetPatientNeedServicesResultDTO>>> ExcuteAsync(ulong patientId)
        {
            try
            {
                if (patientId is 0)
                    throw new ArgumentException("شناسه بیمار نمیتواند خالی یا 0 باشد.");

                var patientNeedServices = _context.PatientNeedService.Where(pns => pns.PatientId == patientId).ToList();

                return new BaseResultDTO<List<GetPatientNeedServicesResultDTO>>
                {
                    IsSuccess = true,
                    Message = "لیست درخواست‌ سرویس‌هایِ بیمار مورد نظر با موفقیت برگشت داده شد.",
                    Data = patientNeedServices.Select(pns => new GetPatientNeedServicesResultDTO
                    {
                        IsDone = pns.IsDone,
                        IsPast = pns.IsPast,
                        NurseId = pns.NurseId,
                        PatientId = patientId,
                        ServiceDateTime = pns.ServiceDateTime,
                        ServiceId = pns.ServiceId,
                        SuperVisorId = pns.SuperVisorId
                    }).ToList()
                };

            }
            catch (Exception ex)
            {
                return new BaseResultDTO<List<GetPatientNeedServicesResultDTO>>
                {
                    IsSuccess = false,
                    Message = ex.Message,
                    Data = null
                };
            }
        }
    }
}

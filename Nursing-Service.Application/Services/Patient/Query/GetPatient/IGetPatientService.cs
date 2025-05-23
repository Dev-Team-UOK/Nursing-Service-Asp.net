using Nursing_Service.Application.Interfaces.Contexts;
using Nursing_Service.Application.Services.Patient.Query.GetPatientNeedServices;
using Nursing_Service.Common.Dto.Base;

namespace Nursing_Service.Application.Services.Patient.Query.GetPatient
{
    public interface IGetPatientService
    {
        Task<BaseResultDTO<GetPatientResultDTO>> ExcuteAsync(ulong patientId);
    }

    public class GetPatientService : IGetPatientService
    {
        private IDataBaseContext _context;
        private IGetPatientNeedServices _patientNeedServices;

        public GetPatientService(IDataBaseContext context, IGetPatientNeedServices patientNeedServices)
        {
            _context = context;
            _patientNeedServices = patientNeedServices;
        }

        public async Task<BaseResultDTO<GetPatientResultDTO>> ExcuteAsync(ulong patientId)
        {
            try
            {
                if (patientId == 0)
                    throw new Exception("شناسه بیمار معتبر نیست");

                var patient = _context.Patients.FirstOrDefault(p => p.Id == patientId);

                if (patient is null)
                    throw new Exception("هیچ بیماری با شناسه مورد نظر پیدا نشد.");

                var patientNeedServices = (await _patientNeedServices.ExcuteAsync(patientId))?.Data;

                return new BaseResultDTO<GetPatientResultDTO>
                {
                    IsSuccess = true,
                    Message = "اطلاعات بیمار با موفقیت برگشت داده شد",
                    Data = new GetPatientResultDTO
                    {
                        FullName = patient.FullName,
                        Address = patient.Address,
                        Age = patient.Age,
                        Height = patient.Height,
                        Gender = patient.Gender,
                        IllnessHistory = patient.IllnessHistory,
                        PhoneNumber = patient.PhoneNumber,
                        Weight = patient.Weight,
                        NeedServices = patientNeedServices
                    }
                };
            }
            catch (Exception ex)
            {
                return new BaseResultDTO<GetPatientResultDTO>
                {
                    IsSuccess = false,
                    Message = ex.Message,
                    Data = null
                };
            }
        }
    }
}

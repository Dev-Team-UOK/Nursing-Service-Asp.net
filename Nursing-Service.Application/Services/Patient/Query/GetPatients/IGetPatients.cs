using Nursing_Service.Application.Interfaces.Contexts;
using Nursing_Service.Application.Services.Patient.Query.GetPatientNeedServices;
using Nursing_Service.Common.Dto.Base;

namespace Nursing_Service.Application.Services.Patient.Query.GetPatients
{
    public interface IGetPatients
    {
        Task<BaseResultDTO<List<GetPatientsResultDTO>>> ExcuteAsync();
    }

    public class GetPatients : IGetPatients
    {
        private IDataBaseContext _context;
        private IGetPatientNeedServices _getPatientServices;

        public GetPatients(IDataBaseContext context, IGetPatientNeedServices getPatientServices)
        {
            _context = context;
            _getPatientServices = getPatientServices;
        }

        public async Task<BaseResultDTO<List<GetPatientsResultDTO>>> ExcuteAsync()
        {
            try
            {
                var patients = _context.Patients.Where(p => p.IsDeleted == false).ToList();
                var result = new List<GetPatientsResultDTO>();

                foreach (var p in patients)
                {
                    result.Add(new GetPatientsResultDTO
                    {
                        FullName = p.FullName,
                        Address = p.Address,
                        Age = p.Age,
                        Gender = p.Gender,
                        Height = p.Height,
                        IllnessHistory = p.IllnessHistory,
                        PhoneNumber = p.PhoneNumber,
                        Services = (await _getPatientServices.ExcuteAsync(p.Id)).Data
                    });
                }

                return new BaseResultDTO<List<GetPatientsResultDTO>>
                {
                    IsSuccess = true,
                    Message = "لیست بیماران با موفقیت برگشت داده شد",
                    Data = result
                };
            }
            catch (Exception ex)
            {
                return new BaseResultDTO<List<GetPatientsResultDTO>>
                {
                    IsSuccess = false,
                    Message = ex.Message,
                    Data = null
                };
            }
        }
    }
}

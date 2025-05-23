using Nursing_Service.Application.Interfaces.Contexts;
using Nursing_Service.Application.Services.Users.Commands.Create;
using Nursing_Service.Common.Dto.Base;

namespace Nursing_Service.Application.Services.Patient.Command.Create
{
    public interface ICreatePatientService
    {
        Task<BaseResultDTO<CreatePatientResultDTO>> ExcuteAsync(CreatePatinetRequestInfo req);
    }

    public class CreatePatientService : ICreatePatientService
    {
        private IDataBaseContext _context;

        public CreatePatientService(IDataBaseContext context)
        {
            _context = context;
        }

        public async Task<BaseResultDTO<CreatePatientResultDTO>> ExcuteAsync(CreatePatinetRequestInfo req)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(req.FullName))
                    throw new Exception("FullName cant be null.");
                if (string.IsNullOrWhiteSpace(req.Address))
                    throw new Exception("Address cant be null.");
                if (string.IsNullOrWhiteSpace(req.PhoneNumber))
                    throw new Exception("Phone number cant be null.");
                if (req.Age is 0)
                    throw new Exception("Age cant be 0.");

                var patient = new Domain.Entities.Patient.Patient
                {
                    FullName = req.FullName,
                    Address = req.Address,
                    Age = req.Age,
                    Gender = req.Gender,
                    Height = req.Height,
                    Weight = req.Weight,
                    PhoneNumber = req.PhoneNumber,
                    IllnessHistory = req.IllnessHistory
                };

                await _context.Patients.AddAsync(patient);

                await _context.SaveChangesAsync();

                return new BaseResultDTO<CreatePatientResultDTO>()
                {
                    IsSuccess = true,
                    Message = "بیمار با موفقیت ایجاد شد.",
                    Data = new CreatePatientResultDTO
                    {
                        Id = patient.Id,
                    }
                };
            }
            catch (Exception ex)
            {
                return new BaseResultDTO<CreatePatientResultDTO>()
                {
                    IsSuccess = false,
                    Message = ex.Message,
                    Data = new CreatePatientResultDTO
                    {
                        Id = 0
                    }
                };
            }
        }
    }
}

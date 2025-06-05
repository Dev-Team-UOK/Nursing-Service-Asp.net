using Microsoft.EntityFrameworkCore;
using Nursing_Service.Application.Interfaces.Contexts;
using Nursing_Service.Common.Dto.Base;

namespace Nursing_Service.Application.Services.Patient.Command.Update
{
    public interface IUpdatePatientService
    {
        Task<BaseResultDTO> ExcuteAsync(UpdatePatientRequestInfo req);
    }

    public class UpdatePatientService : IUpdatePatientService
    {
        private IDataBaseContext _context;

        public UpdatePatientService(IDataBaseContext context)
        {
            _context = context;
        }

        public async Task<BaseResultDTO> ExcuteAsync(UpdatePatientRequestInfo req)
        {
            try
            {
                if (req.Id is 0)
                    throw new Exception("شناسه بیمار نمیتواند 0 باشد.");

                var patient = await _context.Patients.FirstOrDefaultAsync(p => p.Id == req.Id);

                if (patient is null)
                    throw new Exception("هیج بیماری با شناسه مورد نظر یافت نشد.");

                if (req.IllnessHistory is not null)
                    patient.IllnessHistory = req.IllnessHistory;
                if (req.Height is not null)
                    patient.Height = req.Height;
                if (req.Weight is not null)
                    patient.Weight = req.Weight;
                if (req.Address is not null)
                    patient.Address = req.Address;
                if (req.Age is not null)
                    patient.Age = req.Age.Value;
                if (req.FullName is not null)
                    patient.FullName = req.FullName;

                patient.UpdatedDateTime = DateTime.Now;

                await _context.SaveChangesAsync();

                return new BaseResultDTO
                {
                    IsSuccess = true,
                    Message = "کاربر با موفقیت بروزرسانی شد"
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

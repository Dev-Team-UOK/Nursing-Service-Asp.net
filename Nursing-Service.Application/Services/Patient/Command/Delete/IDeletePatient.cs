using Nursing_Service.Application.Interfaces.Contexts;
using Nursing_Service.Common.Dto.Base;

namespace Nursing_Service.Application.Services.Patient.Command.Delete
{
    public interface IDeletePatient
    {
        Task<BaseResultDTO> ExcuteAsync(ulong id);
    }

    public class DeletePatient : IDeletePatient
    {
        private readonly IDataBaseContext _context;
        public DeletePatient(IDataBaseContext context)
        {
            _context = context;
        }
        public async Task<BaseResultDTO> ExcuteAsync(ulong id)
        {
            try
            {
                var patient = await _context.Patients.FindAsync(id);
                if (patient is null)
                {
                    return new BaseResultDTO
                    {
                        IsSuccess = false,
                        Message = "Patient not found."
                    };
                }
                
                patient.IsDeleted = true; 

                await _context.SaveChangesAsync();
                
                return new BaseResultDTO
                {
                    IsSuccess = true,
                    Message = "Patient deleted successfully."
                };
            }
            catch (Exception ex)
            {
                return new BaseResultDTO
                {
                    IsSuccess = false,
                    Message = $"بیمار حذف نشد. خطا: {ex.Message}"
                };
            }
        }
    }
}

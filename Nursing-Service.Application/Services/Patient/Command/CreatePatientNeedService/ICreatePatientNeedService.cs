using Nursing_Service.Application.Interfaces.Contexts;
using Nursing_Service.Common.Dto.Base;
using Nursing_Service.Domain.Entities.Patient;

namespace Nursing_Service.Application.Services.Patient.Command.CreatePatientNeedService
{
    public interface ICreatePatientNeedService
    {
        Task<BaseResultDTO<CreatePatientNeedSeviceResultDTO>> ExcuteAsync(CreatePatientRequesInfo req);
    }

    public class CreatePatientNeedService : ICreatePatientNeedService
    {
        private IDataBaseContext _context;

        public CreatePatientNeedService(IDataBaseContext context)
        {
            _context = context;
        }

        public async Task<BaseResultDTO<CreatePatientNeedSeviceResultDTO>> ExcuteAsync(CreatePatientRequesInfo req)
        {
            try
            {
                if (req.PatientId is 0)
                    throw new Exception("شناسه بیمار نمیتواند خالی باشد");
                if (req.ServiceId is 0)
                    throw new Exception("شناسه سرویس نمیتواند خالی باشد");
                if (req.SuperVisorId is 0)
                    throw new Exception("شناسه سرپرستار نمیتواند خالی باشد");

                var needService = new PatientNeedService
                {
                    ServiceDateTime = req.ServiceDateTime,
                    ServiceId = req.ServiceId,
                    SuperVisorId = req.SuperVisorId,
                    PatientId = req.PatientId
                };

                await _context.PatientNeedService.AddAsync(needService);

                await _context.SaveChangesAsync();

                return new BaseResultDTO<CreatePatientNeedSeviceResultDTO>
                {
                    IsSuccess = true,
                    Message = "درخواست سرویس با موفقیت ایجاد گردید.",
                    Data = new CreatePatientNeedSeviceResultDTO
                    {
                        Id = needService.Id
                    }
                };
            }
            catch (Exception ex)
            {
                return new BaseResultDTO<CreatePatientNeedSeviceResultDTO>
                {
                    IsSuccess = false,
                    Message = ex.Message,
                    Data = null
                }
            }
        }
    }
}

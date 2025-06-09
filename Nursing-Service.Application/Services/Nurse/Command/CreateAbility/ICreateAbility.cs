using Microsoft.EntityFrameworkCore;
using Nursing_Service.Application.Interfaces.Contexts;
using Nursing_Service.Common.Dto.Base;
using Nursing_Service.Domain.Entities.Nurse;

namespace Nursing_Service.Application.Services.Nurse.Command.CreateAbility
{
    public interface ICreateAbility
    {
        Task<BaseResultDTO> ExcuteAsync(ulong nurseId, ulong serviceId);
    }

    public class CreateAbility : ICreateAbility
    {
        private IDataBaseContext _context;

        public CreateAbility(IDataBaseContext context)
        {
            _context = context;
        }

        public async Task<BaseResultDTO> ExcuteAsync(ulong nurseId, ulong serviceId)
        {
            try
            {
                if (nurseId == 0)
                    throw new ArgumentException("شناسه پرستار نمیتواند 0 باشد.");
                if (serviceId == 0)
                    throw new ArgumentException("شناسه سرویس نمیتواند 0 باشد.");

                // Check if the nurse already has this service
                var existingAbility = await _context.NurseCanDoService
                    .FirstOrDefaultAsync(ncs => ncs.NurseId == nurseId && ncs.ServiceId == serviceId);
                if (existingAbility != null)
                {
                    return new BaseResultDTO
                    {
                        IsSuccess = false,
                        Message = "این پرستار از قبل این توانایی را دارد."
                    };
                }

                var nurseCanDuService = new NurseCanDoService
                {
                    NurseId = nurseId,
                    ServiceId = serviceId
                };

                await _context.NurseCanDoService.AddAsync(nurseCanDuService);
                await _context.SaveChangesAsync();

                return new BaseResultDTO
                {
                    IsSuccess = true,
                    Message = "توانایی با موفقیت ایجاد شد."
                };
            }
            catch (Exception ex)
            {
                return new BaseResultDTO
                {
                    IsSuccess = false,
                    Message = $"خطا در ایجاد توانایی: {ex.Message}"
                };
            }
        }
    }
}

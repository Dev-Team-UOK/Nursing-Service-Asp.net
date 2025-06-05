using Microsoft.EntityFrameworkCore;
using Nursing_Service.Application.Interfaces.Contexts;
using Nursing_Service.Common.Dto.Base;
using Nursing_Service.Common.Extensions;
using Nursing_Service.Domain.Entities.Nurse;
using Nursing_Service.Domain.Entities.User;

namespace Nursing_Service.Application.Services.Nurse.Command.Update
{
    public interface IUpdateNurseService
    {
        Task<BaseResultDTO> ExcuteAsync(UpdateNurseRequestInfo req);
    }

    public class UpdateNurseService
    {
        private IDataBaseContext _context;

        public UpdateNurseService(IDataBaseContext context)
        {
            _context = context;
        }

        public async Task<BaseResultDTO> ExcuteAsync(UpdateNurseRequestInfo req)
        {
            try
            {
                if (req.Id == 0)
                    throw new NotImplementedException("آیدی پرستار نمیتواند 0 باشد.");
                if (req.DoService.Any() is false)
                    throw new NotImplementedException("پرستار میبایست حتما سرویسی ارائه دهد.");

                var nurse = await _context.Nurses.FirstOrDefaultAsync(n => n.Id == req.Id);

                if (nurse is null)
                    throw new NotImplementedException("هیچ پرستاری با شناسه مورد نظر یافت نشد.");

                var passHaser = new PasswordHasher();

                if (req.SuperVisorId is not null)
                    nurse.SuperVisorId = req.SuperVisorId;
                if (req.WorkHistoryInYear is not null)
                    nurse.WorkHistoryInYear = req.WorkHistoryInYear;
                if (req.UserName is not null)
                    nurse.UserName = req.UserName;
                if (req.FirstName is not null)
                    nurse.FirstName = req.FirstName;
                if (req.LastName is not null)
                    nurse.LastName = req.LastName;
                if (req.Email is not null)
                    nurse.Email = req.Email;
                if (req.PhoneNumber is not null)
                    nurse.PhoneNumber = req.PhoneNumber;
                if (req.Password is not null)
                    nurse.Password = passHaser.HashPassword(req.Password);

                nurse.UpdatedDateTime = DateTime.Now;

                var currentNurseCanDoServices = _context.NurseCanDoService.Where(ncds => ncds.NurseId == req.Id).ToList();

                foreach (var currentNurseCanDoService in currentNurseCanDoServices)
                {
                    currentNurseCanDoService.IsDeleted = true;

                    _context.NurseCanDoService.Update(currentNurseCanDoService);
                }

                foreach (var serviceId in req.DoService)
                {
                    if ((await _context.NurseCanDoService
                       .FirstOrDefaultAsync(ncds => ncds.NurseId == req.Id && ncds.ServiceId == serviceId))
                       is not null)
                    {
                        var ncds = await _context.NurseCanDoService.FirstOrDefaultAsync(ncds => ncds.NurseId == req.Id && ncds.ServiceId == serviceId);

                        ncds!.IsDeleted = false;
                    }
                    else
                    {
                        var ncds = new NurseCanDoService
                        {
                            NurseId = req.Id,
                            ServiceId = serviceId,
                        };

                        await _context.NurseCanDoService.AddAsync(ncds);
                    }
                }

                return new BaseResultDTO
                {
                    IsSuccess = true,
                    Message = "پرستار با موفقیت بروزرسانی شد."
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

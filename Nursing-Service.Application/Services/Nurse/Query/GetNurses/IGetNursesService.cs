using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Nursing_Service.Application.Interfaces.Contexts;
using Nursing_Service.Application.Services.PatinetNeedService.Query.GetPatientNeedServices;
using Nursing_Service.Application.Services.Service.Query.GetServices;
using Nursing_Service.Common.Dto.Base;
using Nursing_Service.Domain.Entities.Patient;

namespace Nursing_Service.Application.Services.Nurse.Query.GetNurses
{
    public interface IGetNursesService
    {
        Task<BaseResultDTO<List<GetNurseResultDTO>>> ExcuteAsync(ulong? id = null, ulong? superVisorId = null);
    }

    public class GetNursesService : IGetNursesService
    {
        private IDataBaseContext _context;
        private IGetServices _getService;

        public GetNursesService(IDataBaseContext context, IGetServices getService)
        {
            _context = context;
            _getService = getService;
        }

        public async Task<BaseResultDTO<List<GetNurseResultDTO>>> ExcuteAsync(ulong? nurseId = null, ulong? superVisorId = null)
        {
            try
            {
                if (nurseId is not null && nurseId == 0)
                    throw new NotImplementedException("شناسه پرستار نمیتواند 0 باشد.");
                if (superVisorId == 0)
                    throw new NotImplementedException("شناسه سوپروایزور نمیتواند 0 باشد.");
                List<Domain.Entities.Nurse.Nurse> nurses;

                if (nurseId is not null)
                    nurses = await _context.Nurses.Where(n => n.Id == nurseId && n.IsDeleted == false).ToListAsync();
                if (superVisorId is not null)
                    nurses = await _context.Nurses.Where(n => n.SuperVisorId == superVisorId && n.IsDeleted == false).ToListAsync();
                else
                    nurses = await _context.Nurses.Where(n => n.IsDeleted == false).ToListAsync();

                if (nurses is null && nurseId is not null)
                    throw new NotImplementedException("هیچ پرستاری با شناسه مورد نظر پیدا نشد.");

                var result = new List<GetNurseResultDTO>();

                if (nurses is null)
                    return new BaseResultDTO<List<GetNurseResultDTO>>
                    {
                        IsSuccess = true,
                        Message = "لیست پرستاران با موفقیت برگشت داده شد.",
                        Data = null
                    };

                foreach (var n in nurses)
                {
                    result.Add(new GetNurseResultDTO
                    {
                        Email = n.Email,
                        FirstName = n.FirstName,
                        LastName = n.LastName,
                        Password = n.Password,
                        PhoneNumber = n.PhoneNumber,
                        UserName = n.UserName,
                        Id = n.Id,
                        NurseNumber = n.NurseNumber,
                        StartWorkingInCompany = n.StartWorkingInCompany,
                        SuperVisorId = n.SuperVisorId,
                        WorkHistoryInYear = n.WorkHistoryInYear,
                        DoService = (await _getService.ExcuteAsync(nurseId: nurseId, null, null)).Data!
                    });
                }

                return new BaseResultDTO<List<GetNurseResultDTO>>
                {
                    IsSuccess = true,
                    Message = "لیست پرستاران با موفقیت برگشت داده شد.",
                    Data = result
                };
            }
            catch (Exception ex)
            {
                return new BaseResultDTO<List<GetNurseResultDTO>>
                {
                    IsSuccess = false,
                    Message = ex.Message,
                    Data = null
                };
            }
        }
    }
}

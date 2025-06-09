using Microsoft.EntityFrameworkCore;
using Nursing_Service.Application.Interfaces.Contexts;
using Nursing_Service.Application.Services.Nurse.Query.GetNurses;
using Nursing_Service.Common.Dto.Base;
using Nursing_Service.Domain.Entities.SuperVisor;

namespace Nursing_Service.Application.Services.SuperVisor.Query.GetSuperVisors
{
    public interface IGetSuperVisors
    {
        Task<BaseResultDTO<List<GetSuperVisorResultDTO>>> ExcuteAsync(ulong? superVisorId = null);
    }

    public class GetSuperVisors : IGetSuperVisors
    {
        private IDataBaseContext _context;
        private IGetNursesService _nurseService;

        public GetSuperVisors(IGetNursesService nurseService, IDataBaseContext context)
        {
            _nurseService = nurseService;
            _context = context;
        }

        public async Task<BaseResultDTO<List<GetSuperVisorResultDTO>>> ExcuteAsync(ulong? superVisorId = null)
        {
            try
            {
                var superVisors = new List<Domain.Entities.SuperVisor.SuperVisor>();

                if (superVisorId is not null)
                    superVisors = superVisorId == 0 ?
                        throw new NotImplementedException("شناسه سوپروایزر نمیتواند 0 باشد.")
                        : await _context.SuperVisors.Where(sv => sv.Id == superVisorId && sv.IsDeleted == false).ToListAsync();
                else 
                    superVisors = await _context.SuperVisors.Where(u => u.IsDeleted == false).ToListAsync();

                if (superVisors.Any() is false && superVisorId is not null)
                    throw new NotImplementedException("هیچ سوپروایزوری با شناسه موردنظر یافت نشد.");

                var data = new List<GetSuperVisorResultDTO>();

                foreach (var item in superVisors)
                {
                    var nurses = (await _nurseService.ExcuteAsync(superVisorId:item.Id)).Data;

                    data.Add(new GetSuperVisorResultDTO
                    {
                        Id = item.Id,
                        Email = item.Email,
                        UserName = item.UserName,
                        Password = item.Password,
                        PhoneNumber = item.PhoneNumber,
                        FirstName = item.FirstName,
                        LastName = item.LastName,
                        Shift = item.Shift,
                        Nurses = nurses
                    });
                }

                return new BaseResultDTO<List<GetSuperVisorResultDTO>>
                {
                    IsSuccess = true,
                    Message = "لیست کاربران با موفقیت برگشت داده شد.",
                    Data = data
                };
            }
            catch (Exception ex)
            {
                return new BaseResultDTO<List<GetSuperVisorResultDTO>>
                {
                    IsSuccess = false,
                    Message = ex.Message,
                    Data = null
                };
            }
        }
    }
}

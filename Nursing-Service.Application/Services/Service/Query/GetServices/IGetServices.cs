using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Nursing_Service.Application.Interfaces.Contexts;
using Nursing_Service.Common.Dto.Base;
using Nursing_Service.Domain.Entities.Service;

namespace Nursing_Service.Application.Services.Service.Query.GetServices
{
    public interface IGetServices
    {
        Task<BaseResultDTO<List<GetServiceResultDTO>>> ExcuteAsync(
            ulong? nurseId = null, 
            ulong? patientId = null, 
            ulong? patientNeedServiceId = null
        );
    }

    public class GetService : IGetServices
    {
        private IDataBaseContext _context;

        public GetService(IDataBaseContext context)
        {
            _context = context;
        }

        public async Task<BaseResultDTO<List<GetServiceResultDTO>>> ExcuteAsync(
            ulong? nurseId = null, 
            ulong? patientId = null, 
            ulong? patientNeedServiceId = null)
        {
            try
            {
                var services = new List<Domain.Entities.Service.Service>();

                if (nurseId is not null)
                    services = nurseId == 0 ? 
                        throw new NotImplementedException("شناسه پرستار نمیتواند 0 باشد.") 
                        : await _context.Services.Where(s => s.NurseDoService.Any(n => n.NurseId == nurseId) && s.IsDeleted == false).ToListAsync();
                if (patientId is not null)
                    services = patientId == 0 ?
                        throw new NotImplementedException("شناسه بیمار نمیتواند 0 باشد.")
                        : await _context.Services.Where(s => s.PatientNeedServices.Any(pns => pns.PatientId == patientId) && s.IsDeleted == false).ToListAsync();
                if (patientNeedServiceId is not null)
                    services = patientNeedServiceId == 0 ?
                        throw new NotImplementedException("شناسه درخواست سرویس نمیتواند 0 باشد.") 
                        : await _context.Services.Where(s => s.PatientNeedServices.Any(pns => pns.Id == patientNeedServiceId) && s.IsDeleted == false).ToListAsync();

                return new BaseResultDTO<List<GetServiceResultDTO>>
                {
                    IsSuccess = true,
                    Message = "لیست خدمات به موفقیت برگشت داده شدند.",
                    Data = services.Select(s => new GetServiceResultDTO
                    {
                        Id = s.Id,
                        Name = s.Name,
                        Cost = s.Cost,
                        MinDuration = s.MinDuration
                    }).ToList()
                };
            }
            catch (Exception ex)
            {
                return new BaseResultDTO<List<GetServiceResultDTO>>
                {
                    IsSuccess = false,
                    Message = ex.Message,
                    Data = null
                };
            }
        }
    }
}

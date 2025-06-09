using Nursing_Service.Application.Interfaces.Contexts;
using Nursing_Service.Common.Dto.Base;
using System.Threading.Tasks;

namespace Nursing_Service.Application.Services.SuperVisor.Command.AssignNurseToPNS
{
    public interface IAssignNurseToPNS
    {
        Task<BaseResultDTO> ExcuteAsync(ulong patientNeedServiceId, ulong nurseId);
    }

    public class AssignNurseToPNS : IAssignNurseToPNS
    {
        private readonly IDataBaseContext _context;

        public AssignNurseToPNS(IDataBaseContext context)
        {
            _context = context;
        }

        public async Task<BaseResultDTO> ExcuteAsync(ulong patientNeedServiceId, ulong nurseId)
        {
            var pns = await _context.PatientNeedService.FindAsync(patientNeedServiceId);
            if (pns == null)
            {
                return new BaseResultDTO
                {
                    IsSuccess = false,
                    Message = "درخواست سرویس مورد نظر یافت نشد."
                };
            }

            var nurse = await _context.Nurses.FindAsync(nurseId);
            if (nurse == null)
            {
                return new BaseResultDTO
                {
                    IsSuccess = false,
                    Message = "پرستار مورد نظر یافت نشد."
                };
            }

            pns.NurseId = nurseId;
            pns.AssignNurse = nurse;

            await _context.SaveChangesAsync();

            return new BaseResultDTO
            {
                IsSuccess = true,
                Message = "پرستار با موفقیت به درخواست سرویس اختصاص یافت."
            };
        }
    }
}
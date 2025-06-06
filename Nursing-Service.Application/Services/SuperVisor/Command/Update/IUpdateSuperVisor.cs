using Microsoft.EntityFrameworkCore;
using Nursing_Service.Application.Interfaces.Contexts;
using Nursing_Service.Common.Dto.Base;
using Nursing_Service.Common.Extensions;

namespace Nursing_Service.Application.Services.SuperVisor.Command.Update
{
    public interface IUpdateSuperVisor
    {
        Task<BaseResultDTO> ExcuteAsync(UpdateSuperVisorRequestInfo req);
    }

    public class UpdateSuperVisor : IUpdateSuperVisor
    {
        private IDataBaseContext _context;

        public UpdateSuperVisor(IDataBaseContext context)
        {
            _context = context;
        }

        public async Task<BaseResultDTO> ExcuteAsync(UpdateSuperVisorRequestInfo req)
        {
            try
            {
                var superVisor = await _context.SuperVisors.FirstOrDefaultAsync(n => n.Id == req.Id);

                if (superVisor is null)
                    throw new NotImplementedException("هیچ سوپروایزوری با شناسه مورد نظر یافت نشد.");

                var passHaser = new PasswordHasher();

                if (req.Shift is not null)
                    superVisor.Shift = req.Shift.Value;
                if (req.UserName is not null)
                    superVisor.UserName = req.UserName;
                if (req.FirstName is not null)
                    superVisor.FirstName = req.FirstName;
                if (req.LastName is not null)
                    superVisor.LastName = req.LastName;
                if (req.Email is not null)
                    superVisor.Email = req.Email;
                if (req.PhoneNumber is not null)
                    superVisor.PhoneNumber = req.PhoneNumber;
                if (req.Password is not null)
                    superVisor.Password = passHaser.HashPassword(req.Password);

                superVisor.UpdatedDateTime = DateTime.Now;

                await _context.SaveChangesAsync();

                return new BaseResultDTO
                {
                    IsSuccess = true,
                    Message = "سوپروایزور با موفقیت بروزرسانی شد."
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

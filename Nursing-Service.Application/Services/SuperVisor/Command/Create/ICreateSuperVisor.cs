using Nursing_Service.Application.Interfaces.Contexts;
using Nursing_Service.Application.Services.Users.Commands.Create;
using Nursing_Service.Common.Dto.Base;
using Nursing_Service.Common.Extensions;
using Nursing_Service.Domain.Entities.SuperVisor;
using Nursing_Service.Domain.Entities.User;

namespace Nursing_Service.Application.Services.SuperVisor.Command.Create
{
    public interface ICreateSuperVisor
    {
        Task<BaseResultDTO<CreateSuperVisorResultDto>> ExcuteAsync(CreateSuperVisorRequestInfo req);
    }

    public class CreateSuperVisor : ICreateSuperVisor
    {
        private IDataBaseContext _context;

        public CreateSuperVisor(IDataBaseContext context)
        {
            _context = context;
        }

        public async Task<BaseResultDTO<CreateSuperVisorResultDto>> ExcuteAsync(CreateSuperVisorRequestInfo req)
        {
            try
            {
                if (String.IsNullOrWhiteSpace(req.UserName))
                    throw new Exception("UserName cant be null.");
                if (String.IsNullOrWhiteSpace(req.Password))
                    throw new Exception("Password cant be null.");
                if (String.IsNullOrWhiteSpace(req.PhoneNumber))
                    throw new Exception("Phone number cant be null.");
                if (String.IsNullOrWhiteSpace(req.Email))
                    throw new Exception("Email cant be null.");

                var passHasher = new PasswordHasher();

                var superVisor = new Domain.Entities.SuperVisor.SuperVisor
                {
                    Email = req.Email,
                    Password = passHasher.HashPassword(req.Password),
                    PhoneNumber = req.PhoneNumber,
                    Role = RoleEnum.SuperVisor,
                    UserName = req.UserName,
                    FirstName = req.FirstName,
                    LastName = req.LastName,
                    Shift = req.Shift
                };

                await _context.SuperVisors.AddAsync(superVisor);

                await _context.SaveChangesAsync();

                return new BaseResultDTO<CreateSuperVisorResultDto>()
                {
                    IsSuccess = true,
                    Message = "سوپر وایزور با موفقیت ایجاد شد.",
                    Data = new CreateSuperVisorResultDto
                    {
                        // The user ID will be retrieved after creation.
                        Id = superVisor.Id
                    }
                };
            }
            catch (Exception ex)
            {
                return new BaseResultDTO<CreateSuperVisorResultDto>
                {
                    IsSuccess = false,
                    Message = ex.Message,
                    Data = null
                };
            }
        }
    }
}

using Nursing_Service.Application.Interfaces.Contexts;
using Nursing_Service.Common.Dto.Base;
using Nursing_Service.Common.Extensions;
using Nursing_Service.Domain.Entities.User;

namespace Nursing_Service.Application.Services.Users.Commands.Create
{
    public class CreateUserService : ICreateUserService
    {
        private IDataBaseContext _context;

        public CreateUserService(IDataBaseContext context)
        {
            _context = context;
        }

        public async Task<BaseResultDTO<CreateUserResultDto>> ExcuteAsync(CreateUserRequestInfo req)
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

                var user = new User
                (
                    userName: req.UserName,
                    firstName: req.FirstName,
                    lastName: req.LastName,
                    phoneNumber: req.PhoneNumber,
                    email: req.Email,
                    password: passHasher.HashPassword(req.Password)
                );
                

                await _context.Users.AddAsync(user);

                await _context.SaveChangesAsync();

                return new BaseResultDTO<CreateUserResultDto>()
                {
                    IsSuccess = true,
                    Message = "کاربر با موفقیت ایجاد شد.",
                    Data = new CreateUserResultDto
                    {
                        // The user ID will be retrieved after creation.
                        UserId = user.Id
                    }
                };
            }
            catch (Exception ex)
            {
                return new BaseResultDTO<CreateUserResultDto>()
                {
                    IsSuccess = false,
                    Message = ex.Message,
                    Data = new CreateUserResultDto
                    {
                        UserId = 0
                    }
                };
            }
        }
    }
}

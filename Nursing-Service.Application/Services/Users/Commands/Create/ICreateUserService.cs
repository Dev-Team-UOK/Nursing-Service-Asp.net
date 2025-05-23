using Nursing_Service.Common.Dto.Base;

namespace Nursing_Service.Application.Services.Users.Commands.Create
{
    public interface ICreateUserService
    {
        Task<BaseResultDTO<CreateUserResultDto>> ExcuteAsync(CreateUserRequestInfo req);
    }
}

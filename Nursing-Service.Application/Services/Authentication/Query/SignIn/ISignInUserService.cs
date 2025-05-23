using Nursing_Service.Common.Dto.Base;

namespace Nursing_Service.Application.Services.Users.Queries.SignIn
{
    public interface ISignInUserService
    {
        Task<BaseResultDTO<SignInUserResultDto>> ExcuteAsync(SignInUserRequestInfo req);
    }
}

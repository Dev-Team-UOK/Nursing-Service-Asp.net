using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nursing_Service.Application.Services.Authentication.Command.SignUp;
using Nursing_Service.Common.Dto.Base;

namespace Nursing_Service.Application.Services.Users.Queries.SignIn
{
    public interface ISignInUserService
    {
        Task<BaseResultDto<SignInUserResultDto>> ExcuteAsync(SignInUserRequestInfo req);
    }
}

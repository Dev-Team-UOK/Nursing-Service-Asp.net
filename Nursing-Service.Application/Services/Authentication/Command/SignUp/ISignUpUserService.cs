using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nursing_Service.Common.Dto.Base;

namespace Nursing_Service.Application.Services.Authentication.Command.SignUp
{
    public interface ISignUpUserService
    {
        Task<BaseResultDto<SignUpUserResultDto>> ExcuteAsync(SignUpUserRequestInfo req);
    }
}

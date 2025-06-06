using Nursing_Service.Application.Services.Users.Commands.Create;
using Nursing_Service.Domain.Entities.SuperVisor;

namespace Nursing_Service.Application.Services.SuperVisor.Command.Create
{
    public class CreateSuperVisorRequestInfo : CreateUserRequestInfo
    {
        public Shift Shift { get; set; }
    }
}

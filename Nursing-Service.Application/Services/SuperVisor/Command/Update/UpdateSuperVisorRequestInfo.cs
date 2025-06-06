using Nursing_Service.Application.Services.Users.Commands.Update;
using Nursing_Service.Domain.Entities.SuperVisor;

namespace Nursing_Service.Application.Services.SuperVisor.Command.Update
{
    public class UpdateSuperVisorRequestInfo : UpdateUserRequestInfo
    {
        public Shift? Shift { get; set; }
    }
}

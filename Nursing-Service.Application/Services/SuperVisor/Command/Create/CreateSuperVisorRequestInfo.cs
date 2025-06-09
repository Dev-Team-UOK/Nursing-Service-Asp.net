using Nursing_Service.Application.Services.Users.Commands.Create;
using Nursing_Service.Domain.Entities.SuperVisor;
using System.ComponentModel;

namespace Nursing_Service.Application.Services.SuperVisor.Command.Create
{
    public class CreateSuperVisorRequestInfo : CreateUserRequestInfo
    {
        [DisplayName("شیفت")]
        public Shift Shift { get; set; }
    }
}

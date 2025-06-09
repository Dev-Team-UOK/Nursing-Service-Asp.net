using Nursing_Service.Application.Services.Users.Commands.Update;
using Nursing_Service.Domain.Entities.SuperVisor;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Nursing_Service.Application.Services.SuperVisor.Command.Update
{
    public class UpdateSuperVisorRequestInfo : UpdateUserRequestInfo
    {
        [DisplayName("شیفت")]
        public Shift? Shift { get; set; }
    }
}

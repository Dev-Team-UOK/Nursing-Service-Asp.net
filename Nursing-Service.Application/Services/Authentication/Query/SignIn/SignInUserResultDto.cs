using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nursing_Service.Application.Services.Users.Queries.SignIn
{
    public class SignInUserResultDto
    {
        public ulong Id { get; set; }
        public string UserName { get; set; }
        public string Phone { get; set; }
    }
}

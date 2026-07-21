using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nursing_Service.Infrastructure.SMS.Ir
{
    public class VerifySendModel
    {
        public required string Mobile { get; set; }

        public required int TemplateId { get; set; }

        public required VerifySendParameterModel[] Parameters { get; set; }
    }
    public class VerifySendParameterModel
    {
        public required string Name { get; set; }
        public required string Value { get; set; }
    }

}

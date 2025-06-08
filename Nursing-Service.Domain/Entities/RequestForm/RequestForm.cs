using Nursing_Service.Domain.Entities.Base;

namespace Nursing_Service.Domain.Entities.RequestForm
{
    public class RequestForm : BaseEntity
    {
        public required string FullName { get; set; }
        public required string PhoneNumber { get; set; }
        public required string Address { get; set; }
        public string Name { get; set; }
    }
}

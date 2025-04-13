namespace Nursing_Service.Domain.Entities.Base
{
    public class BaseEntityNoId
    {
        public DateTime CreatedDateTime { get; set; }
        public DateTime? UpdatedDateTime { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedDateTime { get; set; }
    }
}

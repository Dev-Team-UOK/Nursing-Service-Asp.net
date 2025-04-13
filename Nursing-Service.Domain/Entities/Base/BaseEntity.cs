namespace Nursing_Service.Domain.Entities.Base
{
    public abstract class BaseEntity<TType>
    {
        public TType Id { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public DateTime? UpdatedDateTime { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedDateTime { get; set; }
    }

    public abstract class BaseEntity : BaseEntity<ulong>
    {
    }
}

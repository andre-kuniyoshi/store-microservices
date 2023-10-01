
namespace Core.Common
{
    public abstract class BaseControlEntity : IEntity, IControlEntity
    {
        public Guid Id { get; set; }
        public bool Active { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string? LastModifiedBy { get; set; }
        public DateTime? LastModifiedDate { get; set; }
    }
}

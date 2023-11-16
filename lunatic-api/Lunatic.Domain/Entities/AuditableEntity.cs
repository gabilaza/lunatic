
namespace Lunatic.Domain.Entities {
    public class AuditableEntity {
        public Guid CreatedByUserId { get; protected set; }
        public DateTime CreatedDate { get; protected set; }
        public Guid LastModifiedByUserId { get; set; }
        public DateTime LastModifiedDate { get; set; }

        protected AuditableEntity(Guid createdByUserId) {
            CreatedByUserId = createdByUserId;
            CreatedDate = DateTime.Now;
            LastModifiedByUserId = CreatedByUserId;
            LastModifiedDate = CreatedDate;
        }
    }
}

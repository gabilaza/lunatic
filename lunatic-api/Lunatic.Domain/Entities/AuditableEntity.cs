
namespace Lunatic.Domain.Entities {
    public class AuditableEntity {
        public User CreatedByUser { get; protected set; }
        public DateTime CreatedDate { get; protected set; }
        public User LastModifiedByUser { get; set; }
        public DateTime LastModifiedDate { get; set; }

        protected AuditableEntity(User createdByUser) {
            CreatedByUser = createdByUser;
            CreatedDate = DateTime.UtcNow;

            LastModifiedByUser = CreatedByUser;
            LastModifiedDate = CreatedDate;
        }
    }
}

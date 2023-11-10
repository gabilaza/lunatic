
using Lunatic.Domain.Utils;

namespace Lunatic.Domain.Entities {
    public class Issue : AuditableEntity {
        private Issue(string title, string description) {
            Id = Guid.NewGuid();
            Title = title;
            Description = description;
        }

        public Guid Id { get; private set; }
        public string Title { get; private set; }
        public string Description { get; private set; }

        public static Result<Issue> New(string title, string description) {
            if(string.IsNullOrWhiteSpace(title)) {
                return Result<Issue>.Failure("Title is required.");
            }

            if(string.IsNullOrWhiteSpace(description)) {
                return Result<Issue>.Failure("Description is required.");
            }

            return Result<Issue>.Success(new Issue(title, description));
        }
    }
}

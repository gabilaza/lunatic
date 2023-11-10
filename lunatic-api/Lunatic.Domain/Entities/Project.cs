
using Lunatic.Domain.Utils;

namespace Lunatic.Domain.Entities {
    public class Project : AuditableEntity {
        private Project(string title, string description) {
            Id = Guid.NewGuid();
            Title = title;
            Description = description;
        }

        public Guid Id { get; private set; }
        public string Title { get; private set; }
        public string Description { get; private set; }
        public List<Task>? Tasks { get; private set; }

        public static Result<Project> Create(string title, string description) {
            if(string.IsNullOrWhiteSpace(title)) {
                return Result<Project>.Failure("Title is required.");
            }

            if(string.IsNullOrWhiteSpace(description)) {
                return Result<Project>.Failure("Description is required.");
            }

            return Result<Project>.Success(new Project(title, description));
        }

        public void AddTask(Task task) {
            if(Tasks == null) {
                Tasks = new List<Task>();
            }

            Tasks.Add(task);
        }
    }
}

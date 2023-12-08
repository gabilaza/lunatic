
using Lunatic.Domain.Utils;

namespace Lunatic.Domain.Entities {
    public class Project : AuditableEntity {
        private Project(Guid createdByUserId, string title, string description) : base(createdByUserId) {
            Id = Guid.NewGuid();
            Title = title;
            Description = description;
        }

        public Guid Id { get; private set; }
        public string Title { get; private set; }
        public string Description { get; private set; }
        public List<Guid>? TaskIds { get; private set; }

        public static Result<Project> Create(Guid createdByUserId, string title, string description) {
            if(createdByUserId == default) {
                return Result<Project>.Failure("Created User id should not be default.");
            }

            if(string.IsNullOrWhiteSpace(title)) {
                return Result<Project>.Failure("Title is required.");
            }

            if(string.IsNullOrWhiteSpace(description)) {
                return Result<Project>.Failure("Description is required.");
            }

            return Result<Project>.Success(new Project(createdByUserId, title, description));
        }

        public void Update(string title,  string description, List<Guid> taskIds)
        {
            Title = title;
            Description = description;
            TaskIds = taskIds;
        }

        public void AddTask(Task task) {
            if(TaskIds == null) {
                TaskIds = new List<Guid>();
            }

            TaskIds.Add(task.Id);
        }
    }
}


using Lunatic.Domain.Utils;

namespace Lunatic.Domain.Entities {
    public class Project : AuditableEntity {
        private Project(User createdByUser, Team team, string title, string description) : base(createdByUser) {
            Id = Guid.NewGuid();
            Team = team;

            Title = title;
            Description = description;
        }

        public Guid Id { get; private set; }
        public Team Team { get; private set; }

        public string Title { get; private set; }
        public string Description { get; private set; }

        public ICollection<Task> Tasks { get; private set; } = new List<Task>();

        public static Result<Project> Create(User createdByUser, Team team,string title, string description) {
            if(string.IsNullOrWhiteSpace(title)) {
                return Result<Project>.Failure("Title is required.");
            }

            if(string.IsNullOrWhiteSpace(description)) {
                return Result<Project>.Failure("Description is required.");
            }

            return Result<Project>.Success(new Project(createdByUser, team, title, description));
        }

        public void Update(string title, string description) {
            Title = title;
            Description = description;
        }

        public void AddTask(Task task) => Tasks.Add(task);
        public void RemoveTask(Task task) => Tasks.Remove(task);
    }
}

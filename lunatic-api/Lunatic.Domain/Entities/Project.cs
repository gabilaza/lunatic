
using Lunatic.Domain.Utils;

namespace Lunatic.Domain.Entities {
    public class Project : AuditableEntity {
        private Project(Guid createdByUserId, Team team, string title, string description) : base(createdByUserId) {
            Id = Guid.NewGuid();
            Team = team;

            Title = title;
            Description = description;
        }

        public Guid Id { get; private set; }
        public Team Team { get; private set; }

        public string Title { get; private set; }
        public string Description { get; private set; }

        public ICollection<Guid> TaskIds { get; private set; } = new List<Guid>();

        public static Result<Project> Create(Guid createdByUserId, Team team,string title, string description) {
            if(createdByUserId == default) {
                return Result<Project>.Failure("Created by User Id is required.");
            }

            if(string.IsNullOrWhiteSpace(title)) {
                return Result<Project>.Failure("Title is required.");
            }

            if(string.IsNullOrWhiteSpace(description)) {
                return Result<Project>.Failure("Description is required.");
            }

            return Result<Project>.Success(new Project(createdByUserId, team, title, description));
        }

        public void Update(string title, string description) {
            Title = title;
            Description = description;
        }

        public void AddTask(Task task) => TaskIds.Add(task.Id);
        public void RemoveTask(Task task) => TaskIds.Remove(task.Id);
    }
}

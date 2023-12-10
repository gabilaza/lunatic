
using Lunatic.Domain.Utils;

namespace Lunatic.Domain.Entities {
    public class Team : AuditableEntity {
        private Team(User createdByUser, string name) : base(createdByUser) {
            Id = Guid.NewGuid();

            Name = name;
        }

        public Guid Id { get; private set; }

        public string Name { get; private set; }

        public ICollection<User> Members { get; private set; } = new List<User>();
        public ICollection<Project> Projects { get; private set; } = new List<Project>();

        public static Result<Team> Create(User createdByUser, string name) {
            if(string.IsNullOrWhiteSpace(name)) {
                return Result<Team>.Failure("Name is required.");
            }

            return Result<Team>.Success(new Team(createdByUser, name));
        }

        public void Update(string name, List<User> members, List<Project> projects) {
            Name = name;
            Members = members;
            Projects = projects;
        }

        public void AddMember(User user) => Members.Add(user);
        public void RemoveMember(User user) => Members.Remove(user);

        public void AddProject(Project project) => Projects.Add(project);
        public void RemoveProject(Project project) => Projects.Remove(project);
    }
}

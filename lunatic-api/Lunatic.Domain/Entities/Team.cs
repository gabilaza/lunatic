
using Lunatic.Domain.Utils;

namespace Lunatic.Domain.Entities {
    public class Team : AuditableEntity {
        private Team(Guid createdByUserId, string name) : base(createdByUserId) {
            Id = Guid.NewGuid();
            Name = name;
        }

        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public List<User>? Members { get; private set; }
        public List<Project>? Projects { get; private set; }

        public static Result<Team> Create(Guid createdByUserId, string name) {
            if(createdByUserId == default) {
                return Result<Team>.Failure("Created User id should not be default.");
            }

            if(string.IsNullOrWhiteSpace(name)) {
                return Result<Team>.Failure("Name is required.");
            }

            return Result<Team>.Success(new Team(createdByUserId, name));
        }

        public void AddMember(User user) {
            if(Members == null) {
                Members = new List<User>();
            }

            Members.Add(user);
        }

        public void AddProject(Project project) {
            if(Projects == null) {
                Projects = new List<Project>();
            }

            Projects.Add(project);
        }
    }
}

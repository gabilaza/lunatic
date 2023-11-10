
using Lunatic.Domain.Utils;

namespace Lunatic.Domain.Entities {
    public class Team : AuditableEntity {
        private Team(string name) {
            Id = Guid.NewGuid();
            Name = name;
        }

        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public List<User>? Members { get; private set; }
        // public Dictionary<User, TeamRole>? Members { get; private set; }
        public List<Project>? Projects { get; private set; }

        public static Result<Team> Create(string name) {
            if(string.IsNullOrWhiteSpace(name)) {
                return Result<Team>.Failure("Name is required.");
            }

            return Result<Team>.Success(new Team(name));
        }

        // public void AddMember(User user, TeamRole teamRole) {
        //     if(Members == null) {
        //         Members = new Dictionary<User, TeamRole>();
        //     }
        //
        //     // hm...
        //     Members.TryAdd(user, teamRole);
        // }
        public void AddUser(User user) {
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

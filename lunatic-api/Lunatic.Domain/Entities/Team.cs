
using Lunatic.Domain.Utils;

namespace Lunatic.Domain.Entities {
    public class Team : AuditableEntity {
        private Team(string name) {
            Id = Guid.NewGuid();
            Name = name;
        }

        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public List<Tuple<User, TeamRole>>? Users { get; private set; }
        public List<Project>? Projects { get; private set; }

        public static Result<Team> Create(string name) {
            if(string.IsNullOrWhiteSpace(name)) {
                return Result<Team>.Failure("Name is required.");
            }

            return Result<Team>.Success(new Team(name));
        }

        public void AddUser(User user, TeamRole teamRole) {
            if(Users == null) {
                Users = new List<Tuple<User, TeamRole>>();
            }

            Users.Add(Tuple.Create(user, teamRole));
        }

        public void AddProject(Project project) {
            if(Projects == null) {
                Projects = new List<Project>();
            }

            Projects.Add(project);
        }
    }
}

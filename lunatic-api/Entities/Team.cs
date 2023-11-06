
using Utils;

namespace Entities {
    public class Team {
        private Team(string name) {
            Id = Guid.NewGuid();
            Name = name;
        }

        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public List<User>? Users { get; private set; }
        public List<Project>? Projects { get; private set; }

        public static Result<Team> New(string name) {
            if(string.IsNullOrWhiteSpace(name)) {
                return Result<Team>.Failure("Name is required.");
            }

            return Result<Team>.Success(new Team(name));
        }

        public void AddUser(User user) {
            if(Users == null) {
                Users = new List<User>();
            }

            Users.Add(user);
        }

        public void AddProject(Project project) {
            if(Projects == null) {
                Projects = new List<Project>();
            }

            Projects.Add(project);
        }
    }
}

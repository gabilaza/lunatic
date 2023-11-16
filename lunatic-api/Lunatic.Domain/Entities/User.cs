
using Lunatic.Domain.Utils;

namespace Lunatic.Domain.Entities {
    public class User {
        private User(string username, string password, Role role) {
            CreatedDate = DateTime.Now;
            Id = Guid.NewGuid();
            Username = username;
            Password = password;
            Role = role;
        }

        public DateTime CreatedDate { get; private set; }
        public Guid Id { get; private set; }
        public string Username { get; private set; }
        public string Password { get; private set; }
        public Role Role { get; private set; }
        public List<Team>? Teams { get; private set; }

        public static Result<User> Create(string username, string password, Role role) {
            if(string.IsNullOrWhiteSpace(username)) {
                return Result<User>.Failure("Username is required.");
            }

            if(string.IsNullOrWhiteSpace(password)) {
                return Result<User>.Failure("Password is required.");
            }

            return Result<User>.Success(new User(username, password, role));
        }

        public void AddTeam(Team team) {
            if(Teams == null) {
                Teams = new List<Team>();
            }

            Teams.Add(team);
        }
    }
}

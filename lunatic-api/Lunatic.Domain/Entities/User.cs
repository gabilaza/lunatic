
using Lunatic.Domain.Utils;

namespace Lunatic.Domain.Entities {
    public class User : AuditableEntity {
        private User(string username) {
            Id = Guid.NewGuid();
            Username = username;
        }

        public Guid Id { get; private set; }
        public string Username { get; private set; }

        public static Result<User> New(string username) {
            if(string.IsNullOrWhiteSpace(username)) {
                return Result<User>.Failure("Username is required.");
            }

            return Result<User>.Success(new User(username));
        }
    }
}

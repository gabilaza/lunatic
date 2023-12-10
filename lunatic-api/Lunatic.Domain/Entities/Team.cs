
using Lunatic.Domain.Utils;

namespace Lunatic.Domain.Entities {
    public class Team : AuditableEntity {
        private Team(Guid createdByUserId, string name) : base(createdByUserId) {
            Id = Guid.NewGuid();

            Name = name;
        }

        public Guid Id { get; private set; }

        public string Name { get; private set; }

        public ICollection<Guid> MemberIds { get; private set; } = new List<Guid>();
        public ICollection<Guid> ProjectIds { get; private set; } = new List<Guid>();

        public static Result<Team> Create(Guid createdByUserId, string name) {
            if(createdByUserId == default) {
                return Result<Team>.Failure("Created by User Id is required.");
            }

            if(string.IsNullOrWhiteSpace(name)) {
                return Result<Team>.Failure("Name is required.");
            }

            return Result<Team>.Success(new Team(createdByUserId, name));
        }

        public void Update(string name) {
            Name = name;
        }

        public void AddMember(User user) => MemberIds.Add(user.Id);
        public void RemoveMember(User user) => MemberIds.Remove(user.Id);

        public void AddProject(Project project) => ProjectIds.Add(project.Id);
        public void RemoveProject(Project project) => ProjectIds.Remove(project.Id);
    }
}

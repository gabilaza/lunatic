
using Lunatic.Domain.Entities;


namespace Lunatic.Domain.Tests.Entities.TeamTests {
    public class AddAndRemoveMemberTests {
        public const string TeamName = "Lunatic";
        public const string TeamDescription = "Lunatic Description";

        public const string UserFirstName = "Gabriel";
        public const string UserLastName = "Laza";
        public const string UserEmail = "glaza@gmail.com";
        public const string UserUsername = "glaza";
        public const string UserPassword = "String123#";
        public const Role UserRole = Role.USER;

        [Fact]
        public void GivenTeamWithUser_WhenAddAndRemoveMember_ThenMemberAddedAndRemovedSuccessfully() {
            // given
            var team = Team.Create(Guid.NewGuid(), TeamName, TeamDescription).Value;
            var user = User.Create(UserFirstName, UserLastName, UserEmail, UserUsername, UserPassword, UserRole).Value;
            var userId = Guid.NewGuid();

            // when
            team.AddMember(user);
            team.AddMember(userId);
            team.RemoveMember(user);
            team.RemoveMember(userId);

            // then
            Assert.DoesNotContain(user.UserId, team.MemberIds);
            Assert.DoesNotContain(userId, team.MemberIds);
        }
    }
}


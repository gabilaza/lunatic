
using NSubstitute;
using Lunatic.Domain.Entities;


namespace Lunatic.Domain.Tests.Entities.UserTests {
    public class AddAndRemoveTeamTests {
        public const string FirstName = "Gabriel";
        public const string LastName = "Laza";
        public const string Email = "glaza@gmail.com";
        public const string Username = "glaza";
        public const string Password = "String123#";
        public const Role MockRole = Role.USER;

        public const string Name = "Lunatic";
        public const string Description = "Lunatic Description";

        [Fact]
        public void GivenUserWithTeams_WhenAddAndRemoveTeam_ThenTeamAddedAndRemovedSuccessfully() {
            // given
            var user = User.Create(FirstName, LastName, Email, Username, Password, MockRole).Value;
            var team = Team.Create(Guid.NewGuid(), Name, Description).Value;
            var teamId = Guid.NewGuid();

            // when
            user.AddTeam(team);
            user.AddTeam(teamId);
            user.RemoveTeam(team);
            user.RemoveTeam(teamId);

            // then
            Assert.DoesNotContain(team.TeamId, user.TeamIds);
            Assert.DoesNotContain(teamId, user.TeamIds);
        }
    }
}


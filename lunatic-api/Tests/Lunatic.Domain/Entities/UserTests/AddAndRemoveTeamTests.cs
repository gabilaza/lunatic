
using Lunatic.Domain.Entities;


namespace Tests.Lunatic.Domain.Entities.UserTests {
    public class AddAndRemoveTeamTests {
        public const string UserFirstName = "Gabriel";
        public const string UserLastName = "Laza";
        public const string UserEmail = "glaza@gmail.com";
        public const string UserUsername = "glaza";
        public const string UserPassword = "String123#";
        public const Role UserRole = Role.USER;

        public const string TeamName = "Lunatic";
        public const string TeamDescription = "Lunatic Description";

        [Fact]
        public void GivenUserWithTeams_WhenAddAndRemoveTeam_ThenTeamAddedAndRemovedSuccessfully() {
            // given
            var user = User.Create(UserFirstName, UserLastName, UserEmail, UserUsername, UserPassword, UserRole).Value;
            var team = Team.Create(Guid.NewGuid(), TeamName, TeamDescription).Value;
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


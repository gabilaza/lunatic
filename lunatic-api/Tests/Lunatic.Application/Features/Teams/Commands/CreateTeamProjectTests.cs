using Lunatic.Application.Features.Teams.Commands.CreateTeamProject;
using Lunatic.Application.Persistence;
using Lunatic.Domain.Entities;
using Lunatic.Domain.Utils;
using NSubstitute;

namespace Tests.Lunatic.Application.Features.Teams.Commands
{
    public class CreateTeamProjectTests
    {
        private static Result<User> user = User.Create("firstName", "lastName", "email@email.com", "username", "password", Role.USER);
        private static Result<Team> team = Team.Create(user.Value.UserId, "teamName", "teamDescription");

        [Fact]
		public async void GivenCreateTeamProjectCommand_WhenCreate_Then_SuccessResponse()
        {
            // given
            var userRepository = Substitute.For<IUserRepository>();
            var teamRepository = Substitute.For<ITeamRepository>();
            var projectRepository = Substitute.For<IProjectRepository>();

            userRepository.ExistsByIdAsync(user.Value.UserId).Returns(true);
            teamRepository.ExistsByIdAsync(team.Value.TeamId).Returns(true);
            teamRepository.FindByIdAsync(team.Value.TeamId).Returns(team);

            var command = new CreateTeamProjectCommand
            {
                TeamId = team.Value.TeamId,
                UserId = user.Value.UserId,
                Title = "projectTitle",
                Description = "projectDescription"
            };

            var commandHandler = new CreateTeamProjectCommandHandler(projectRepository, teamRepository, userRepository);

            var source = new CancellationTokenSource();

            // when
            var response = await commandHandler.Handle(command, source.Token);

            // then
            Assert.True(response.Success);
            Assert.NotNull(response.Project);
            Assert.Null(response.ValidationErrors);
        }
	}
}

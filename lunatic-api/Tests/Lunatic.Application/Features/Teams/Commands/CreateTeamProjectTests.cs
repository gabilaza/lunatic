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
		public async void GivenCreateTeamProjectCommand_WhenCreate_ThenSuccessResponse()
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

        //[Theory]
        //[InlineData(new[]("", team.Value.TeamId, "", ""))]
        [Fact]
        public async void GivenInvalidUserId_WhenCreate_ThenFailureResponse()
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
                UserId = Guid.NewGuid(),
                Title = "projectTitle",
                Description = "projectDescription"
            };

            var commandHandler = new CreateTeamProjectCommandHandler(projectRepository, teamRepository, userRepository);

            var source = new CancellationTokenSource();

            var response = await commandHandler.Handle(command, source.Token);

            Assert.False(response.Success);
            Assert.Null(response.Project);
            Assert.NotNull(response.ValidationErrors);
		}

		[Fact]
		public async void GivenInvalidTeamId_WhenCreate_ThenFailureResponse()
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
				TeamId = Guid.NewGuid(),
				UserId = user.Value.UserId,
				Title = "projectTitle",
				Description = "projectDescription"
			};

			var commandHandler = new CreateTeamProjectCommandHandler(projectRepository, teamRepository, userRepository);

			var source = new CancellationTokenSource();

			var response = await commandHandler.Handle(command, source.Token);

			Assert.False(response.Success);
			Assert.Null(response.Project);
			Assert.NotNull(response.ValidationErrors);
		}

		[Fact]
		public async void GivenInvalidTitle_WhenCreate_ThenFailureResponse()
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
				Title = "",
				Description = "projectDescription"
			};

			var commandHandler = new CreateTeamProjectCommandHandler(projectRepository, teamRepository, userRepository);

			var source = new CancellationTokenSource();

			var response = await commandHandler.Handle(command, source.Token);

			Assert.False(response.Success);
			Assert.Null(response.Project);
			Assert.NotNull(response.ValidationErrors);
		}

		[Fact]
		public async void GivenInvalidDescription_WhenCreate_ThenFailureResponse()
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
				Description = ""
			};

			var commandHandler = new CreateTeamProjectCommandHandler(projectRepository, teamRepository, userRepository);

			var source = new CancellationTokenSource();

			var response = await commandHandler.Handle(command, source.Token);

			Assert.False(response.Success);
			Assert.Null(response.Project);
			Assert.NotNull(response.ValidationErrors);
		}
	}
}

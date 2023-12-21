using Lunatic.Application.Features.Teams.Commands.CreateTeam;
using Lunatic.Application.Persistence;
using Lunatic.Domain.Entities;
using Lunatic.Domain.Utils;
using NSubstitute;

namespace Tests.Lunatic.Application.Features.Teams.Commands
{
    public class CreateTeamTests
    {
        private Result<User> user = User.Create("firstName", "lastName", "email@email.com", "username", "password", Role.USER);

        [Fact]
        public async void GivenCreateTeamCommand_WhenCreate_Then_SuccessResponse()
        {
            // given
            var userRepository = Substitute.For<IUserRepository>();
            var teamRepository = Substitute.For<ITeamRepository>();

            userRepository.FindByIdAsync(user.Value.UserId).Returns(user);
            userRepository.ExistsByIdAsync(user.Value.UserId).Returns(true);

            var command = new CreateTeamCommand
            {
                Name = "TeamName",
                Description = "TeamDescription",
                UserId = user.Value.UserId
            };

            var commandHandler = new CreateTeamCommandHandler(teamRepository, userRepository);
            var source = new CancellationTokenSource();

            // when
            var response = await commandHandler.Handle(command, source.Token);

            // then
            Assert.True(response.Success);
            Assert.NotNull(response.Team);
            Assert.Null(response.ValidationErrors);
        }
    }
}

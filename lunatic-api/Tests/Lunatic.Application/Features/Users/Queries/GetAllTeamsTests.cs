
using Lunatic.Domain.Entities;
using Lunatic.Domain.Utils;
using Lunatic.Application.Persistence;
using Lunatic.Application.Features.Users.Queries.GetAllTeams;
using NSubstitute;


namespace Tests.Lunatic.Application.Features.Users.Commands {
    public class GetAllTeamsTests {
        [Fact]
        public async void GivenGetAllUserTeamsComamnd_WhenGetAllUserTeams_ThenFailureResponse() {
            // given
            var userRepository = Substitute.For<IUserRepository>();
            var userId = Guid.NewGuid();
            userRepository.FindByIdAsync(userId).Returns(Result<User>.Failure("Lunatic Entity Not Found"));
            var teamRepository = Substitute.For<ITeamRepository>();
            var command = new GetAllUserTeamsQuery(userId);
            var commandHandler = new GetAllUserTeamsQueryHandler(userRepository, teamRepository);
            var source = new CancellationTokenSource();

            // when
            var response = await commandHandler.Handle(command, source.Token);

            // then
            Assert.False(response.Success);
            Assert.Null(response.Teams);
            Assert.NotNull(response.ValidationErrors);
        }
    }
}


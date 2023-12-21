
using Lunatic.Domain.Entities;
using Lunatic.Domain.Utils;
using Lunatic.Application.Persistence;
using Lunatic.Application.Features.Users.Queries.GetAllTeams;
using NSubstitute;


namespace Tests.Lunatic.Application.Features.Users.Queries {
    public class GetAllTeamsTests {
        [Fact]
        public async void GivenGetAllUserTeamsComamnd_WhenGetAllUserTeams_ThenFailureResponse() {
            // given
            var userRepository = Substitute.For<IUserRepository>();
            var userId = Guid.NewGuid();
            userRepository.FindByIdAsync(userId).Returns(Result<User>.Failure("Lunatic Entity Not Found"));
            var teamRepository = Substitute.For<ITeamRepository>();
            var query = new GetAllUserTeamsQuery(userId);
            var queryHandler = new GetAllUserTeamsQueryHandler(userRepository, teamRepository);
            var source = new CancellationTokenSource();

            // when
            var response = await queryHandler.Handle(query, source.Token);

            // then
            Assert.False(response.Success);
            Assert.Null(response.Teams);
            Assert.NotNull(response.ValidationErrors);
        }
    }
}


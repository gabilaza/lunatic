
using Lunatic.Domain.Entities;
using Lunatic.Application.Persistence;
using Lunatic.Application.Features.Users.Commands.CreateUser;
using NSubstitute;


namespace Tests.Lunatic.Application.Features.Users.Commands {
    public class CreateUserTests {
        public const string FirstName = "Gabriel";
        public const string LastName = "Laza";
        public const string Email = "glaza@gmail.com";
        public const string Username = "glaza";
        public const string Password = "String123#";
        public const Role MockRole = Role.USER;

        [Fact]
        public async void GivenCreateUserComamnd_WhenCreate_ThenSuccessResult() {
            // given
            var userRepository = Substitute.For<IUserRepository>();
            var command = new CreateUserCommand {
                FirstName = FirstName,
                LastName = LastName,
                Email = Email,
                Username = Username,
                Password = Password,
                Role = MockRole
            };
            var commandHandler = new CreateUserCommandHandler(userRepository);
            var source = new CancellationTokenSource();

            // when
            var response = await commandHandler.Handle(command, source.Token);

            // then
            Assert.True(response.Success);
            Assert.NotNull(response.User);
            Assert.Null(response.ValidationErrors);
        }
    }
}


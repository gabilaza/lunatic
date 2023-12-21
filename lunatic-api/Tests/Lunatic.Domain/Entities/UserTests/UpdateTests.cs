
using Lunatic.Domain.Entities;


namespace Tests.Lunatic.Domain.Entities.UserTests {
    public class UpdateTests {
        public const string FirstName = "Gabriel";
        public const string LastName = "Laza";
        public const string Email = "glaza@gmail.com";
        public const string Username = "glaza";
        public const string Password = "String123#";

        [Fact]
        public void GivenUser_WhenUpdateUser_ThenUpdatedSuccessfully() {
            // given
            var user = User.Create(FirstName, LastName, Email, Username, Password, Role.USER).Value;

            // when
            user.Update("Update" + FirstName, "Update" + LastName, "Update" + Email, "Update" + Username, "Update" + Password, Role.ADMIN);

            // then
            Assert.Equal("Update" + FirstName, user.FirstName);
            Assert.Equal("Update" + LastName, user.LastName);
            Assert.Equal("Update" + Email, user.Email);
            Assert.Equal("Update" + Username, user.Username);
            Assert.Equal("Update" + Password, user.Password);
            Assert.Equal(Role.ADMIN, user.Role);
        }
    }
}


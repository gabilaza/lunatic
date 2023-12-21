
using Lunatic.Domain.Entities;
using Task = Lunatic.Domain.Entities.Task;


namespace Tests.Lunatic.Domain.Entities.TaskTests {
    public class AddAndRemoveAssigneeTests {
        public const string TaskTitle = "Lunatic";
        public const string TaskDescription = "Lunatic Description";

        public const string UserFirstName = "Gabriel";
        public const string UserLastName = "Laza";
        public const string UserEmail = "glaza@gmail.com";
        public const string UserUsername = "glaza";
        public const string UserPassword = "String123#";
        public const Role UserRole = Role.USER;

        [Fact]
        public void GivenTaskWithUser_WhenAddAndRemoveMember_ThenMemberAddedAndRemovedSuccessfully() {
            // given
            var task = Task.Create(Guid.NewGuid(), Guid.NewGuid(), TaskTitle, TaskDescription, TaskPriority.LOW).Value;
            var user = User.Create(UserFirstName, UserLastName, UserEmail, UserUsername, UserPassword, UserRole).Value;
            var userId = Guid.NewGuid();

            // when
            task.AddAssignee(user);
            task.AddAssignee(userId);
            task.RemoveAssignee(user);
            task.RemoveAssignee(userId);

            // then
            Assert.DoesNotContain(user.UserId, task.AssigneeIds);
            Assert.DoesNotContain(userId, task.AssigneeIds);
        }
    }
}


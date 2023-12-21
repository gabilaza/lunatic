
using Lunatic.Domain.Entities;
using Task = Lunatic.Domain.Entities.Task;
using TaskStatus = Lunatic.Domain.Entities.TaskStatus;


namespace Tests.Lunatic.Domain.Entities.TaskTests {
    public class UpdateTests {
        public const string Title = "Lunatic";
        public const string Description = "Lunatic Description";

        [Fact]
        public void GivenTask_WhenUpdateTask_ThenUpdatedSuccessfully() {
            // given
            var task = Task.Create(Guid.NewGuid(), Guid.NewGuid(), Title, Description, TaskPriority.LOW).Value;

            // when
            task.Update("Update" + Title, "Update" + Description, TaskPriority.MEDIUM, TaskStatus.IN_PROGRESS);

            // then
            Assert.Equal("Update" + Title, task.Title);
            Assert.Equal("Update" + Description, task.Description);
            Assert.Equal(TaskPriority.MEDIUM, task.Priority);
            Assert.Equal(TaskStatus.IN_PROGRESS, task.Status);
        }
    }
}


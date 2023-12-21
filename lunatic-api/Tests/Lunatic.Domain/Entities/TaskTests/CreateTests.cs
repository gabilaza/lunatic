
using Lunatic.Domain.Entities;
using Task = Lunatic.Domain.Entities.Task;


namespace Tests.Lunatic.Domain.Entities.TaskTests {
    public class CreateTests {
        public const string Title = "Lunatic";
        public const string Description = "Lunatic Description";
        public const TaskPriority Priority = TaskPriority.LOW;

        [Fact]
        public void GivenValidTask_WhenCreateTask_ThenSuccessResult() {
            // given

            // when
            var taskResult = Task.Create(Guid.NewGuid(), Guid.NewGuid(), Title, Description, Priority);

            // then
            Assert.True(taskResult.IsSuccess);
        }

        [Theory]
        [InlineData(null, Description)]
        [InlineData(Title, null)]
        [InlineData("", Description)]
        [InlineData(Title, "")]
        public void GivenInvalidTask_WhenCreateTask_ThenFailureResult(string title, string description) {
            // given

            // when
            var taskResult = Task.Create(Guid.NewGuid(), Guid.NewGuid(), title, description, Priority);

            // then
            Assert.False(taskResult.IsSuccess);
        }

        [Fact]
        public void GivenInvalidTask_WhenCreateTask_ThenFailureResult_2() {
            // given

            // when
            var taskResult = Task.Create(default, Guid.NewGuid(), Title, Description, Priority);

            // then
            Assert.False(taskResult.IsSuccess);
        }

        [Fact]
        public void GivenInvalidTask_WhenCreateTask_ThenFailureResult_3() {
            // given

            // when
            var taskResult = Task.Create(Guid.NewGuid(), default, Title, Description, Priority);

            // then
            Assert.False(taskResult.IsSuccess);
        }
    }
}


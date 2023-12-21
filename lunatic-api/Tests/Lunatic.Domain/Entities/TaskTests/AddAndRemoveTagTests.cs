
using Lunatic.Domain.Entities;
using Task = Lunatic.Domain.Entities.Task;


namespace Tests.Lunatic.Domain.Entities.TaskTests {
    public class AddAndRemoveTagTests {
        public const string Title = "Lunatic";
        public const string Description = "Lunatic Description";
        public const TaskPriority Priority = TaskPriority.LOW;

        [Fact]
        public void GivenTaskWithProject_WhenAddAndRemoveProject_ThenProjectAddedAndRemovedSuccessfully() {
            // given
            var task = Task.Create(Guid.NewGuid(), Guid.NewGuid(), Title, Description, Priority).Value;
            var tag = Tag.DOCUMENTATION;

            // when
            task.AddTag(tag);
            task.RemoveTag(tag);

            // then
            Assert.DoesNotContain(tag, task.Tags);
        }
    }
}


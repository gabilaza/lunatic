
using Lunatic.Domain.Entities;
using Task = Lunatic.Domain.Entities.Task;


namespace Tests.Lunatic.Domain.Entities.ProjectTests {
    public class AddAndRemoveTaskTests {
        public const string ProjectTitle = "Lunatic";
        public const string ProjectDescription = "Lunatic Description";

        public const string TaskTitle = "Lunatic";
        public const string TaskDescription = "Lunatic Description";

        [Fact]
        public void GivenProjectWithProject_WhenAddAndRemoveProject_ThenProjectAddedAndRemovedSuccessfully() {
            // given
            var project = Project.Create(Guid.NewGuid(), Guid.NewGuid(), ProjectTitle, ProjectDescription).Value;
            var task = Task.Create(Guid.NewGuid(), Guid.NewGuid(), TaskTitle, TaskDescription, TaskPriority.LOW).Value;
            var taskId = Guid.NewGuid();

            // when
            project.AddTask(task);
            project.AddTask(taskId);
            project.RemoveTask(task);
            project.RemoveTask(taskId);

            // then
            Assert.DoesNotContain(task.TaskId, project.TaskIds);
            Assert.DoesNotContain(taskId, project.TaskIds);
        }
    }
}


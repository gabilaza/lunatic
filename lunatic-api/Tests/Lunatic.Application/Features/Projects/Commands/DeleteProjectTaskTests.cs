
using Lunatic.Application.Features.Projects.Commands.DeleteProjectTask;
using Lunatic.Application.Persistence;
using Lunatic.Domain.Entities;
using Task = Lunatic.Domain.Entities.Task;
using Lunatic.Domain.Utils;
using NSubstitute;

namespace Tests.Lunatic.Application.Features.Projects.Commands {
    public class DeleteProjectTaskTests {
        private static Result<Project> project = Project.Create(Guid.NewGuid(), Guid.NewGuid(), "projectTitle", "projectDescription");
        private static Result<Task> task = Task.Create(Guid.NewGuid(), Guid.NewGuid(), "taskTitle", "taskTitle", TaskPriority.LOW);

        public DeleteProjectTaskTests() {
            project.Value.AddTask(task.Value);
        }

        [Fact]
        public async void GivenCreateProjectTaskCommand_WhenCreate_Then_SuccessResponse() {
            var projectRepository = Substitute.For<IProjectRepository>();
            var taskRepository = Substitute.For<ITaskRepository>();

            taskRepository.ExistsByIdAsync(task.Value.TaskId).Returns(true);
            projectRepository.ExistsByIdAsync(project.Value.ProjectId).Returns(true);
            projectRepository.FindByIdAsync(project.Value.ProjectId).Returns(project);
            taskRepository.DeleteAsync(task.Value.TaskId).Returns(task);

            var command = new DeleteProjectTaskCommand {
                ProjectId = project.Value.ProjectId,
                TaskId = task.Value.TaskId
            };

            var commandHandler = new DeleteProjectTaskCommandHandler(projectRepository, taskRepository);

            var source = new CancellationTokenSource();

            var response = await commandHandler.Handle(command, source.Token);

            Assert.True(response.Success);
            Assert.Null(response.ValidationErrors);
        }

    }
}

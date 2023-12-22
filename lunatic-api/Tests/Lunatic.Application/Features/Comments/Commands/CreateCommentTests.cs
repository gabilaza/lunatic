
using Lunatic.Application.Features.Tasks.Commands.CreateTaskComment;
using Lunatic.Application.Persistence;
using Task = Lunatic.Domain.Entities.Task;
using Lunatic.Domain.Entities;
using Lunatic.Domain.Utils;
using NSubstitute;

namespace Tests.Lunatic.Application.Features.Comments.Commands {
    public class CreateTaskCommentTests {
        private static Result<User> user = User.Create("firstName", "lastName", "email@email.com", "username", "password", Role.USER);
        private static Result<Task> task = Task.Create(user.Value.UserId, Guid.NewGuid(), "projectTitle", "projectDescription", TaskPriority.LOW);
        private static Result<Comment> comment = Comment.Create(user.Value.UserId, task.Value.TaskId, "commentContent");

        [Fact]
        public async void GivenCreateProjectTaskCommand_WhenCreate_ThenSuccessResponse() {
            var userRepository = Substitute.For<IUserRepository>();
            var taskRepository = Substitute.For<ITaskRepository>();
            var commentRepository = Substitute.For<ICommentRepository>();

            userRepository.ExistsByIdAsync(user.Value.UserId).Returns(true);
            taskRepository.ExistsByIdAsync(task.Value.TaskId).Returns(true);
            taskRepository.FindByIdAsync(task.Value.TaskId).Returns(task);

            var command = new CreateTaskCommentCommand {
                UserId = user.Value.UserId,
                TaskId = task.Value.TaskId,

                Content = "Test"
            };

            var commandHandler = new CreateTaskCommentCommandHandler(taskRepository, commentRepository, userRepository);

            var source = new CancellationTokenSource();

            var response = await commandHandler.Handle(command, source.Token);

            Assert.True(response.Success);
            Assert.NotNull(response.Comment);
            Assert.Null(response.ValidationErrors);
        }

        // [Fact]
        // public async void GivenInvalidUserId_WhenCreate_Then_FailureResponse() {
        //     var userRepository = Substitute.For<IUserRepository>();
        //     var projectRepository = Substitute.For<IProjectRepository>();
        //     var taskRepository = Substitute.For<ITaskRepository>();
        //
        //     userRepository.ExistsByIdAsync(user.Value.UserId).Returns(true);
        //     projectRepository.ExistsByIdAsync(project.Value.ProjectId).Returns(true);
        //     projectRepository.FindByIdAsync(project.Value.ProjectId).Returns(project);
        //
        //     var command = new CreateProjectTaskCommand
        //     {
        //         UserId = Guid.NewGuid(),
        //         Title = "Test",
        //         Description = "Test",
        //         Priority = TaskPriority.HIGH,
        //         ProjectId = project.Value.ProjectId,
        //     };
        //
        //     var commandHandler = new CreateProjectTaskCommandHandler(taskRepository, projectRepository, userRepository);
        //
        //     var source = new CancellationTokenSource();
        //
        //     var response = await commandHandler.Handle(command, source.Token);
        //
        //     Assert.False(response.Success);
        //     Assert.Null(response.Task);
        //     Assert.NotNull(response.ValidationErrors);
        // }
        //
        // [Fact]
        // public async void GivenInvalidProjectId_WhenCreate_Then_FailureResponse()
        // {
        //     var userRepository = Substitute.For<IUserRepository>();
        //     var projectRepository = Substitute.For<IProjectRepository>();
        //     var taskRepository = Substitute.For<ITaskRepository>();
        //
        //     userRepository.ExistsByIdAsync(user.Value.UserId).Returns(true);
        //     projectRepository.ExistsByIdAsync(project.Value.ProjectId).Returns(true);
        //     projectRepository.FindByIdAsync(project.Value.ProjectId).Returns(project);
        //
        //     var command = new CreateProjectTaskCommand
        //     {
        //         UserId = user.Value.UserId,
        //         Title = "Test",
        //         Description = "Test",
        //         Priority = TaskPriority.HIGH,
        //         ProjectId = Guid.NewGuid(),
        //     };
        //
        //     var commandHandler = new CreateProjectTaskCommandHandler(taskRepository, projectRepository, userRepository);
        //
        //     var source = new CancellationTokenSource();
        //
        //     var response = await commandHandler.Handle(command, source.Token);
        //
        //     Assert.False(response.Success);
        //     Assert.Null(response.Task);
        //     Assert.NotNull(response.ValidationErrors);
        // }
        //
        // [Fact]
        // public async void GivenInvalidTitle_WhenCreate_Then_FailureResponse()
        // {
        //     var userRepository = Substitute.For<IUserRepository>();
        //     var projectRepository = Substitute.For<IProjectRepository>();
        //     var taskRepository = Substitute.For<ITaskRepository>();
        //
        //     userRepository.ExistsByIdAsync(user.Value.UserId).Returns(true);
        //     projectRepository.ExistsByIdAsync(project.Value.ProjectId).Returns(true);
        //     projectRepository.FindByIdAsync(project.Value.ProjectId).Returns(project);
        //
        //     var command = new CreateProjectTaskCommand
        //     {
        //         UserId = user.Value.UserId,
        //         Title = "",
        //         Description = "Test",
        //         Priority = TaskPriority.HIGH,
        //         ProjectId = project.Value.ProjectId,
        //     };
        //
        //     var commandHandler = new CreateProjectTaskCommandHandler(taskRepository, projectRepository, userRepository);
        //
        //     var source = new CancellationTokenSource();
        //
        //     var response = await commandHandler.Handle(command, source.Token);
        //
        //     Assert.False(response.Success);
        //     Assert.Null(response.Task);
        //     Assert.NotNull(response.ValidationErrors);
        // }
        //
        // [Fact]
        // public async void GivenInvalidDescription_WhenCreate_Then_FailureResponse()
        // {
        //     var userRepository = Substitute.For<IUserRepository>();
        //     var projectRepository = Substitute.For<IProjectRepository>();
        //     var taskRepository = Substitute.For<ITaskRepository>();
        //
        //     userRepository.ExistsByIdAsync(user.Value.UserId).Returns(true);
        //     projectRepository.ExistsByIdAsync(project.Value.ProjectId).Returns(true);
        //     projectRepository.FindByIdAsync(project.Value.ProjectId).Returns(project);
        //
        //     var command = new CreateProjectTaskCommand
        //     {
        //         UserId = Guid.NewGuid(),
        //         Title = "Test",
        //         Description = "",
        //         Priority = TaskPriority.HIGH,
        //         ProjectId = project.Value.ProjectId,
        //     };
        //
        //     var commandHandler = new CreateProjectTaskCommandHandler(taskRepository, projectRepository, userRepository);
        //
        //     var source = new CancellationTokenSource();
        //
        //     var response = await commandHandler.Handle(command, source.Token);
        //
        //     Assert.False(response.Success);
        //     Assert.Null(response.Task);
        //     Assert.NotNull(response.ValidationErrors);
        // }
    }
}

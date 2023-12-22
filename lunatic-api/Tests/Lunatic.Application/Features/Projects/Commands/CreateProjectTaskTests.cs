using Lunatic.Application.Features.Projects.Commands.CreateProjectTask;
using Lunatic.Application.Persistence;
using Lunatic.Domain.Entities;
using Lunatic.Domain.Utils;
using NSubstitute;

namespace Tests.Lunatic.Application.Features.Projects.Commands
{
	public class CreateProjectTaskTests
	{
		private static Result<User> user = User.Create("firstName", "lastName", "email@email.com", "username", "password", Role.USER);
		private static Result<Team> team = Team.Create(user.Value.UserId, "teamName", "teamDescription");
		private static Result<Project> project = Project.Create(user.Value.UserId, team.Value.TeamId, "projectTitle", "projectDescription");

		[Fact]
		public async void GivenCreateProjectTaskCommand_WhenCreate_Then_SuccessResponse()
		{
			var userRepository = Substitute.For<IUserRepository>();
			var projectRepository = Substitute.For<IProjectRepository>();
			var taskRepository = Substitute.For<ITaskRepository>();

			userRepository.ExistsByIdAsync(user.Value.UserId).Returns(true);
			projectRepository.ExistsByIdAsync(project.Value.ProjectId).Returns(true);
			projectRepository.FindByIdAsync(project.Value.ProjectId).Returns(project);

			var command = new CreateProjectTaskCommand
			{
				UserId = user.Value.UserId,
				Title = "Test",
				Description = "Test",
				Priority = TaskPriority.HIGH,
				ProjectId = project.Value.ProjectId,
			};

			var commandHandler = new CreateProjectTaskCommandHandler(taskRepository, projectRepository, userRepository);

			var source = new CancellationTokenSource();

			var response = await commandHandler.Handle(command, source.Token);

			Assert.True(response.Success);
			Assert.NotNull(response.Task);
			Assert.Null(response.ValidationErrors);
			Assert.NotNull(taskRepository.FindByIdAsync(response.Task.TaskId));
		}

		[Fact]
		public async void GivenInvalidUserId_WhenCreate_Then_FailureResponse()
		{
			var userRepository = Substitute.For<IUserRepository>();
			var projectRepository = Substitute.For<IProjectRepository>();
			var taskRepository = Substitute.For<ITaskRepository>();

			userRepository.ExistsByIdAsync(user.Value.UserId).Returns(true);
			projectRepository.ExistsByIdAsync(project.Value.ProjectId).Returns(true);
			projectRepository.FindByIdAsync(project.Value.ProjectId).Returns(project);

			var command = new CreateProjectTaskCommand
			{
				UserId = Guid.NewGuid(),
				Title = "Test",
				Description = "Test",
				Priority = TaskPriority.HIGH,
				ProjectId = project.Value.ProjectId,
			};

			var commandHandler = new CreateProjectTaskCommandHandler(taskRepository, projectRepository, userRepository);

			var source = new CancellationTokenSource();

			var response = await commandHandler.Handle(command, source.Token);

			Assert.False(response.Success);
			Assert.Null(response.Task);
			Assert.NotNull(response.ValidationErrors);
		}

		[Fact]
		public async void GivenInvalidProjectId_WhenCreate_Then_FailureResponse()
		{
			var userRepository = Substitute.For<IUserRepository>();
			var projectRepository = Substitute.For<IProjectRepository>();
			var taskRepository = Substitute.For<ITaskRepository>();

			userRepository.ExistsByIdAsync(user.Value.UserId).Returns(true);
			projectRepository.ExistsByIdAsync(project.Value.ProjectId).Returns(true);
			projectRepository.FindByIdAsync(project.Value.ProjectId).Returns(project);

			var command = new CreateProjectTaskCommand
			{
				UserId = user.Value.UserId,
				Title = "Test",
				Description = "Test",
				Priority = TaskPriority.HIGH,
				ProjectId = Guid.NewGuid(),
			};

			var commandHandler = new CreateProjectTaskCommandHandler(taskRepository, projectRepository, userRepository);

			var source = new CancellationTokenSource();

			var response = await commandHandler.Handle(command, source.Token);

			Assert.False(response.Success);
			Assert.Null(response.Task);
			Assert.NotNull(response.ValidationErrors);
		}

		[Fact]
		public async void GivenInvalidTitle_WhenCreate_Then_FailureResponse()
		{
			var userRepository = Substitute.For<IUserRepository>();
			var projectRepository = Substitute.For<IProjectRepository>();
			var taskRepository = Substitute.For<ITaskRepository>();

			userRepository.ExistsByIdAsync(user.Value.UserId).Returns(true);
			projectRepository.ExistsByIdAsync(project.Value.ProjectId).Returns(true);
			projectRepository.FindByIdAsync(project.Value.ProjectId).Returns(project);

			var command = new CreateProjectTaskCommand
			{
				UserId = user.Value.UserId,
				Title = "",
				Description = "Test",
				Priority = TaskPriority.HIGH,
				ProjectId = project.Value.ProjectId,
			};

			var commandHandler = new CreateProjectTaskCommandHandler(taskRepository, projectRepository, userRepository);

			var source = new CancellationTokenSource();

			var response = await commandHandler.Handle(command, source.Token);

			Assert.False(response.Success);
			Assert.Null(response.Task);
			Assert.NotNull(response.ValidationErrors);
		}

		[Fact]
		public async void GivenInvalidDescription_WhenCreate_Then_FailureResponse()
		{
			var userRepository = Substitute.For<IUserRepository>();
			var projectRepository = Substitute.For<IProjectRepository>();
			var taskRepository = Substitute.For<ITaskRepository>();

			userRepository.ExistsByIdAsync(user.Value.UserId).Returns(true);
			projectRepository.ExistsByIdAsync(project.Value.ProjectId).Returns(true);
			projectRepository.FindByIdAsync(project.Value.ProjectId).Returns(project);

			var command = new CreateProjectTaskCommand
			{
				UserId = Guid.NewGuid(),
				Title = "Test",
				Description = "",
				Priority = TaskPriority.HIGH,
				ProjectId = project.Value.ProjectId,
			};

			var commandHandler = new CreateProjectTaskCommandHandler(taskRepository, projectRepository, userRepository);

			var source = new CancellationTokenSource();

			var response = await commandHandler.Handle(command, source.Token);

			Assert.False(response.Success);
			Assert.Null(response.Task);
			Assert.NotNull(response.ValidationErrors);
		}
	}
}

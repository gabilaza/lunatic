using Lunatic.Application.Features.Comments.Commands.CreateCommentEmote;
using Lunatic.Application.Persistence;
using Lunatic.Domain.Entities;
using Lunatic.Domain.Utils;
using NSubstitute;
using Task = Lunatic.Domain.Entities.Task;

namespace Tests.Lunatic.Application.Features.CommentEmotes.Commands
{
	public class CreateCommentEmoteTests
	{
		private static Result<User> user = User.Create("firstName", "lastName", "email@email.com", "username", "password", Role.USER);
		private static Result<Team> team = Team.Create(user.Value.UserId, "teamName", "teamDescription");
		private static Result<Project> project = Project.Create(user.Value.UserId, team.Value.TeamId, "projectTitle", "projectDescription");
		private static Result<Task> task = Task.Create(user.Value.UserId, project.Value.ProjectId, "taskTitle", "taskDescription", TaskPriority.HIGH);
		private static Result<Comment> comment = Comment.Create(user.Value.UserId, task.Value.TaskId, "taskContent");

		[Fact]
		public async void GivenCreateCommentEmoteCommand_WhenCreate_ThenSuccessResponse()
		{
			// given
			var userRepository = Substitute.For<IUserRepository>();
			var teamRepository = Substitute.For<ITeamRepository>();
			var projectRepository = Substitute.For<IProjectRepository>();
			var taskRepository = Substitute.For<ITaskRepository>();
			var commentRepository = Substitute.For<ICommentRepository>();
			var commentEmoteRepository = Substitute.For<ICommentEmoteRepository>();

            userRepository.ExistsByIdAsync(user.Value.UserId).Returns(true);
			commentRepository.ExistsByIdAsync(comment.Value.CommentId).Returns(true);
			commentRepository.FindByIdAsync(comment.Value.CommentId).Returns(comment);

			var command = new CreateCommentEmoteCommand
			{
				UserId = user.Value.UserId,
				CommentId = comment.Value.CommentId,
				Emote = Emote.CRY
			};

			var commandHandler = new CreateCommentEmoteCommandHandler(commentEmoteRepository, commentRepository, userRepository);
			var source = new CancellationTokenSource();

			// when
			var response = await commandHandler.Handle(command, source.Token);

			// then
			Assert.True(response.Success);
			Assert.NotNull(response.CommentEmote);
			Assert.Null(response.ValidationErrors);
			Assert.NotNull(commentEmoteRepository.FindByIdAsync(response.CommentEmote.CommentEmoteId));
		}
	}
}

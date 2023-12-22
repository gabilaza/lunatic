using Lunatic.Application.Features.Comments.Commands.DeleteCommentEmote;
using Lunatic.Application.Persistence;
using Lunatic.Domain.Entities;
using Lunatic.Domain.Utils;
using NSubstitute;

namespace Tests.Lunatic.Application.Features.CommentEmotes.Commands
{
	public class DeleteCommentEmoteTests
	{
		private static Result<Comment> comment = Comment.Create(Guid.NewGuid(), Guid.NewGuid(), "taskContent");
		private static Result<CommentEmote> commentEmote = CommentEmote.Create(Guid.NewGuid(), comment.Value.CommentId, Emote.CRY);

		[Fact]
		public async void GivenDeleteCommentEmoteCommand_WhenDelete_ThenSuccessResponse()
		{

			// given
			var commentRepository = Substitute.For<ICommentRepository>();
			var commentEmoteRepository = Substitute.For<ICommentEmoteRepository>();

			commentRepository.ExistsByIdAsync(comment.Value.CommentId).Returns(true);
			commentRepository.FindByIdAsync(comment.Value.CommentId).Returns(comment);
			comment.Value.EmoteIds.Add(commentEmote.Value.CommentEmoteId);
			commentEmoteRepository.ExistsByIdAsync(commentEmote.Value.CommentEmoteId).Returns(true);
			commentEmoteRepository.DeleteAsync(commentEmote.Value.CommentEmoteId).Returns(commentEmote);

			var command = new DeleteCommentEmoteCommand
			{
				CommentEmoteId = commentEmote.Value.CommentEmoteId,
				CommentId = comment.Value.CommentId,
			};

			var commandHandler = new DeleteCommentEmoteCommandHandler(commentRepository, commentEmoteRepository);

			var source = new CancellationTokenSource();

			// when
			var response = await commandHandler.Handle(command, source.Token);

			// then
			Assert.True(response.Success);
			Assert.Null(response.ValidationErrors);
		}
	}
}

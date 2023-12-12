using Lunatic.Application.Features.Comments.Commands.CreateCommentEmote;
using Lunatic.Application.Features.Comments.Commands.DeleteCommentEmote;
using Lunatic.Application.Features.Comments.Queries.GetAllEmotes;
using Microsoft.AspNetCore.Mvc;

namespace Lunatic.API.Controllers {
    public class CommentsController : ApiControllerBase {
        [HttpPost("{commentId}/emotes")]
        [Produces("application/json")]
        [ProducesResponseType<CreateCommentEmoteCommandResponse>(StatusCodes.Status201Created)]
        [ProducesResponseType<CreateCommentEmoteCommandResponse>(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateEmote(Guid commentId, CreateCommentEmoteCommand command) {
            if(commentId != command.CommentId) {
                return BadRequest(new CreateCommentEmoteCommandResponse {
                        Success = false,
                        ValidationErrors = new List<string> { "The comment Id Path and comment Id Body must be equal." }
                });
            }

            var result = await Mediator.Send(command);
            if(!result.Success) {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpDelete("{commentId}/emotes/{emoteId}")]
        [Produces("application/json")]
        [ProducesResponseType<DeleteCommentEmoteCommandResponse>(StatusCodes.Status200OK)]
        [ProducesResponseType<DeleteCommentEmoteCommandResponse>(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteEmote(Guid commentId, Guid emoteId) {
            var deleteCommentEmoteCommand = new DeleteCommentEmoteCommand() { 
                CommentId = commentId,
                CommentEmoteId = emoteId
            };
            var result = await Mediator.Send(deleteCommentEmoteCommand);
            if(!result.Success) {
                return NotFound(result);
            }
            return Ok(result);
        }

        [HttpGet("{commentId}/emotes")]
        [Produces("application/json")]
        [ProducesResponseType<GetAllCommentEmotesQueryResponse>(StatusCodes.Status200OK)]
        [ProducesResponseType<GetAllCommentEmotesQueryResponse>(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAllEmotes(Guid taskId) {
            var result = await Mediator.Send(new GetAllCommentEmotesQuery(taskId));
            if(!result.Success) {
                return NotFound(result);
            }
            return Ok(result);
        }
    }
}


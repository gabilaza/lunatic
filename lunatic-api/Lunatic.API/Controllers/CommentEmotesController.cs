using Lunatic.Application.Features.CommentEmotes.Commands.CreateCommentEmote;
using Lunatic.Application.Features.CommentEmotes.Commands.DeleteCommentEmote;
using Lunatic.Application.Features.CommentEmotes.Commands.UpdateCommentEmote;
using Lunatic.Application.Features.CommentEmotes.Queries.GetAll;
using Lunatic.Application.Features.CommentEmotes.Queries.GetById;
using Microsoft.AspNetCore.Mvc;

namespace Lunatic.API.Controllers {
    [Route("api/v1/comment/emotes")]
    public class CommentEmotesController : ApiControllerBase {
        [HttpPost]
        [Produces("application/json")]
        [ProducesResponseType<CreateCommentEmoteCommandResponse>(StatusCodes.Status201Created)]
        [ProducesResponseType<CreateCommentEmoteCommandResponse>(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create(CreateCommentEmoteCommand command) {
            var result = await Mediator.Send(command);
            if(!result.Success) {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpPut("{id}")]
        [Produces("application/json")]
        [ProducesResponseType<UpdateCommentEmoteCommandResponse>(StatusCodes.Status200OK)]
        [ProducesResponseType<UpdateCommentEmoteCommandResponse>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<UpdateCommentEmoteCommandResponse>(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(Guid id, UpdateCommentEmoteCommand command) {
            if(id != command.Id) {
                return BadRequest(new UpdateCommentEmoteCommandResponse {
                        Success = false,
                        ValidationErrors = new List<string> { "The Id Path and Id Body must be equal." }
                });
            }

            var existsResult = await Mediator.Send(new GetByIdCommentEmoteQuery(id));
            if(!existsResult.Success) {
                return NotFound(existsResult);
            }

            var result = await Mediator.Send(command);
            if(!result.Success) {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpDelete("{id}")]
        [Produces("application/json")]
        [ProducesResponseType<DeleteCommentEmoteCommandResponse>(StatusCodes.Status200OK)]
        [ProducesResponseType<DeleteCommentEmoteCommandResponse>(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(Guid id) {
            var deleteCommentEmoteCommand = new DeleteCommentEmoteCommand() { Id = id };
            var result = await Mediator.Send(deleteCommentEmoteCommand);
            if(!result.Success) {
                return NotFound(result);
            }
            return Ok(result);
        }

        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType<GetAllCommentEmotesQueryResponse>(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll() {
            var result = await Mediator.Send(new GetAllCommentEmotesQuery());
            return Ok(result);
        }

        [HttpGet("{id}")]
        [Produces("application/json")]
        [ProducesResponseType<GetByIdCommentEmoteQueryResponse>(StatusCodes.Status200OK)]
        [ProducesResponseType<GetByIdCommentEmoteQueryResponse>(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(Guid id) {
            var result = await Mediator.Send(new GetByIdCommentEmoteQuery(id));
            if(!result.Success) {
                return NotFound(result);
            }
            return Ok(result);
        }
    }
}

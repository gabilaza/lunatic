using Lunatic.Application.Features.Comments.Commands.CreateComment;
using Lunatic.Application.Features.Comments.Commands.DeleteComment;
using Lunatic.Application.Features.Comments.Commands.UpdateComment;
using Lunatic.Application.Features.Comments.Queries.GetAll;
using Lunatic.Application.Features.Comments.Queries.GetById;
using Lunatic.Application.Responses;
using Microsoft.AspNetCore.Mvc;

namespace Lunatic.API.Controllers {
    public class CommentsController : ApiControllerBase {
        [HttpPost]
        [ProducesResponseType<CreateCommentCommandResponse>(StatusCodes.Status201Created)]
        [ProducesResponseType<CreateCommentCommandResponse>(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create(CreateCommentCommand command) {
            var result = await Mediator.Send(command);
            if(!result.Success) {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpPut("{id}")]
        [ProducesResponseType<UpdateCommentCommandResponse>(StatusCodes.Status200OK)]
        [ProducesResponseType<UpdateCommentCommandResponse>(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(Guid id, UpdateCommentCommand command) {
            if(id != command.Id) {
                return BadRequest(new ResponseBase {
                        Success = false,
                        ValidationErrors = new List<string> { "The Id Path and Id Body must be equal." }
                });
            }

            var existsResult = await Mediator.Send(new GetByIdCommentQuery(id));
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
        [ProducesResponseType<DeleteCommentCommandResponse>(StatusCodes.Status200OK)]
        [ProducesResponseType<DeleteCommentCommandResponse>(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(Guid id) {
            var deleteCommentCommand = new DeleteCommentCommand() { Id = id };
            var result = await Mediator.Send(deleteCommentCommand);
            if(!result.Success) {
                return NotFound(result);
            }
            return Ok(result);
        }

        [HttpGet]
        [ProducesResponseType<GetAllCommentsQueryResponse>(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll() {
            var result = await Mediator.Send(new GetAllCommentsQuery());
            return Ok(result);
        }

        [HttpGet("{id}")]
        [ProducesResponseType<GetByIdCommentQueryResponse>(StatusCodes.Status200OK)]
        [ProducesResponseType<GetByIdCommentQueryResponse>(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(Guid id) {
            var result = await Mediator.Send(new GetByIdCommentQuery(id));
            if(!result.Success) {
                return NotFound(result);
            }
            return Ok(result);
        }
    }
}


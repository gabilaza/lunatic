using Lunatic.Application.Features.Tasks.Commands.CreateTaskComment;
using Lunatic.Application.Features.Tasks.Commands.UpdateTaskComment;
using Lunatic.Application.Features.Tasks.Commands.DeleteTaskComment;
using Lunatic.Application.Features.Tasks.Queries.GetAllComments;
using Lunatic.Application.Features.Tasks.Queries.GetByIdComment;
using Microsoft.AspNetCore.Mvc;

namespace Lunatic.API.Controllers {
    public class TasksController : ApiControllerBase {
        [HttpPost("{taskId}/comments")]
        [Produces("application/json")]
        [ProducesResponseType<CreateTaskCommentCommandResponse>(StatusCodes.Status201Created)]
        [ProducesResponseType<CreateTaskCommentCommandResponse>(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateComment(Guid taskId, CreateTaskCommentCommand command) {
            if(taskId != command.TaskId) {
                return BadRequest(new CreateTaskCommentCommandResponse {
                        Success = false,
                        ValidationErrors = new List<string> { "The task Id Path and task Id Body must be equal." }
                });
            }

            var result = await Mediator.Send(command);
            if(!result.Success) {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpPut("comments/{commentId}")]
        [Produces("application/json")]
        [ProducesResponseType<UpdateTaskCommentCommandResponse>(StatusCodes.Status200OK)]
        [ProducesResponseType<UpdateTaskCommentCommandResponse>(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateComment(Guid commentId, UpdateTaskCommentCommand command) {
            if(commentId != command.CommentId) {
                return BadRequest(new UpdateTaskCommentCommandResponse {
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

        [HttpDelete("{taskId}/comments/{commentId}")]
        [Produces("application/json")]
        [ProducesResponseType<DeleteTaskCommentCommandResponse>(StatusCodes.Status200OK)]
        [ProducesResponseType<DeleteTaskCommentCommandResponse>(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteComment(Guid taskId, Guid commentId) {
            var deleteTaskCommentCommand = new DeleteTaskCommentCommand() { 
                TaskId = taskId,
                CommentId = commentId
            };
            var result = await Mediator.Send(deleteTaskCommentCommand);
            if(!result.Success) {
                return NotFound(result);
            }
            return Ok(result);
        }

        [HttpGet("{taskId}/comments")]
        [Produces("application/json")]
        [ProducesResponseType<GetAllTaskCommentsQueryResponse>(StatusCodes.Status200OK)]
        [ProducesResponseType<GetAllTaskCommentsQueryResponse>(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAllComments(Guid taskId) {
            var result = await Mediator.Send(new GetAllTaskCommentsQuery(taskId));
            if(!result.Success) {
                return NotFound(result);
            }
            return Ok(result);
        }

        [HttpGet("comments/{commentId}")]
        [Produces("application/json")]
        [ProducesResponseType<GetByIdTaskCommentQueryResponse>(StatusCodes.Status200OK)]
        [ProducesResponseType<GetByIdTaskCommentQueryResponse>(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetByIdComment(Guid commentId) {
            var result = await Mediator.Send(new GetByIdTaskCommentQuery {
                    CommentId = commentId});
            if(!result.Success) {
                return NotFound(result);
            }
            return Ok(result);
        }
    }
}


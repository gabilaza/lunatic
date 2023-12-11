using Lunatic.Application.Features.Tasks.Commands.CreateTask;
using Lunatic.Application.Features.Tasks.Commands.DeleteTask;
using Lunatic.Application.Features.Tasks.Commands.UpdateTask;
using Lunatic.Application.Features.Tasks.Queries.GetAll;
using Lunatic.Application.Features.Tasks.Queries.GetById;
using Microsoft.AspNetCore.Mvc;

namespace Lunatic.API.Controllers {
    public class TasksController : ApiControllerBase {
        [HttpPost]
        [Produces("application/json")]
        [ProducesResponseType<CreateTaskCommandResponse>(StatusCodes.Status201Created)]
        [ProducesResponseType<CreateTaskCommandResponse>(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create(CreateTaskCommand command) {
            var result = await Mediator.Send(command);
            if(!result.Success) {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpPut("{taskId}")]
        [Produces("application/json")]
        [ProducesResponseType<UpdateTaskCommandResponse>(StatusCodes.Status200OK)]
        [ProducesResponseType<UpdateTaskCommandResponse>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<UpdateTaskCommandResponse>(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(Guid taskId, UpdateTaskCommand command) {
            if(taskId != command.TaskId) {
                return BadRequest(new UpdateTaskCommandResponse {
                        Success = false,
                        ValidationErrors = new List<string> { "The Id Path and Id Body must be equal." }
                });
            }

            var existsResult = await Mediator.Send(new GetByIdTaskQuery(taskId));
            if(!existsResult.Success) {
                return NotFound(existsResult);
            }

            var result = await Mediator.Send(command);
            if(!result.Success) {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpDelete("{taskId}")]
        [Produces("application/json")]
        [ProducesResponseType<DeleteTaskCommandResponse>(StatusCodes.Status200OK)]
        [ProducesResponseType<DeleteTaskCommandResponse>(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(Guid taskId) {
            var deleteTaskCommand = new DeleteTaskCommand() { TaskId = taskId };
            var result = await Mediator.Send(deleteTaskCommand);
            if(!result.Success) {
                return NotFound(result);
            }
            return Ok(result);
        }

        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType<GetAllTasksQueryResponse>(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll() {
            var result = await Mediator.Send(new GetAllTasksQuery());
            return Ok(result);
        }

        [HttpGet("{taskId}")]
        [Produces("application/json")]
        [ProducesResponseType<GetByIdTaskQueryResponse>(StatusCodes.Status200OK)]
        [ProducesResponseType<GetByIdTaskQueryResponse>(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(Guid taskId) {
            var result = await Mediator.Send(new GetByIdTaskQuery(taskId));
            if(!result.Success) {
                return NotFound(result);
            }
            return Ok(result);
        }
    }
}


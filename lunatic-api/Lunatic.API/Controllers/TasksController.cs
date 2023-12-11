using Lunatic.Application.Features.Tasks.Commands.CreateTask;
using Lunatic.Application.Features.Tasks.Commands.DeleteTask;
using Lunatic.Application.Features.Tasks.Commands.UpdateTask;
using Lunatic.Application.Features.Tasks.Queries.GetAll;
using Lunatic.Application.Features.Tasks.Queries.GetById;
using Lunatic.Application.Responses;
using Microsoft.AspNetCore.Mvc;

namespace Lunatic.API.Controllers {
    public class TasksController : ApiControllerBase {
        [HttpPost]
        [ProducesResponseType<CreateTaskCommandResponse>(StatusCodes.Status201Created)]
        [ProducesResponseType<CreateTaskCommandResponse>(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create(CreateTaskCommand command) {
            var result = await Mediator.Send(command);
            if(!result.Success) {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpPut("{id}")]
        [ProducesResponseType<UpdateTaskCommandResponse>(StatusCodes.Status200OK)]
        [ProducesResponseType<UpdateTaskCommandResponse>(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(Guid id, UpdateTaskCommand command) {
            if(id != command.Id) {
                return BadRequest(new ResponseBase {
                        Success = false,
                        ValidationErrors = new List<string> { "The Id Path and Id Body must be equal." }
                });
            }

            var existsResult = await Mediator.Send(new GetByIdTaskQuery(id));
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
        [ProducesResponseType<DeleteTaskCommandResponse>(StatusCodes.Status200OK)]
        [ProducesResponseType<DeleteTaskCommandResponse>(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(Guid id) {
            var deleteTaskCommand = new DeleteTaskCommand() { Id = id };
            var result = await Mediator.Send(deleteTaskCommand);
            if(!result.Success) {
                return NotFound(result);
            }
            return Ok(result);
        }

        [HttpGet]
        [ProducesResponseType<GetAllTasksQueryResponse>(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll() {
            var result = await Mediator.Send(new GetAllTasksQuery());
            return Ok(result);
        }

        [HttpGet("{id}")]
        [ProducesResponseType<GetByIdTaskQueryResponse>(StatusCodes.Status200OK)]
        [ProducesResponseType<GetByIdTaskQueryResponse>(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(Guid id) {
            var result = await Mediator.Send(new GetByIdTaskQuery(id));
            if(!result.Success) {
                return NotFound(result);
            }
            return Ok(result);
        }
    }
}


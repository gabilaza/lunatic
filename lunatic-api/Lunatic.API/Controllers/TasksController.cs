using Lunatic.Application.Features.Tasks.Commands.CreateTask;
using Lunatic.Application.Features.Tasks.Commands.DeleteTask;
using Lunatic.Application.Features.Tasks.Commands.UpdateTask;
using Lunatic.Application.Features.Tasks.Queries.GetAll;
using Lunatic.Application.Features.Tasks.Queries.GetById;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Lunatic.API.Controllers {
    public class TasksController : ApiControllerBase {
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> Create(CreateTaskComand command) {
            var result = await Mediator.Send(command);
            if (!result.Success) {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> Update(Guid id, UpdateTaskCommand command) {
            var existsResult = await Mediator.Send(new GetByIdTaskQuery(id));
            if (!existsResult.Success) {
                return NotFound(existsResult);
            }

            var result = await Mediator.Send(command);
            if (!result.Success) {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> Delete(Guid id) {
            var deleteTaskCommand = new DeleteTaskCommand() { Id = id };
            var result = await Mediator.Send(deleteTaskCommand);
            if (!result.Success) {
                return NotFound(result);
            }
            return NoContent();
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> GetAll() {
            var result = await Mediator.Send(new GetAllTasksQuery());
            return Ok(result);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> Get(Guid id) {
            var result = await Mediator.Send(new GetByIdTaskQuery(id));
            if (!result.Success) {
                return NotFound(result);
            }
            return Ok(result);
        }
    }
}


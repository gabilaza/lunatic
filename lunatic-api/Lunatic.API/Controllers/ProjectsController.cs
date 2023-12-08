using Lunatic.Application.Features.Projects.Commands.CreateProject;
using Lunatic.Application.Features.Projects.Commands.DeleteProject;
using Lunatic.Application.Features.Projects.Commands.UpdateProject;
using Lunatic.Application.Features.Projects.Queries.GetAll;
using Lunatic.Application.Features.Projects.Queries.GetById;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Lunatic.API.Controllers {
    public class ProjectsController : ApiControllerBase {
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> Create(CreateProjectCommand command) {
            var result = await Mediator.Send(command);
            if (!result.Success) {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Roles = "User")]

        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateProjectCommand command) {
            var existsResult = await Mediator.Send(new GetByIdProjectQuery(id));
            if (!existsResult.Success) {
                return BadRequest(existsResult);
            }

            var result = await Mediator.Send(command);
            if (!result.Success) {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> Delete(Guid id) {
            var deleteProjectCommand = new DeleteProjectCommand() { Id = id };
            var result = await Mediator.Send(deleteProjectCommand);
            if (!result.Success) {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> GetAll() {
            var result = await Mediator.Send(new GetAllProjectsQuery());
            return Ok(result);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> Get(Guid id) {
            var result = await Mediator.Send(new GetByIdProjectQuery(id));
            if (!result.Success) {
                return NotFound(result);
            }
            return Ok(result);
        }
    }
}


using Lunatic.Application.Features.Projects.Commands.CreateProject;
using Lunatic.Application.Features.Projects.Commands.DeleteProject;
using Lunatic.Application.Features.Projects.Commands.UpdateProject;
using Lunatic.Application.Features.Projects.Queries.GetAll;
using Lunatic.Application.Features.Projects.Queries.GetById;
using Microsoft.AspNetCore.Mvc;

namespace Lunatic.API.Controllers
{
    public class ProjectsController : ApiControllerBase
    {
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Create(CreateProjectCommand command)
        {
            var result = await Mediator.Send(command);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateProjectCommand command)
        {
            var existsResult = Mediator.Send(new GetByIdProjectQuery(id));
            if (existsResult.Result == null)
            {
                return BadRequest(existsResult);
            }

            var result = await Mediator.Send(command);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Delete(Guid id, [FromBody] DeleteProjectCommand command)
        {
            //var existsResult = Mediator.Send(new GetByIdProjectQuery(id));
            //if (existsResult.Result == null)
            //{
            //    return BadRequest(existsResult);
            //}

            var result = await Mediator.Send(command);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var result = await Mediator.Send(new GetAllProjectsQuery());
            return Ok(result);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Get(Guid id)
        {
            var result = await Mediator.Send(new GetByIdProjectQuery(id));
            return Ok(result);
        }
    }
}


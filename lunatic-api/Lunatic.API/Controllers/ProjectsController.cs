using Lunatic.Application.Features.Projects.Commands.CreateProject;
using Lunatic.Application.Features.Projects.Commands.DeleteProject;
using Lunatic.Application.Features.Projects.Commands.UpdateProject;
using Lunatic.Application.Features.Projects.Queries.GetAll;
using Lunatic.Application.Features.Projects.Queries.GetById;
using Microsoft.AspNetCore.Mvc;

namespace Lunatic.API.Controllers {
    public class ProjectsController : ApiControllerBase {
        [HttpPost]
        [Produces("application/json")]
        [ProducesResponseType<CreateProjectCommandResponse>(StatusCodes.Status201Created)]
        [ProducesResponseType<CreateProjectCommandResponse>(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create(CreateProjectCommand command) {
            var result = await Mediator.Send(command);
            if(!result.Success) {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpPut("{projectId}")]
        [Produces("application/json")]
        [ProducesResponseType<UpdateProjectCommandResponse>(StatusCodes.Status200OK)]
        [ProducesResponseType<UpdateProjectCommandResponse>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<UpdateProjectCommandResponse>(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(Guid projectId, UpdateProjectCommand command) {
            if(projectId != command.ProjectId) {
                return BadRequest(new UpdateProjectCommandResponse {
                        Success = false,
                        ValidationErrors = new List<string> { "The Id Path and Id Body must be equal." }
                });
            }

            var existsResult = await Mediator.Send(new GetByIdProjectQuery(projectId));
            if(!existsResult.Success) {
                return NotFound(existsResult);
            }

            var result = await Mediator.Send(command);
            if(!result.Success) {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpDelete("{projectId}")]
        [Produces("application/json")]
        [ProducesResponseType<DeleteProjectCommandResponse>(StatusCodes.Status200OK)]
        [ProducesResponseType<DeleteProjectCommandResponse>(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(Guid projectId) {
            var deleteProjectCommand = new DeleteProjectCommand() { ProjectId = projectId };
            var result = await Mediator.Send(deleteProjectCommand);
            if(!result.Success) {
                return NotFound(result);
            }
            return Ok(result);
        }

        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType<GetAllProjectsQueryResponse>(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll() {
            var result = await Mediator.Send(new GetAllProjectsQuery());
            return Ok(result);
        }

        [HttpGet("{projectId}")]
        [Produces("application/json")]
        [ProducesResponseType<GetByIdProjectQueryResponse>(StatusCodes.Status200OK)]
        [ProducesResponseType<GetByIdProjectQueryResponse>(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(Guid projectId) {
            var result = await Mediator.Send(new GetByIdProjectQuery(projectId));
            if(!result.Success) {
                return NotFound(result);
            }
            return Ok(result);
        }
    }
}


using Lunatic.Application.Features.Projects.Commands.CreateProjectTask;
using Lunatic.Application.Features.Projects.Commands.UpdateProjectTask;
using Lunatic.Application.Features.Projects.Commands.DeleteProjectTask;
using Lunatic.Application.Features.Projects.Queries.GetAllTasks;
using Lunatic.Application.Features.Projects.Queries.GetByIdTask;
using Microsoft.AspNetCore.Mvc;

namespace Lunatic.API.Controllers {
    public class ProjectsController : ApiControllerBase {
        [HttpPost("{projectId}/tasks")]
        [Produces("application/json")]
        [ProducesResponseType<CreateProjectTaskCommandResponse>(StatusCodes.Status201Created)]
        [ProducesResponseType<CreateProjectTaskCommandResponse>(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateTask(Guid projectId, CreateProjectTaskCommand command) {
            if(projectId != command.ProjectId) {
                return BadRequest(new CreateProjectTaskCommandResponse {
                        Success = false,
                        ValidationErrors = new List<string> { "The project Id Path and project Id Body must be equal." }
                });
            }

            var result = await Mediator.Send(command);
            if(!result.Success) {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpPut("{projectId}/tasks/{taskId}")]
        [Produces("application/json")]
        [ProducesResponseType<UpdateProjectTaskCommandResponse>(StatusCodes.Status200OK)]
        [ProducesResponseType<UpdateProjectTaskCommandResponse>(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateTask(Guid projectId, Guid taskId, UpdateProjectTaskCommand command) {
            if(projectId != command.ProjectId) {
                return BadRequest(new UpdateProjectTaskCommandResponse {
                        Success = false,
                        ValidationErrors = new List<string> { "The project Id Path and project Id Body must be equal." }
                });
            }
            if(taskId != command.TaskId) {
                return BadRequest(new UpdateProjectTaskCommandResponse {
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

        [HttpDelete("{projectId}/tasks/{taskId}")]
        [Produces("application/json")]
        [ProducesResponseType<DeleteProjectTaskCommandResponse>(StatusCodes.Status200OK)]
        [ProducesResponseType<DeleteProjectTaskCommandResponse>(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteTask(Guid projectId, Guid taskId) {
            var deleteProjectTaskCommand = new DeleteProjectTaskCommand() { 
                ProjectId = projectId,
                TaskId = taskId
            };
            var result = await Mediator.Send(deleteProjectTaskCommand);
            if(!result.Success) {
                return NotFound(result);
            }
            return Ok(result);
        }

        [HttpGet("{projectId}/projects")]
        [Produces("application/json")]
        [ProducesResponseType<GetAllProjectTasksQueryResponse>(StatusCodes.Status200OK)]
        [ProducesResponseType<GetAllProjectTasksQueryResponse>(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAllTasks(Guid projectId) {
            var result = await Mediator.Send(new GetAllProjectTasksQuery(projectId));
            if(!result.Success) {
                return NotFound(result);
            }
            return Ok(result);
        }

        [HttpGet("{projectId}/tasks/{taskId}")]
        [Produces("application/json")]
        [ProducesResponseType<GetByIdProjectTaskQueryResponse>(StatusCodes.Status200OK)]
        [ProducesResponseType<GetByIdProjectTaskQueryResponse>(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetByIdTask(Guid projectId, Guid taskId) {
            var result = await Mediator.Send(new GetByIdProjectTaskQuery {
                    ProjectId = projectId,
                    TaskId = taskId});
            if(!result.Success) {
                return NotFound(result);
            }
            return Ok(result);
        }
    }
}


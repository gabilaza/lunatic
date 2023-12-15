using Lunatic.Application.Features.Teams.Commands.AddTeamMember;
using Lunatic.Application.Features.Teams.Commands.CreateTeam;
using Lunatic.Application.Features.Teams.Commands.CreateTeamProject;
using Lunatic.Application.Features.Teams.Commands.DeleteTeam;
using Lunatic.Application.Features.Teams.Commands.DeleteTeamMember;
using Lunatic.Application.Features.Teams.Commands.DeleteTeamProject;
using Lunatic.Application.Features.Teams.Commands.UpdateTeam;
using Lunatic.Application.Features.Teams.Commands.UpdateTeamProject;
using Lunatic.Application.Features.Teams.Queries.GetAll;
using Lunatic.Application.Features.Teams.Queries.GetAllMembers;
using Lunatic.Application.Features.Teams.Queries.GetAllProjects;
using Lunatic.Application.Features.Teams.Queries.GetById;
using Lunatic.Application.Features.Teams.Queries.GetByIdProject;
using Microsoft.AspNetCore.Mvc;

namespace Lunatic.API.Controllers {
	public class TeamsController : ApiControllerBase {
		[HttpPost]
		[Produces("application/json")]
		[ProducesResponseType<CreateTeamCommandResponse>(StatusCodes.Status201Created)]
		[ProducesResponseType<CreateTeamCommandResponse>(StatusCodes.Status400BadRequest)]
		public async Task<IActionResult> Create(CreateTeamCommand command) {
			var result = await Mediator.Send(command);
			if (!result.Success) {
				return BadRequest(result);
			}
			return Ok(result);
		}

		[HttpPut("{teamId}")]
		[Produces("application/json")]
		[ProducesResponseType<UpdateTeamCommandResponse>(StatusCodes.Status200OK)]
		[ProducesResponseType<UpdateTeamCommandResponse>(StatusCodes.Status400BadRequest)]
		[ProducesResponseType<UpdateTeamCommandResponse>(StatusCodes.Status404NotFound)]
		public async Task<IActionResult> Update(Guid teamId, UpdateTeamCommand command) {
			if (teamId != command.TeamId) {
				return BadRequest(new UpdateTeamCommandResponse {
					Success = false,
					ValidationErrors = new List<string> { "The Id Path and Id Body must be equal." }
				});
			}

			var existsResult = await Mediator.Send(new GetByIdTeamQuery(teamId));
			if (!existsResult.Success) {
				return NotFound(existsResult);
			}

			var result = await Mediator.Send(command);
			if (!result.Success) {
				return BadRequest(result);
			}
			return Ok(result);
		}

		[HttpDelete("{teamId}")]
		[Produces("application/json")]
		[ProducesResponseType<DeleteTeamCommandResponse>(StatusCodes.Status200OK)]
		[ProducesResponseType<DeleteTeamCommandResponse>(StatusCodes.Status404NotFound)]
		public async Task<IActionResult> Delete(Guid teamId) {
			var deleteTeamCommand = new DeleteTeamCommand() { TeamId = teamId };
			var result = await Mediator.Send(deleteTeamCommand);
			if (!result.Success) {
				return NotFound(result);
			}
			return Ok(result);
		}

		[HttpGet]
		[Produces("application/json")]
		[ProducesResponseType<GetAllTeamsQueryResponse>(StatusCodes.Status200OK)]
		public async Task<IActionResult> GetAll() {
			var result = await Mediator.Send(new GetAllTeamsQuery());
			return Ok(result);
		}

		[HttpGet("{teamId}")]
		[Produces("application/json")]
		[ProducesResponseType<GetByIdTeamQueryResponse>(StatusCodes.Status200OK)]
		[ProducesResponseType<GetByIdTeamQueryResponse>(StatusCodes.Status404NotFound)]
		public async Task<IActionResult> Get(Guid teamId) {
			var result = await Mediator.Send(new GetByIdTeamQuery(teamId));
			if (!result.Success) {
				return NotFound(result);
			}
			return Ok(result.Team);
		}

		[HttpGet("{teamId}/members")]
		[Produces("application/json")]
		[ProducesResponseType<GetAllTeamMembersQueryResponse>(StatusCodes.Status200OK)]
		[ProducesResponseType<GetAllTeamMembersQueryResponse>(StatusCodes.Status404NotFound)]
		public async Task<IActionResult> GetAllMembers(Guid teamId) {
			var result = await Mediator.Send(new GetAllTeamMembersQuery(teamId));
			if (!result.Success) {
				return NotFound(result);
			}
			return Ok(result);
		}

		[HttpGet("{teamId}/projects")]
		[Produces("application/json")]
		[ProducesResponseType<GetAllTeamProjectsQueryResponse>(StatusCodes.Status200OK)]
		[ProducesResponseType<GetAllTeamProjectsQueryResponse>(StatusCodes.Status404NotFound)]
		public async Task<IActionResult> GetAllProjects(Guid teamId) {
			var result = await Mediator.Send(new GetAllTeamProjectsQuery(teamId));
			if (!result.Success) {
				return NotFound(result);
			}
			return Ok(result);
		}

		[HttpGet("projects/{projectId}")]
		[Produces("application/json")]
		[ProducesResponseType<GetByIdTeamProjectQueryResponse>(StatusCodes.Status200OK)]
		[ProducesResponseType<GetByIdTeamProjectQueryResponse>(StatusCodes.Status404NotFound)]
		public async Task<IActionResult> GetProject(Guid projectId) {
			var result = await Mediator.Send(new GetByIdTeamProjectQuery {
				ProjectId = projectId
			});
			if (!result.Success) {
				return NotFound(result);
			}
			return Ok(result.Project);
		}

		[HttpPost("{teamId}/projects")]
		[Produces("application/json")]
		[ProducesResponseType<CreateTeamProjectCommandResponse>(StatusCodes.Status201Created)]
		[ProducesResponseType<CreateTeamProjectCommandResponse>(StatusCodes.Status400BadRequest)]
		public async Task<IActionResult> CreateProject(Guid teamId, CreateTeamProjectCommand command) {
			if (teamId != command.TeamId) {
				return BadRequest(new CreateTeamProjectCommandResponse {
					Success = false,
					ValidationErrors = new List<string> { "The team Id Path and team Id Body must be equal." }
				});
			}

			var result = await Mediator.Send(command);
			if (!result.Success) {
				return BadRequest(result);
			}
			return Ok(result);
		}

		[HttpPut("{teamId}/projects/{projectId}")]
		[Produces("application/json")]
		[ProducesResponseType<UpdateTeamProjectCommandResponse>(StatusCodes.Status200OK)]
		[ProducesResponseType<UpdateTeamProjectCommandResponse>(StatusCodes.Status400BadRequest)]
		public async Task<IActionResult> UpdateProject(Guid teamId, Guid projectId, UpdateTeamProjectCommand command) {
			if (teamId != command.TeamId) {
				return BadRequest(new UpdateTeamProjectCommandResponse {
					Success = false,
					ValidationErrors = new List<string> { "The team Id Path and team Id Body must be equal." }
				});
			}
			if (projectId != command.ProjectId) {
				return BadRequest(new UpdateTeamProjectCommandResponse {
					Success = false,
					ValidationErrors = new List<string> { "The project Id Path and project Id Body must be equal." }
				});
			}

			var result = await Mediator.Send(command);
			if (!result.Success) {
				return BadRequest(result);
			}
			return Ok(result);
		}

		[HttpDelete("{teamId}/projects/{projectId}")]
		[Produces("application/json")]
		[ProducesResponseType<DeleteTeamProjectCommandResponse>(StatusCodes.Status200OK)]
		[ProducesResponseType<DeleteTeamProjectCommandResponse>(StatusCodes.Status404NotFound)]
		public async Task<IActionResult> DeleteProject(Guid teamId, Guid projectId) {
			var deleteTeamProjectCommand = new DeleteTeamProjectCommand() {
				TeamId = teamId,
				ProjectId = projectId
			};
			var result = await Mediator.Send(deleteTeamProjectCommand);
			if (!result.Success) {
				return NotFound(result);
			}
			return Ok(result);
		}

		[HttpPost("{teamId}/members")]
		[Produces("application/json")]
		[ProducesResponseType<AddTeamMemberCommandResponse>(StatusCodes.Status200OK)]
		[ProducesResponseType<AddTeamMemberCommandResponse>(StatusCodes.Status400BadRequest)]
		public async Task<IActionResult> AddMember(Guid teamId, AddTeamMemberCommand command) {
			if (teamId != command.TeamId) {
				return BadRequest(new AddTeamMemberCommandResponse {
					Success = false,
					ValidationErrors = new List<string> { "The team Id Path and team Id Body must be equal." }
				});
			}

			var result = await Mediator.Send(command);
			if (!result.Success) {
				return BadRequest(result);
			}
			return Ok(result);
		}

		[HttpPost("{teamId}/members/{userId}")]
		[Produces("application/json")]
		[ProducesResponseType<DeleteTeamMemberCommandResponse>(StatusCodes.Status200OK)]
		[ProducesResponseType<DeleteTeamMemberCommandResponse>(StatusCodes.Status400BadRequest)]
		public async Task<IActionResult> DeleteMember(Guid teamId, Guid userId, DeleteTeamMemberCommand command) {
			if (teamId != command.TeamId) {
				return BadRequest(new DeleteTeamMemberCommandResponse {
					Success = false,
					ValidationErrors = new List<string> { "The team Id Path and team Id Body must be equal." }
				});
			}
			if (userId != command.UserId) {
				return BadRequest(new DeleteTeamMemberCommandResponse {
					Success = false,
					ValidationErrors = new List<string> { "The user Id Path and user Id Body must be equal." }
				});
			}

			var result = await Mediator.Send(command);
			if (!result.Success) {
				return BadRequest(result);
			}
			return Ok(result);
		}
	}
}


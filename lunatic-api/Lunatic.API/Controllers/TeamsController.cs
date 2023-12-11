using Lunatic.Application.Features.Teams.Commands.CreateTeam;
using Lunatic.Application.Features.Teams.Commands.DeleteTeam;
using Lunatic.Application.Features.Teams.Commands.UpdateTeam;
using Lunatic.Application.Features.Teams.Queries.GetAll;
using Lunatic.Application.Features.Teams.Queries.GetById;
using Microsoft.AspNetCore.Mvc;

namespace Lunatic.API.Controllers {
    public class TeamsController : ApiControllerBase {
        [HttpPost]
        [Produces("application/json")]
        [ProducesResponseType<CreateTeamCommandResponse>(StatusCodes.Status201Created)]
        [ProducesResponseType<CreateTeamCommandResponse>(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create(CreateTeamCommand command) {
            var result = await Mediator.Send(command);
            if(!result.Success) {
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
            if(teamId != command.TeamId) {
                return BadRequest(new UpdateTeamCommandResponse {
                        Success = false,
                        ValidationErrors = new List<string> { "The Id Path and Id Body must be equal." }
                });
            }

            var existsResult = await Mediator.Send(new GetByIdTeamQuery(teamId));
            if(!existsResult.Success) {
                return NotFound(existsResult);
            }

            var result = await Mediator.Send(command);
            if(!result.Success) {
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
            if(!result.Success) {
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
            if(!result.Success) {
                return NotFound(result);
            }
            return Ok(result);
        }
    }
}


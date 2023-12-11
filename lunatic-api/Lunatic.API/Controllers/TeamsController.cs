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

        [HttpPut("{id}")]
        [Produces("application/json")]
        [ProducesResponseType<UpdateTeamCommandResponse>(StatusCodes.Status200OK)]
        [ProducesResponseType<UpdateTeamCommandResponse>(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(Guid id, UpdateTeamCommand command) {
            if(id != command.Id) {
                return BadRequest(new UpdateTeamCommandResponse {
                        Success = false,
                        ValidationErrors = new List<string> { "The Id Path and Id Body must be equal." }
                });
            }

            var existsResult = await Mediator.Send(new GetByIdTeamQuery(id));
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
        [Produces("application/json")]
        [ProducesResponseType<DeleteTeamCommandResponse>(StatusCodes.Status200OK)]
        [ProducesResponseType<DeleteTeamCommandResponse>(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(Guid id) {
            var deleteTeamCommand = new DeleteTeamCommand() { Id = id };
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

        [HttpGet("{id}")]
        [Produces("application/json")]
        [ProducesResponseType<GetByIdTeamQueryResponse>(StatusCodes.Status200OK)]
        [ProducesResponseType<GetByIdTeamQueryResponse>(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(Guid id) {
            var result = await Mediator.Send(new GetByIdTeamQuery(id));
            if(!result.Success) {
                return NotFound(result);
            }
            return Ok(result);
        }
    }
}


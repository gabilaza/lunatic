using Lunatic.Application.Features.Users.Commands.CreateUser;
using Lunatic.Application.Features.Users.Commands.DeleteUser;
using Lunatic.Application.Features.Users.Commands.UpdateUser;
using Lunatic.Application.Features.Users.Queries.GetAll;
using Lunatic.Application.Features.Users.Queries.GetById;
using Microsoft.AspNetCore.Mvc;

namespace Lunatic.API.Controllers {
    public class UsersController : ApiControllerBase {
        [HttpPost]
        [Produces("application/json")]
        [ProducesResponseType<CreateUserCommandResponse>(StatusCodes.Status201Created)]
        [ProducesResponseType<CreateUserCommandResponse>(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create(CreateUserCommand command) {
            var result = await Mediator.Send(command);
            if(!result.Success) {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpPut("{id}")]
        [Produces("application/json")]
        [ProducesResponseType<UpdateUserCommandResponse>(StatusCodes.Status200OK)]
        [ProducesResponseType<UpdateUserCommandResponse>(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(Guid id, UpdateUserCommand command) {
            if(id != command.Id) {
                return BadRequest(new UpdateUserCommandResponse {
                        Success = false,
                        ValidationErrors = new List<string> { "The Id Path and Id Body must be equal." }
                });
            }

            var existsResult = await Mediator.Send(new GetByIdUserQuery(id));
            if(!existsResult.Success) {
                return BadRequest(existsResult);
            }

            var result = await Mediator.Send(command);
            if(!result.Success) {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpDelete("{id}")]
        [Produces("application/json")]
        [ProducesResponseType<DeleteUserCommandResponse>(StatusCodes.Status200OK)]
        [ProducesResponseType<DeleteUserCommandResponse>(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(Guid id) {
            var deleteUserCommand = new DeleteUserCommand() { Id = id };
            var result = await Mediator.Send(deleteUserCommand);
            if(!result.Success) {
                return NotFound(result);
            }
            return Ok(result);
        }

        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType<GetAllUsersQueryResponse>(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll() {
            var result = await Mediator.Send(new GetAllUsersQuery());
            return Ok(result);
        }

        [HttpGet("{id}")]
        [Produces("application/json")]
        [ProducesResponseType<GetByIdUserQueryResponse>(StatusCodes.Status200OK)]
        [ProducesResponseType<GetByIdUserQueryResponse>(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(Guid id) {
            var result = await Mediator.Send(new GetByIdUserQuery(id));
            if(!result.Success) {
                return NotFound(result);
            }
            return Ok(result);
        }
    }
}


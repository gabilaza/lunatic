using Lunatic.Application.Features.Users.Commands.DeleteUser;
using Lunatic.Application.Features.Users.Commands.UpdateUser;
using Lunatic.Application.Features.Users.Queries.GetAll;
using Lunatic.Application.Features.Users.Queries.GetById;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace Lunatic.API.Controllers {
    public class UsersController : ApiControllerBase {

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Roles = "User")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateUserCommand command) {
            var existsResult = await Mediator.Send(new GetByIdUserQuery(id));
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
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Authorize(Roles = "User")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(Guid id) {
            var deleteUserCommand = new DeleteUserCommand() { Id = id};
            var result = await Mediator.Send(deleteUserCommand);
            if (!result.Success) {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> GetAll() {
            var result = await Mediator.Send(new GetAllUsersQuery());
            return Ok(result);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> Get(Guid id) {
            var result = await Mediator.Send(new GetByIdUserQuery(id));
            if(!result.Success) {
                return NotFound(result);
            }
            return Ok(result);
        }
    }
}


using Lunatic.Application.Features.Users.Commands.CreateUser;
using Lunatic.Application.Features.Users.Commands.UpdateUser;
using Lunatic.Application.Features.Users.Commands.DeleteUser;
using Lunatic.Application.Features.Users.Queries.GetAll;
using Lunatic.Application.Features.Users.Queries.GetById;
using Microsoft.AspNetCore.Mvc;

namespace Lunatic.API.Controllers {
    public class UsersController : ApiControllerBase {
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Create(CreateUserComand command) {
            var result = await Mediator.Send(command);
            if (!result.Success) {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Update(UpdateUserCommand command) {
            var result = await Mediator.Send(command);
            if (!result.Success) {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Delete(DeleteUserComand command) {
            var result = await Mediator.Send(command);
            if (!result.Success) {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll() {
            var result = await Mediator.Send(new GetAllUsersQuery());
            return Ok(result);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Get(Guid id) {
            var result = await Mediator.Send(new GetByIdUserQuery(id));
            return Ok(result);
        }
    }
}


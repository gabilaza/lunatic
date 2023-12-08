
using Lunatic.Domain.Entities;
using Lunatic.Domain.Models;
using MediatR;


namespace Lunatic.Application.Features.Users.Commands.UpdateUser
{
    public class UpdateUserCommand : IRequest<UpdateUserCommandResponse> {
        public string Id { get; set; }
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string Username { get; set; } = default!;
        public string Password { get; set; } = default!;
        public Role Role { get; set; }

        public List<Team>? Teams { get; set;}
    }
}

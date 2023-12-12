﻿
using MediatR;


namespace Lunatic.Application.Features.Teams.Commands.DeleteTeamMember {
    public class DeleteTeamMemberCommand : IRequest<DeleteTeamMemberCommandResponse> {
        public Guid UserId { get; set; } = default!;
        public Guid TeamId { get; set; } = default!;
    }
}

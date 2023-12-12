﻿
using Lunatic.Application.Responses;
using Lunatic.Application.Features.Teams.Payload;


namespace Lunatic.Application.Features.Teams.Commands.AddTeamMember {
    public class AddTeamMemberCommandResponse : ResponseBase {
        public AddTeamMemberCommandResponse() : base() { }

        public TeamDto Team { get; set; } = default!;
    }
}

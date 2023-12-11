﻿
using Lunatic.Application.Responses;
using Lunatic.Application.Features.Teams.Payload;


namespace Lunatic.Application.Features.Teams.Commands.UpdateTeam {
    public class UpdateTeamCommandResponse : ResponseBase {
        public UpdateTeamCommandResponse() : base() { }

        public TeamDto Team { get; set; } = default!;
    }
}

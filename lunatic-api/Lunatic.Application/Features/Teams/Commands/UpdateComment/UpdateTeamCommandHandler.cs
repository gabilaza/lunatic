﻿
using Lunatic.Application.Persistence;
using Lunatic.Application.Features.Teams.Payload;
using MediatR;


namespace Lunatic.Application.Features.Teams.Commands.UpdateTeam {
    public class UpdateTeamCommandHandler : IRequestHandler<UpdateTeamCommand, UpdateTeamCommandResponse> {
        private readonly ITeamRepository teamRepository;

        public UpdateTeamCommandHandler(ITeamRepository teamRepository) {
            this.teamRepository = teamRepository;
        }

        public async Task<UpdateTeamCommandResponse> Handle(UpdateTeamCommand request, CancellationToken cancellationToken) {
            var validator = new UpdateTeamCommandValidator(teamRepository);
            var validatorResult = await validator.ValidateAsync(request, cancellationToken);

            if(!validatorResult.IsValid) {
                return new UpdateTeamCommandResponse {
                    Success = false,
                    ValidationErrors = validatorResult.Errors.Select(e => e.ErrorMessage).ToList()
                };
            }

            var teamResult = await teamRepository.FindByIdAsync(request.Id);
            if(!teamResult.IsSuccess) {
                return new UpdateTeamCommandResponse {
                    Success = false,
                    ValidationErrors = new List<string> { "Team not found" }
                };
            }

            teamResult.Value.Update(request.Name);

            var dbTeam = await teamRepository.UpdateAsync(teamResult.Value);

            return new UpdateTeamCommandResponse {
                Success = true,
                Team = new TeamDto {
                    Id = dbTeam.Value.Id,

                    Name = dbTeam.Value.Name,

                    MemberIds = dbTeam.Value.MemberIds,
                    ProjectIds = dbTeam.Value.ProjectIds,
                }
            };
        }
    }
}

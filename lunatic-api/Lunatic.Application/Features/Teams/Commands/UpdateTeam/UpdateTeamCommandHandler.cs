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
            var validator = new UpdateTeamCommandValidator(this.teamRepository);
            var validatorResult = await validator.ValidateAsync(request, cancellationToken);

            if(!validatorResult.IsValid) {
                return new UpdateTeamCommandResponse {
                    Success = false,
                    ValidationErrors = validatorResult.Errors.Select(e => e.ErrorMessage).ToList()
                };
            }

            var teamResult = await this.teamRepository.FindByIdAsync(request.TeamId);
            if(!teamResult.IsSuccess) {
                return new UpdateTeamCommandResponse {
                    Success = false,
                    ValidationErrors = new List<string> { "Team not found" }
                };
            }

            teamResult.Value.Update(request.Name, request.Description);

            var dbTeamResult = await this.teamRepository.UpdateAsync(teamResult.Value);

            return new UpdateTeamCommandResponse {
                Success = true,
                Team = new TeamDto {
                    TeamId = dbTeamResult.Value.TeamId,

                    Name = dbTeamResult.Value.Name,
                    Description = dbTeamResult.Value.Description,

                    MemberIds = dbTeamResult.Value.MemberIds,
                    ProjectIds = dbTeamResult.Value.ProjectIds,
                }
            };
        }
    }
}

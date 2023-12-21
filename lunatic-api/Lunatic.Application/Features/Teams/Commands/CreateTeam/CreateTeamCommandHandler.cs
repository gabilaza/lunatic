
using Lunatic.Application.Persistence;
using Lunatic.Domain.Entities;
using Lunatic.Application.Features.Teams.Payload;
using MediatR;


namespace Lunatic.Application.Features.Teams.Commands.CreateTeam {
    public class CreateTeamCommandHandler : IRequestHandler<CreateTeamCommand, CreateTeamCommandResponse> {
        private readonly ITeamRepository teamRepository;

        private readonly IUserRepository userRepository;

        public CreateTeamCommandHandler(ITeamRepository teamRepository, IUserRepository userRepository) {
            this.teamRepository = teamRepository;
            this.userRepository = userRepository;
        }

        public async Task<CreateTeamCommandResponse> Handle(CreateTeamCommand request, CancellationToken cancellationToken) {
            var validator = new CreateTeamCommandValidator(this.userRepository);
            var validatorResult = await validator.ValidateAsync(request, cancellationToken);

            if(!validatorResult.IsValid) {
                return new CreateTeamCommandResponse {
                    Success = false,
                    ValidationErrors = validatorResult.Errors.Select(e => e.ErrorMessage).ToList()
                };
            }

            var team = Team.Create(request.UserId, request.Name, request.Description).Value;

            team.AddMember(request.UserId);
            await this.teamRepository.AddAsync(team);
            var user = (await this.userRepository.FindByIdAsync(request.UserId)).Value;
            user.AddTeam(team.TeamId);
            await this.userRepository.UpdateAsync(user);

            return new CreateTeamCommandResponse {
                Success = true,
                Team = new TeamDto {
                    TeamId = team.TeamId,

                    Name = team.Name,
                    Description = team.Description,

                    MemberIds = team.MemberIds,
                    ProjectIds = team.ProjectIds,
                }
            };
        }
    }
}

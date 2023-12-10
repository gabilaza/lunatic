
using Lunatic.Application.Persistence;
using Lunatic.Domain.Entities;
using Lunatic.Application.Features.Teams.Payload;
using MediatR;


namespace Lunatic.Application.Features.Teams.Commands.CreateTeam {
    public class CreateTeamCommandHandler : IRequestHandler<CreateTeamComand, CreateTeamCommandResponse> {
        private readonly ITeamRepository teamRepository;

        private readonly IUserRepository userRepository;

        public CreateTeamCommandHandler(ITeamRepository teamRepository, IUserRepository userRepository) {
            this.teamRepository = teamRepository;
            this.userRepository = userRepository;
        }

        public async Task<CreateTeamCommandResponse> Handle(CreateTeamComand request, CancellationToken cancellationToken) {
            var validator = new CreateTeamCommandValidator(teamRepository);
            var validatorResult = await validator.ValidateAsync(request, cancellationToken);

            if(!validatorResult.IsValid) {
                return new CreateTeamCommandResponse {
                    Success = false,
                    ValidationErrors = validatorResult.Errors.Select(e => e.ErrorMessage).ToList()
                };
            }

            var user = await this.userRepository.FindByIdAsync(request.UserId);
            if(!user.IsSuccess) {
                return new CreateTeamCommandResponse {
                    Success = false,
                    ValidationErrors = new List<string> { "User not found" }
                };
            }

            var team = Team.Create(user.Value.Id, request.Name);
            if(!team.IsSuccess) {
                return new CreateTeamCommandResponse {
                    Success = false,
                    ValidationErrors = new List<string> { team.Error }
                };
            }

            await teamRepository.AddAsync(team.Value);

            return new CreateTeamCommandResponse {
                Success = true,
                Team = new TeamDto {
                    Id = team.Value.Id,

                    Name = team.Value.Name,

                    MemberIds = team.Value.MemberIds,
                    ProjectIds = team.Value.ProjectIds,
                }
            };
        }
    }
}

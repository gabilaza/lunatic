
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

            var teamResult = Team.Create(request.UserId, request.Name);
            if(!teamResult.IsSuccess) {
                return new CreateTeamCommandResponse {
                    Success = false,
                    ValidationErrors = new List<string> { teamResult.Error }
                };
            }

            await this.teamRepository.AddAsync(teamResult.Value);

            return new CreateTeamCommandResponse {
                Success = true,
                Team = new TeamDto {
                    Id = teamResult.Value.Id,

                    Name = teamResult.Value.Name,

                    MemberIds = teamResult.Value.MemberIds,
                    ProjectIds = teamResult.Value.ProjectIds,
                }
            };
        }
    }
}

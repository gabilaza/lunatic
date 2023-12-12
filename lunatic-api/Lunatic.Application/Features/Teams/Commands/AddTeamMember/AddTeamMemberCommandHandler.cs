
using Lunatic.Application.Persistence;
using Lunatic.Application.Features.Teams.Payload;
using MediatR;


namespace Lunatic.Application.Features.Teams.Commands.AddTeamMember {
    public class AddTeamMemberCommandHandler : IRequestHandler<AddTeamMemberCommand, AddTeamMemberCommandResponse> {
        private readonly ITeamRepository teamRepository;

        private readonly IUserRepository userRepository;

        public AddTeamMemberCommandHandler(ITeamRepository teamRepository, IUserRepository userRepository) {
            this.teamRepository = teamRepository;
            this.userRepository = userRepository;
        }

        public async Task<AddTeamMemberCommandResponse> Handle(AddTeamMemberCommand request, CancellationToken cancellationToken) {
            var validator = new AddTeamMemberCommandValidator(this.userRepository, this.teamRepository);
            var validatorResult = await validator.ValidateAsync(request, cancellationToken);

            if(!validatorResult.IsValid) {
                return new AddTeamMemberCommandResponse {
                    Success = false,
                    ValidationErrors = validatorResult.Errors.Select(e => e.ErrorMessage).ToList()
                };
            }

            var team = (await this.teamRepository.FindByIdAsync(request.TeamId)).Value;
            team.AddMember(request.UserId);
            var dbTeamResult = await this.teamRepository.UpdateAsync(team);

            return new AddTeamMemberCommandResponse {
                Success = true,
                Team = new TeamDto {
                    TeamId = dbTeamResult.Value.TeamId,

                    Name = dbTeamResult.Value.Name,

                    MemberIds = dbTeamResult.Value.MemberIds,
                    ProjectIds = dbTeamResult.Value.ProjectIds,
                }
            };
        }
    }
}

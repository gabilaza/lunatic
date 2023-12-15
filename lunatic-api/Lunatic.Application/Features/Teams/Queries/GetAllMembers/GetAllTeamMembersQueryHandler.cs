
using Lunatic.Domain.Entities;
using Lunatic.Application.Persistence;
using Lunatic.Application.Features.Users.Payload;
using MediatR;


namespace Lunatic.Application.Features.Teams.Queries.GetAllMembers {
    public class GetAllTeamMembersQueryHandler : IRequestHandler<GetAllTeamMembersQuery, GetAllTeamMembersQueryResponse> {
        private readonly ITeamRepository teamRepository;

        private readonly IUserRepository userRepository;

        public GetAllTeamMembersQueryHandler(ITeamRepository teamRepository, IUserRepository userRepository) {
            this.teamRepository = teamRepository;
            this.userRepository = userRepository;
        }

        public async Task<GetAllTeamMembersQueryResponse> Handle(GetAllTeamMembersQuery request, CancellationToken cancellationToken) {
            var teamResult = await this.teamRepository.FindByIdAsync(request.TeamId);
            if(!teamResult.IsSuccess) {
                return new GetAllTeamMembersQueryResponse {
                    Success = false,
                    ValidationErrors = new List<string> { "TeamId must exists." }
                };
            }

            GetAllTeamMembersQueryResponse response = new GetAllTeamMembersQueryResponse();
            var memberIds = teamResult.Value.MemberIds;
            var members = new List<User>();
            foreach (var memberId in memberIds) {
                var member = (await this.userRepository.FindByIdAsync(memberId)).Value;
                members.Add(member);
            }

            response.Members = members.Select(member => new UserDto {
                UserId = member.UserId,

                FirstName = member.FirstName,
                LastName = member.LastName,
                Email = member.Email,
                Username = member.Username,
                Password = member.Password,
                Role = member.Role,

                TeamIds = member.TeamIds
            }).ToList();
            return response;
        }
    }
}

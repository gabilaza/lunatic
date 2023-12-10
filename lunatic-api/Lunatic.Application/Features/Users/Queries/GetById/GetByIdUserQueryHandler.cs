
using Lunatic.Application.Persistence;
using Lunatic.Application.Features.Users.Payload;
using MediatR;


namespace Lunatic.Application.Features.Users.Queries.GetById {
    public class GetByIdUserQueryHandler : IRequestHandler<GetByIdUserQuery, GetByIdUserQueryResponse> {
        private readonly IUserRepository userRepository;

        public GetByIdUserQueryHandler(IUserRepository userRepository) {
            this.userRepository = userRepository;
        }

        public async Task<GetByIdUserQueryResponse> Handle(GetByIdUserQuery request, CancellationToken cancellationToken) {
            var user = await userRepository.FindByIdAsync(request.Id);
            if(!user.IsSuccess) {
                return new GetByIdUserQueryResponse {
                    Success = false,
                    ValidationErrors = new List<string> { "User not found" }
                };
            }

            return new GetByIdUserQueryResponse { 
                Success = true,
                User = new UserDto {
                    Id = user.Value.Id,

                    FirstName = user.Value.FirstName,
                    LastName = user.Value.LastName,
                    Email = user.Value.Email,
                    Username = user.Value.Username,
                    Password = user.Value.Password,
                    Role = user.Value.Role,

                    TeamIds = user.Value.TeamIds
                }
            };
        }
    }
}


using Lunatic.Application.Persistence;
using Lunatic.Application.Features.Users.Payload;
using MediatR;


namespace Lunatic.Application.Features.Users.Queries.GetAll {
    public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, GetAllUsersQueryResponse> {
        private readonly IUserRepository userRepository;

        public GetAllUsersQueryHandler(IUserRepository userRepository) {
            this.userRepository = userRepository;
        }

        public async Task<GetAllUsersQueryResponse> Handle(GetAllUsersQuery request, CancellationToken cancellationToken) {
            GetAllUsersQueryResponse response = new GetAllUsersQueryResponse();
            var users = await userRepository.GetAllAsync();

            if(users.IsSuccess) {
                response.Users = users.Value.Select(u => new UserDto {
                    Id = u.Id,

                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Email = u.Email,
                    Username = u.Username,
                    Password = u.Password,
                    Role = u.Role,

                    TeamIds = u.TeamIds
                }).ToList();
            }
            return response;
        }
    }
}

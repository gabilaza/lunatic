
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
            var users = await this.userRepository.GetAllAsync();

            if(users.IsSuccess) {
                var usersFiltered = from user in users.Value select user;

                if(!string.IsNullOrWhiteSpace(request.Username)) {
                    usersFiltered = from user in usersFiltered where user.Username.StartsWith(request.Username) select user;
                }

                response.Users = usersFiltered.Select(user => new UserDto {
                    UserId = user.UserId,

                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    Username = user.Username,
                    Password = user.Password,
                    Role = user.Role,

                    TeamIds = user.TeamIds
                }).ToList();
            }
            return response;
        }
    }
}

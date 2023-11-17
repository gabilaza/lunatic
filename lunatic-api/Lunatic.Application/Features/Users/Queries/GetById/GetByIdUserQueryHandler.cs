
using Lunatic.Application.Persistence;
using MediatR;


namespace Lunatic.Application.Features.Users.Queries.GetById {
    public class GetByIdUserQueryHandler : IRequestHandler<GetByIdUserQuery, UserDto> {

        private readonly IUserRepository userRepository;

        public GetByIdUserQueryHandler(IUserRepository userRepository) {
            this.userRepository = userRepository;
        }

        public async Task<UserDto> Handle(GetByIdUserQuery request, CancellationToken cancellationToken) {

            var user = await userRepository.FindByIdAsync(request.Id);

            if (user.IsSuccess) {
                return new UserDto {
                    Id = user.Value.Id,
                    FirstName = user.Value.FirstName,
                    LastName = user.Value.LastName,
                    Username = user.Value.Username,
                    Password = user.Value.Password,
                    Role = user.Value.Role,
                    Teams = user.Value.Teams
                };
            }
            return new UserDto();
        }
    }
}

//
// using Lunatic.Application.Persistence;
// using MediatR;
//
//
// namespace Lunatic.Application.Features.Users.Queries.GetById {
//     public class GetByIdUserQueryHandler : IRequestHandler<GetByIdUserQuery, GetByIdUserResponse> {
//         private readonly IUserRepository userRepository;
//
//         public GetByIdUserQueryHandler(IUserRepository userRepository) {
//             this.userRepository = userRepository;
//         }
//
//         public async Task<GetByIdUserResponse> Handle(GetByIdUserQuery request, CancellationToken cancellationToken) {
//             var user = await userRepository.FindByIdAsync(request.Id);
//             if(!user.IsSuccess) {
//                 return new GetByIdUserResponse {
//                     Success = false,
//                     ValidationErrors = new List<string> { "User not found" }
//                 };
//             }
//
//             return new GetByIdUserResponse {
//                 Success = true,
//                 User = new UserDto {
//                     Id = new Guid(user.Value.Id),
//                     FirstName = user.Value.FirstName,
//                     LastName = user.Value.LastName,
//                     Email = user.Value.Email,
//                     Username = user.Value.UserName,
//                     Password = user.Value.PasswordHash,
//                     Role = user.Value.Role,
//                     Teams = user.Value.Teams
//                 }
//             };
//         }
//     }
// }
//

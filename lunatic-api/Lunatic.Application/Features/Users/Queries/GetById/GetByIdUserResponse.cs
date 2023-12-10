
using Lunatic.Application.Responses;
using Lunatic.Application.Features.Users.Payload;


namespace Lunatic.Application.Features.Users.Queries.GetById {
    public class GetByIdUserResponse : ResponseBase {
        public GetByIdUserResponse() : base() {}

        public UserDto User { get; set; } = default!;
    }
}


using Lunatic.Application.Responses;


namespace Lunatic.Application.Features.Users.Queries.GetById {
    public class GetByIdUserResponse : ResponseBase {
        public GetByIdUserResponse() : base() {}

        public UserDto User { get; set; } = default!;
    }
}


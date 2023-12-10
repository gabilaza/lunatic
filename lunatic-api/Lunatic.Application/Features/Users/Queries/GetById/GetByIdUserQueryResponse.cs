
using Lunatic.Application.Responses;
using Lunatic.Application.Features.Users.Payload;


namespace Lunatic.Application.Features.Users.Queries.GetById {
    public class GetByIdUserQueryResponse : ResponseBase {
        public GetByIdUserQueryResponse() : base() {}

        public UserDto User { get; set; } = default!;
    }
}


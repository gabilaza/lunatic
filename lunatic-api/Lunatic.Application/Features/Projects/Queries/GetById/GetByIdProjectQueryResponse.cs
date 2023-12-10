
using Lunatic.Application.Responses;
using Lunatic.Application.Features.Projects.Payload;


namespace Lunatic.Application.Features.Projects.Queries.GetById {
    public class GetByIdProjectQueryResponse : ResponseBase {
        public GetByIdProjectQueryResponse() : base() {}

        public ProjectDto Project { get; set; } = default!;
    }
}


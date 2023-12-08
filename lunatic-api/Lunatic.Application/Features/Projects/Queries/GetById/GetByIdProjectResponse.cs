using Lunatic.Application.Responses;


namespace Lunatic.Application.Features.Projects.Queries.GetById {
    public class GetByIdProjectResponse : ResponseBase {
        public GetByIdProjectResponse() : base() {}

        public ProjectDto Project { get; set; } = default!;
    }
}


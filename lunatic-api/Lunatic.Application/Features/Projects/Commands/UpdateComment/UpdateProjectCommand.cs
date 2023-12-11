
using MediatR;


namespace Lunatic.Application.Features.Projects.Commands.UpdateProject {
    public class UpdateProjectCommand : IRequest<UpdateProjectCommandResponse> {
        public Guid Id { get; set; } = default!;

        public string Title { get; set; } = default!;
        public string Description { get; set; } = default!;
    }
}

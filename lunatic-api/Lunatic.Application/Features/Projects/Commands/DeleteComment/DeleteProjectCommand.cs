
using MediatR;


namespace Lunatic.Application.Features.Projects.Commands.DeleteProject {
    public class DeleteProjectCommand : IRequest<DeleteProjectCommandResponse> {
        public Guid ProjectId { get; set; }
    }
}


using MediatR;


namespace Lunatic.Application.Features.Projects.Commands.DeleteProjectTask {
    public class DeleteProjectTaskCommand : IRequest<DeleteProjectTaskCommandResponse> {
        public Guid ProjectId { get; set; }
        public Guid TaskId { get; set; }
    }
}

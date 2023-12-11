
using MediatR;


namespace Lunatic.Application.Features.Tasks.Commands.DeleteTask {
    public class DeleteTaskCommand : IRequest<DeleteTaskCommandResponse> {
        public Guid TaskId { get; set; }
    }
}


using MediatR;


namespace Lunatic.Application.Features.Tasks.Commands.DeleteTask {
    public class DeleteTaskCommand : IRequest<DeleteTaskCommandResponse> {
        public Guid Id { get; set; }
        //public Guid ProjectId { get; set; } //needs retrinking
    }
}

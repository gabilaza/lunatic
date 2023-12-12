
using Lunatic.Domain.Entities;
using MediatR;
using TaskStatus = Lunatic.Domain.Entities.TaskStatus;


namespace Lunatic.Application.Features.Projects.Commands.UpdateProjectTask {
    public class UpdateProjectTaskCommand : IRequest<UpdateProjectTaskCommandResponse> {
        public Guid TaskId { get; set; } = default!;
        public Guid ProjectId { get; set; } = default!;

        public string Title { get; set; } = default!;
        public string Description { get; set; } = default!;
        public TaskPriority Priority { get; set; } = default!;
        public TaskStatus Status { get; set; } = default!;
    }
}


using Lunatic.Domain.Entities;
using MediatR;


namespace Lunatic.Application.Features.Tasks.Commands.CreateTask {
    public class CreateTaskComand : IRequest<CreateTaskCommandResponse> {
        public Guid UserId { get; set; } = default!;
        public string Title { get; set; } = default!;
        public string Description { get; set; } = default!;
        public TaskPriority Priority { get; set; } = default!;
    }
}

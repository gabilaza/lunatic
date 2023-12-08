using Lunatic.Domain.Entities;
using MediatR;


namespace Lunatic.Application.Features.Projects.Commands.CreateProject {
    public class CreateProjectCommand : IRequest<CreateProjectCommandResponse> {
        public Guid CreatedByUserId { get; set; } = default!;
        public string Title { get; set; } = default!;
        public string Description { get; set; } = default!;
    }
}

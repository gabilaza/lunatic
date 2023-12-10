
using Lunatic.Domain.Entities;
using MediatR;


namespace Lunatic.Application.Features.Projects.Commands.CreateProject {
    public class CreateProjectComand : IRequest<CreateProjectCommandResponse> {
        public Guid UserId { get; set; } = default!;
        public Guid TeamId { get; set; } = default!;

        public string Title { get; set; } = default!;
        public string Description { get; set; } = default!;
    }
}

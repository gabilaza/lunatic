
using Lunatic.Application.Persistence;
using MediatR;


namespace Lunatic.Application.Features.Projects.Commands.DeleteProject {
    public class DeleteProjectCommandHandler : IRequestHandler<DeleteProjectCommand, DeleteProjectCommandResponse> {
        private readonly IProjectRepository projectRepository;

        public DeleteProjectCommandHandler(IProjectRepository projectRepository) {
            this.projectRepository = projectRepository;
        }

        public async Task<DeleteProjectCommandResponse> Handle(DeleteProjectCommand request, CancellationToken cancellationToken) {
            var result = await projectRepository.FindByIdAsync(request.Id);

            if(!result.IsSuccess) {
                return new DeleteProjectCommandResponse {
                    Success = false,
                    ValidationErrors = new List<string> { result.Error }
                };
            }

            result = await projectRepository.DeleteAsync(request.Id);

            if(!result.IsSuccess) {
                return new DeleteProjectCommandResponse {
                    Success = false,
                    ValidationErrors = new List<string> { result.Error }
                };
            }

            return new DeleteProjectCommandResponse {
                Success = true
            };
        }
    }
}

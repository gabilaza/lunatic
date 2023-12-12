
using Lunatic.Application.Persistence;
using MediatR;


namespace Lunatic.Application.Features.Projects.Commands.DeleteProjectTask {
    public class DeleteProjectTaskCommandHandler : IRequestHandler<DeleteProjectTaskCommand, DeleteProjectTaskCommandResponse> {
        private readonly IProjectRepository projectRepository;

        private readonly ITaskRepository taskRepository;

        public DeleteProjectTaskCommandHandler(IProjectRepository projectRepository, ITaskRepository taskRepository) {
            this.projectRepository = projectRepository;
            this.taskRepository = taskRepository;
        }

        public async Task<DeleteProjectTaskCommandResponse> Handle(DeleteProjectTaskCommand request, CancellationToken cancellationToken) {
            var validator = new DeleteProjectTaskCommandValidator(this.projectRepository, this.taskRepository);
            var validatorResult = await validator.ValidateAsync(request, cancellationToken);

            if(!validatorResult.IsValid) {
                return new DeleteProjectTaskCommandResponse {
                    Success = false,
                    ValidationErrors = validatorResult.Errors.Select(e => e.ErrorMessage).ToList()
                };
            }

            var project = (await this.projectRepository.FindByIdAsync(request.ProjectId)).Value;
            project.RemoveTask(request.ProjectId);
            await this.projectRepository.UpdateAsync(project);

            var result = await this.taskRepository.DeleteAsync(request.TaskId);

            if(!result.IsSuccess) {
                return new DeleteProjectTaskCommandResponse {
                    Success = false,
                    ValidationErrors = new List<string> { result.Error }
                };

            }
            return new DeleteProjectTaskCommandResponse {
                Success = true
            };
        }
    }
}

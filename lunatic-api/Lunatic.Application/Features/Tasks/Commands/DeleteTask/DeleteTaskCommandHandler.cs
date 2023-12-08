
using Lunatic.Application.Persistence;
using MediatR;


namespace Lunatic.Application.Features.Tasks.Commands.DeleteTask {
    public class DeleteTaskCommandHandler : IRequestHandler<DeleteTaskCommand, DeleteTaskCommandResponse> {
        private readonly ITaskRepository taskRepository;

        public DeleteTaskCommandHandler(ITaskRepository taskRepository) {
            this.taskRepository = taskRepository;
        }

        public async Task<DeleteTaskCommandResponse> Handle(DeleteTaskCommand request, CancellationToken cancellationToken) {
            var result = await taskRepository.DeleteAsync(request.Id);

            if(!result.IsSuccess) {
                return new DeleteTaskCommandResponse {
                    Success = false,
                    ValidationErrors = new List<string> { result.Error }
                };

            }
            return new DeleteTaskCommandResponse {
                Success = true
            };
        }
    }
}

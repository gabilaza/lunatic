
using Lunatic.Application.Persistence;
using MediatR;


namespace Lunatic.Application.Features.Tasks.Commands.DeleteTaskComment {
    public class DeleteTaskCommentCommandHandler : IRequestHandler<DeleteTaskCommentCommand, DeleteTaskCommentCommandResponse> {
        private readonly ITaskRepository taskRepository;

        private readonly ICommentRepository commentRepository;

        public DeleteTaskCommentCommandHandler(ITaskRepository taskRepository, ICommentRepository commentRepository) {
            this.taskRepository = taskRepository;
            this.commentRepository = commentRepository;
        }

        public async Task<DeleteTaskCommentCommandResponse> Handle(DeleteTaskCommentCommand request, CancellationToken cancellationToken) {
            var validator = new DeleteTaskCommentCommandValidator(this.taskRepository, this.commentRepository);
            var validatorResult = await validator.ValidateAsync(request, cancellationToken);

            if(!validatorResult.IsValid) {
                return new DeleteTaskCommentCommandResponse {
                    Success = false,
                    ValidationErrors = validatorResult.Errors.Select(e => e.ErrorMessage).ToList()
                };
            }

            var task = (await this.taskRepository.FindByIdAsync(request.TaskId)).Value;
            task.RemoveComment(request.CommentId);
            await this.taskRepository.UpdateAsync(task);

            var result = await this.commentRepository.DeleteAsync(request.CommentId);

            if(!result.IsSuccess) {
                return new DeleteTaskCommentCommandResponse {
                    Success = false,
                    ValidationErrors = new List<string> { result.Error }
                };

            }
            return new DeleteTaskCommentCommandResponse {
                Success = true
            };
        }
    }
}

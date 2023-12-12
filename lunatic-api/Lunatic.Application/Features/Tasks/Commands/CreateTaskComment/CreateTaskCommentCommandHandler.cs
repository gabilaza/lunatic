
using Lunatic.Application.Persistence;
using Lunatic.Domain.Entities;
using Lunatic.Application.Features.Comments.Payload;
using MediatR;


namespace Lunatic.Application.Features.Tasks.Commands.CreateTaskComment {
    public class CreateTaskCommentCommandHandler : IRequestHandler<CreateTaskCommentCommand, CreateTaskCommentCommandResponse> {
        private readonly ITaskRepository taskRepository;

        private readonly ICommentRepository commentRepository;

        private readonly IUserRepository userRepository;

        public CreateTaskCommentCommandHandler(ITaskRepository taskRepository, ICommentRepository commentRepository, IUserRepository userRepository) {
            this.taskRepository = taskRepository;
            this.commentRepository = commentRepository;
            this.userRepository = userRepository;
        }

        public async Task<CreateTaskCommentCommandResponse> Handle(CreateTaskCommentCommand request, CancellationToken cancellationToken) {
            var validator = new CreateTaskCommentCommandValidator(this.userRepository, this.taskRepository);
            var validatorResult = await validator.ValidateAsync(request, cancellationToken);

            if(!validatorResult.IsValid) {
                return new CreateTaskCommentCommandResponse {
                    Success = false,
                    ValidationErrors = validatorResult.Errors.Select(e => e.ErrorMessage).ToList()
                };
            }

            var commentResult = Comment.Create(request.UserId, request.TaskId, request.Content);
            if(!commentResult.IsSuccess) {
                return new CreateTaskCommentCommandResponse {
                    Success = false,
                    ValidationErrors = new List<string> { commentResult.Error }
                };
            }

            var task = (await this.taskRepository.FindByIdAsync(request.TaskId)).Value;
            task.AddComment(commentResult.Value);
            await this.taskRepository.UpdateAsync(task);

            await this.commentRepository.AddAsync(commentResult.Value);

            return new CreateTaskCommentCommandResponse {
                Success = true,
                Comment = new CommentDto {
                    CommentId = commentResult.Value.CommentId,
                    TaskId = commentResult.Value.TaskId,

                    Content = commentResult.Value.Content,

                    EmoteIds = commentResult.Value.EmoteIds,
                }
            };
        }
    }
}

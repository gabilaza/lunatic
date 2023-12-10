
using Lunatic.Application.Persistence;
using Lunatic.Domain.Entities;
using Lunatic.Application.Features.Comments.Payload;
using MediatR;


namespace Lunatic.Application.Features.Comments.Commands.CreateComment {
    public class CreateCommentCommandHandler : IRequestHandler<CreateCommentComand, CreateCommentCommandResponse> {
        private readonly ICommentRepository commentRepository;

        private readonly ITaskRepository taskRepository;

        private readonly IUserRepository userRepository;

        public CreateCommentCommandHandler(ICommentRepository commentRepository, ITaskRepository taskRepository, IUserRepository userRepository) {
            this.commentRepository = commentRepository;
            this.taskRepository = taskRepository;
            this.userRepository = userRepository;
        }

        public async Task<CreateCommentCommandResponse> Handle(CreateCommentComand request, CancellationToken cancellationToken) {
            var validator = new CreateCommentCommandValidator(commentRepository);
            var validatorResult = await validator.ValidateAsync(request, cancellationToken);

            if(!validatorResult.IsValid) {
                return new CreateCommentCommandResponse {
                    Success = false,
                    ValidationErrors = validatorResult.Errors.Select(e => e.ErrorMessage).ToList()
                };
            }

            var task = await this.taskRepository.FindByIdAsync(request.TaskId);
            if(!task.IsSuccess) {
                return new CreateCommentCommandResponse {
                    Success = false,
                    ValidationErrors = new List<string> { "Task not found" }
                };
            }

            var user = await this.userRepository.FindByIdAsync(request.UserId);
            if(!user.IsSuccess) {
                return new CreateCommentCommandResponse {
                    Success = false,
                    ValidationErrors = new List<string> { "User not found" }
                };
            }

            var comment = Comment.Create(user.Value.Id, task.Value.Id, request.Content);
            if(!comment.IsSuccess) {
                return new CreateCommentCommandResponse {
                    Success = false,
                    ValidationErrors = new List<string> { comment.Error }
                };
            }

            await commentRepository.AddAsync(comment.Value);

            return new CreateCommentCommandResponse {
                Success = true,
                Comment = new CommentDto {
                    Id = comment.Value.Id,
                    TaskId = comment.Value.TaskId,

                    Content = comment.Value.Content,

                    EmoteIds = comment.Value.EmoteIds,
                }
            };
        }
    }
}

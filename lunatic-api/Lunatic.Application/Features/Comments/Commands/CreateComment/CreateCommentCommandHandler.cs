
using Lunatic.Application.Persistence;
using Lunatic.Domain.Entities;
using Lunatic.Application.Features.Comments.Payload;
using MediatR;


namespace Lunatic.Application.Features.Comments.Commands.CreateComment {
    public class CreateCommentCommandHandler : IRequestHandler<CreateCommentCommand, CreateCommentCommandResponse> {
        private readonly ICommentRepository commentRepository;

        private readonly ITaskRepository taskRepository;

        private readonly IUserRepository userRepository;

        public CreateCommentCommandHandler(ICommentRepository commentRepository, ITaskRepository taskRepository, IUserRepository userRepository) {
            this.commentRepository = commentRepository;
            this.taskRepository = taskRepository;
            this.userRepository = userRepository;
        }

        public async Task<CreateCommentCommandResponse> Handle(CreateCommentCommand request, CancellationToken cancellationToken) {
            var validator = new CreateCommentCommandValidator(this.userRepository, this.taskRepository);
            var validatorResult = await validator.ValidateAsync(request, cancellationToken);

            if(!validatorResult.IsValid) {
                return new CreateCommentCommandResponse {
                    Success = false,
                    ValidationErrors = validatorResult.Errors.Select(e => e.ErrorMessage).ToList()
                };
            }

            var commentResult = Comment.Create(request.UserId, request.TaskId, request.Content);
            if(!commentResult.IsSuccess) {
                return new CreateCommentCommandResponse {
                    Success = false,
                    ValidationErrors = new List<string> { commentResult.Error }
                };
            }

            await this.commentRepository.AddAsync(commentResult.Value);

            return new CreateCommentCommandResponse {
                Success = true,
                Comment = new CommentDto {
                    Id = commentResult.Value.Id,
                    TaskId = commentResult.Value.TaskId,

                    Content = commentResult.Value.Content,

                    EmoteIds = commentResult.Value.EmoteIds,
                }
            };
        }
    }
}

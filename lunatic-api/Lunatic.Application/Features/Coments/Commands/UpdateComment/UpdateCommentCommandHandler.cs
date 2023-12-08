
using Lunatic.Application.Persistence;
using MediatR;

namespace Lunatic.Application.Features.Coments.Commands.UpdateComment
{
    public class UpdateCommentCommandHandler : IRequestHandler<UpdateCommentCommand, UpdateCommentCommandResponse>
    {
        private readonly ICommentRepository commentRepository;

        public UpdateCommentCommandHandler(ICommentRepository commentRepository)
        {
            this.commentRepository = commentRepository;
        }

        public async Task<UpdateCommentCommandResponse> Handle(UpdateCommentCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateCommentCommandValidator(commentRepository);
            var validatorResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validatorResult.IsValid)
            {
                return new UpdateCommentCommandResponse
                {
                    Success = false,
                    ValidationErrors = validatorResult.Errors.Select(e => e.ErrorMessage).ToList()
                };
            }

            var taskResult = await commentRepository.FindByIdAsync(request.Id);
            if (!taskResult.IsSuccess)
            {
                return new UpdateCommentCommandResponse
                {
                    Success = false,
                    ValidationErrors = new List<string> { "Task not found" }
                };
            }

            taskResult.Value.Update(request.Id,request.TaskId,request.Content,request.Emotes);

            var dbTask = await commentRepository.UpdateAsync(taskResult.Value);

            return new UpdateCommentCommandResponse
            {
                Success = true,
                Comment = new UpdateCommentDto
                {
                    Id = dbTask.Value.Id,
                    TaskId = dbTask.Value.TaskId,
                    Content = dbTask.Value.Content,
                    Emotes = dbTask.Value.Emotes,
                }
            };
        }
    }
}

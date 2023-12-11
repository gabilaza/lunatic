
using Lunatic.Application.Persistence;
using Lunatic.Application.Features.CommentEmotes.Payload;
using MediatR;


namespace Lunatic.Application.Features.CommentEmotes.Commands.UpdateCommentEmote {
    public class UpdateCommentEmoteCommandHandler : IRequestHandler<UpdateCommentEmoteCommand, UpdateCommentEmoteCommandResponse> {
        private readonly ICommentEmoteRepository commentEmoteRepository;

        public UpdateCommentEmoteCommandHandler(ICommentEmoteRepository commentEmoteRepository) {
            this.commentEmoteRepository = commentEmoteRepository;
        }

        public async Task<UpdateCommentEmoteCommandResponse> Handle(UpdateCommentEmoteCommand request, CancellationToken cancellationToken) {
            var validator = new UpdateCommentEmoteCommandValidator();
            var validatorResult = await validator.ValidateAsync(request, cancellationToken);

            if(!validatorResult.IsValid) {
                return new UpdateCommentEmoteCommandResponse {
                    Success = false,
                    ValidationErrors = validatorResult.Errors.Select(e => e.ErrorMessage).ToList()
                };
            }

            var commentEmoteResult = await this.commentEmoteRepository.FindByIdAsync(request.Id);
            if(!commentEmoteResult.IsSuccess) {
                return new UpdateCommentEmoteCommandResponse {
                    Success = false,
                    ValidationErrors = new List<string> { "Comment Emote not found" }
                };
            }

            commentEmoteResult.Value.Update(request.Emote);

            var dbCommentEmoteResult = await this.commentEmoteRepository.UpdateAsync(commentEmoteResult.Value);

            return new UpdateCommentEmoteCommandResponse {
                Success = true,
                CommentEmote = new CommentEmoteDto {
                    Id = dbCommentEmoteResult.Value.Id,
                    UserId = dbCommentEmoteResult.Value.UserId,
                    CommentId = dbCommentEmoteResult.Value.CommentId,

                    Emote = dbCommentEmoteResult.Value.Emote,
                }
            };
        }
    }
}

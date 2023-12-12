
using Lunatic.Application.Persistence;
using Lunatic.Domain.Entities;
using Lunatic.Application.Features.CommentEmotes.Payload;
using MediatR;


namespace Lunatic.Application.Features.Comments.Commands.CreateCommentEmote {
    public class CreateCommentEmoteCommandHandler : IRequestHandler<CreateCommentEmoteCommand, CreateCommentEmoteCommandResponse> {
        private readonly ICommentEmoteRepository commentEmoteRepository;

        private readonly ICommentRepository commentRepository;

        private readonly IUserRepository userRepository;

        public CreateCommentEmoteCommandHandler(ICommentEmoteRepository commentEmoteRepository, ICommentRepository commentRepository, IUserRepository userRepository) {
            this.commentEmoteRepository = commentEmoteRepository;
            this.commentRepository = commentRepository;
            this.userRepository = userRepository;
        }

        public async Task<CreateCommentEmoteCommandResponse> Handle(CreateCommentEmoteCommand request, CancellationToken cancellationToken) {
            var validator = new CreateCommentEmoteCommandValidator(this.userRepository, this.commentRepository);
            var validatorResult = await validator.ValidateAsync(request, cancellationToken);

            if(!validatorResult.IsValid) {
                return new CreateCommentEmoteCommandResponse {
                    Success = false,
                    ValidationErrors = validatorResult.Errors.Select(e => e.ErrorMessage).ToList()
                };
            }

            var commentEmoteResult = CommentEmote.Create(request.UserId, request.CommentId, request.Emote);
            if(!commentEmoteResult.IsSuccess) {
                return new CreateCommentEmoteCommandResponse {
                    Success = false,
                    ValidationErrors = new List<string> { commentEmoteResult.Error }
                };
            }

            var comment = (await this.commentRepository.FindByIdAsync(request.CommentId)).Value;
            comment.AddEmote(commentEmoteResult.Value);
            await this.commentRepository.UpdateAsync(comment);

            await this.commentEmoteRepository.AddAsync(commentEmoteResult.Value);

            return new CreateCommentEmoteCommandResponse {
                Success = true,
                CommentEmote = new CommentEmoteDto {
                    CommentEmoteId = commentEmoteResult.Value.CommentEmoteId,
                    UserId = commentEmoteResult.Value.UserId,
                    CommentId = commentEmoteResult.Value.CommentId,

                    Emote = commentEmoteResult.Value.Emote,
                }
            };
        }
    }
}

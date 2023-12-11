
using FluentValidation;


namespace Lunatic.Application.Features.CommentEmotes.Commands.UpdateCommentEmote {
    internal class UpdateCommentEmoteCommandValidator : AbstractValidator<UpdateCommentEmoteCommand> {
        public UpdateCommentEmoteCommandValidator() {
            RuleFor(request => request.CommentEmoteId)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull().WithMessage("{PropertyName} is required.");

            RuleFor(request => request.Emote)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull().WithMessage("{PropertyName} is required.")
                .IsInEnum().WithMessage("{PropertyName} is not a valid emote.");
        }
    }
}

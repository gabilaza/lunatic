
using FluentValidation;


namespace Lunatic.Application.Features.CommentEmotes.Commands.UpdateCommentEmote {
    internal class UpdateCommentEmoteCommandValidator : AbstractValidator<UpdateCommentEmoteCommand> {
        public UpdateCommentEmoteCommandValidator() {
            RuleFor(commetEmote => commetEmote.CommentEmoteId)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull().WithMessage("{PropertyName} is required.");

            RuleFor(commetEmote => commetEmote.Emote)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull().WithMessage("{PropertyName} is required.")
                .IsInEnum().WithMessage("{PropertyName} is not a valid emote.");
        }
    }
}

﻿
using Lunatic.Application.Persistence;
using FluentValidation;


namespace Lunatic.Application.Features.CommentEmotes.Commands.CreateCommentEmote {
    internal class CreateCommentEmoteCommandValidator : AbstractValidator<CreateCommentEmoteCommand> {
        private readonly IUserRepository userRepository;

        private readonly ICommentRepository commentRepository;

        public CreateCommentEmoteCommandValidator(IUserRepository userRepository, ICommentRepository commentRepository) {
            this.userRepository = userRepository;
            this.commentRepository = commentRepository;

            RuleFor(commetEmote => commetEmote.UserId)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull().WithMessage("{PropertyName} is required.")
                .MustAsync(async (userId, cancellationToken) => await this.userRepository.ExistsByIdAsync(userId))
                .WithMessage("{PropertyName} must exists.");

            RuleFor(commetEmote => commetEmote.CommentId)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull().WithMessage("{PropertyName} is required.")
                .MustAsync(async (commentId, cancellationToken) => await this.commentRepository.ExistsByIdAsync(commentId))
                .WithMessage("{PropertyName} must exists.");

            RuleFor(commetEmote => commetEmote.Emote)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull().WithMessage("{PropertyName} is required.")
                .IsInEnum().WithMessage("{PropertyName} is not a valid emote.");
        }
    }
}

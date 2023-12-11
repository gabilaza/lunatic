﻿
using Lunatic.Application.Persistence;
using FluentValidation;


namespace Lunatic.Application.Features.Comments.Commands.CreateComment {
    internal class CreateCommentCommandValidator : AbstractValidator<CreateCommentCommand> {
        private readonly IUserRepository userRepository;

        private readonly ITaskRepository taskRepository;

        public CreateCommentCommandValidator(IUserRepository userRepository, ITaskRepository taskRepository) {
            this.userRepository = userRepository;
            this.taskRepository = taskRepository;

            RuleFor(commet => commet.UserId)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull().WithMessage("{PropertyName} is required.")
                .MustAsync(async (userId, cancellationToken) => await this.userRepository.ExistsByIdAsync(userId))
                .WithMessage("{PropertyName} must exists.");

            RuleFor(commet => commet.TaskId)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull().WithMessage("{PropertyName} is required.")
                .MustAsync(async (taskId, cancellationToken) => await this.taskRepository.ExistsByIdAsync(taskId))
                .WithMessage("{PropertyName} must exists.");

            RuleFor(commet => commet.Content)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull().WithMessage("{PropertyName} is required.")
                .MaximumLength(100).WithMessage("{PropertyName} must not exceed 100 characters.");
        }
    }
}

﻿
using Lunatic.Application.Persistence;
using FluentValidation;


namespace Lunatic.Application.Features.Tasks.Commands.UpdateTask {
    internal class UpdateTaskCommandValidator : AbstractValidator<UpdateTaskCommand> {
        private readonly ITaskRepository taskRepository;

        public UpdateTaskCommandValidator(ITaskRepository taskRepository) {
            this.taskRepository = taskRepository;

            RuleFor(t => t.Title)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull().WithMessage("{PropertyName} is required.")
                .MaximumLength(25).WithMessage("{PropertyName} must not exceed 50 characters.");

            RuleFor(t => t.Description)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull().WithMessage("{PropertyName} is required.")
                .MaximumLength(100).WithMessage("{PropertyName} must not exceed 50 characters.");

            RuleFor(t => t.Priority)
                .NotNull().WithMessage("{PropertyName} is required.");
        }
    }
}
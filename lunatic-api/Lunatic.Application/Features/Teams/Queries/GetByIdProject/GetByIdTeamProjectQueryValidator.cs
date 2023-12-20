
using Lunatic.Application.Persistence;
using FluentValidation;


namespace Lunatic.Application.Features.Teams.Queries.GetByIdProject {
    internal class GetByIdTeamProjectQueryValidator : AbstractValidator<GetByIdTeamProjectQuery> {
        private readonly IProjectRepository projectRepository;

        public GetByIdTeamProjectQueryValidator(IProjectRepository projectRepository) {
            this.projectRepository = projectRepository;

            RuleFor(request => request.ProjectId)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull().WithMessage("{PropertyName} is required.")
                .MustAsync(async (projectId, cancellationToken) => await this.projectRepository.ExistsByIdAsync(projectId))
                .WithMessage("{PropertyName} must exists.");
        }
    }
}


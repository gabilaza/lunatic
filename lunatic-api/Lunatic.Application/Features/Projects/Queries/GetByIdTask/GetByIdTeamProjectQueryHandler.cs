
using Lunatic.Application.Features.Tasks.Payload;
using Lunatic.Application.Persistence;
using MediatR;


namespace Lunatic.Application.Features.Projects.Queries.GetByIdTask {
	public class GetByIdProjectTaskQueryHandler : IRequestHandler<GetByIdProjectTaskQuery, GetByIdProjectTaskQueryResponse> {
		private readonly ITaskRepository taskRepository;

		public GetByIdProjectTaskQueryHandler(ITaskRepository taskRepository) {
			this.taskRepository = taskRepository;
		}

		public async Task<GetByIdProjectTaskQueryResponse> Handle(GetByIdProjectTaskQuery request, CancellationToken cancellationToken) {
			var validator = new GetByIdProjectTaskQueryValidator(this.taskRepository);
			var validatorResult = await validator.ValidateAsync(request, cancellationToken);

			if (!validatorResult.IsValid) {
				return new GetByIdProjectTaskQueryResponse {
					Success = false,
					ValidationErrors = validatorResult.Errors.Select(e => e.ErrorMessage).ToList()
				};
			}

			var task = (await this.taskRepository.FindByIdAsync(request.TaskId)).Value;

			return new GetByIdProjectTaskQueryResponse {
				Success = true,
				Task = new TaskDto {
					TaskId = task.TaskId,
					ProjectId = task.ProjectId,

					Title = task.Title,
					Description = task.Description,
					Priority = task.Priority,
					Status = task.Status,

					//Tags = task.Tags,
					CommentIds = task.CommentIds,
					AssigneeIds = task.AssigneeIds,

					StartedDate = task.StartedDate,
					EndedDate = task.EndedDate,
				}
			};
		}
	}
}

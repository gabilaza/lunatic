﻿
using Lunatic.Application.Persistence;
using Lunatic.Application.Features.Teams.Payload;
using MediatR;


namespace Lunatic.Application.Features.Teams.Queries.GetAll {
    public class GetAllTeamsQueryHandler : IRequestHandler<GetAllTeamsQuery, GetAllTeamsQueryResponse> {
        private readonly ITeamRepository teamRepository;

        public GetAllTeamsQueryHandler(ITeamRepository teamRepository) {
            this.teamRepository = teamRepository;
        }

        public async Task<GetAllTeamsQueryResponse> Handle(GetAllTeamsQuery request, CancellationToken cancellationToken) {
            GetAllTeamsQueryResponse response = new GetAllTeamsQueryResponse();
            var teams = await this.teamRepository.GetAllAsync();

            if(teams.IsSuccess) {
                response.Teams = teams.Value.Select(team => new TeamDto {
                    TeamId = team.TeamId,

                    Name = team.Name,
                    Description = team.Description,

                    MemberIds = team.MemberIds,
                    ProjectIds = team.ProjectIds,
                }).ToList();
            }
            return response;
        }
    }
}

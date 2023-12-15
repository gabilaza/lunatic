FROM mcr.microsoft.com/dotnet/sdk:8.0

RUN dotnet tool install --global dotnet-ef
ENV PATH="$PATH:/root/.dotnet/tools"

WORKDIR /migrator

COPY . ./

CMD cd /migrator/Lunatic.Infrastructure && dotnet ef database update -s ../Lunatic.API --context LunaticContext && cd /migrator/Lunatic.Identity && dotnet ef database update -s ../Lunatic.API --context LunaticUserContext


FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /build

COPY . ./

RUN dotnet restore

RUN dotnet publish -c Release -o release

FROM mcr.microsoft.com/dotnet/aspnet:8.0

COPY --from=build /build/release /release

WORKDIR /release

CMD ["dotnet", "Lunatic.Migrator.dll"]


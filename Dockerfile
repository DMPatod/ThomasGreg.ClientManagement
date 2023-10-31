FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env
WORKDIR /src

COPY src/BuildBlocks.Core /src/BuildBlocks.Core
COPY src/ClientManagement.API /src/ClientManagement.API
COPY src/ClientManagement.Application /src/ClientManagement.Application
COPY src/ClientManagement.Domain /src/ClientManagement.Domain
COPY src/ClientManagement.Infrastructure /src/ClientManagement.Infrastructure

WORKDIR /src/ClientManagement.API
RUN dotnet publish -c release -o /out

FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /out
COPY --from=build-env /out .
ENTRYPOINT ["dotnet", "ClientManagement.API.dll"]
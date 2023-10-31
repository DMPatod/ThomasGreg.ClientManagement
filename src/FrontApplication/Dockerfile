FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env
WORKDIR /src

COPY . .
RUN dotnet publish -c release -o /out

FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /out
COPY --from=build-env /out .
ENTRYPOINT ["dotnet", "FrontApplication.dll"]
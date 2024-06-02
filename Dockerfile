FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG version
ARG NUGET_USERNAME
ARG NUGET_TOKEN
WORKDIR /src

COPY ["Core.Web/Core.Web.csproj", "Core.Web/"]
COPY ["NuGet.config", "Core.Web/"]

RUN dotnet restore "Core.Web/Core.Web.csproj" --configfile Core.Web/NuGet.config

COPY . .

RUN dotnet publish "Core.Web/Core.Web.csproj" -c Release -o out /p:Version=$version

FROM mcr.microsoft.com/dotnet/aspnet:8.0 
WORKDIR /app

EXPOSE 80
EXPOSE 443

COPY --from=build /src/out .
ENTRYPOINT ["dotnet", "Core.Web.dll"]

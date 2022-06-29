#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/Simplify.Project.API/Simplify.Project.API.csproj", "src/Simplify.Project.API/"]
COPY ["src/Simplify.Project.API.Contracts/Simplify.Project.API.Contracts.csproj", "src/Simplify.Project.API.Contracts/"]
COPY ["src/Simplify.Project.Shared/Simplify.Project.Shared.csproj", "src/Simplify.Project.Shared/"]
COPY ["src/Simplify.Project.Front/Simplify.Project.Front.csproj", "src/Simplify.Project.Front/"]
COPY ["src/Simplify.Project.Model/Simplify.Project.Model.csproj", "src/Simplify.Project.Model/"]
RUN dotnet restore "src/Simplify.Project.API/Simplify.Project.API.csproj"
COPY . .
WORKDIR "/src/src/Simplify.Project.API"
RUN dotnet build "Simplify.Project.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Simplify.Project.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Simplify.Project.API.dll"]
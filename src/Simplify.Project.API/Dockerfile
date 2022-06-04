#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 6060
EXPOSE 6160

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/Simplify.Project.API/Simplify.Project.API.csproj", "Simplify.Project.API/"]
RUN dotnet restore "Simplify.Project.API/Simplify.Project.API.csproj"
WORKDIR "/src/Simplify.Project.API"
COPY . .
RUN dotnet build "Simplify.Project.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Simplify.Project.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Simplify.Project.API.dll"]

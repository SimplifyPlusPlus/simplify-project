#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 6061
EXPOSE 6161

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/Simplify.Project.Front/Simplify.Project.Front.csproj", "Simplify.Project.Front/"]
RUN dotnet restore "Simplify.Project.Front/Simplify.Project.Front.csproj"
WORKDIR "/src/Simplify.Project.Front"
COPY . .
RUN dotnet build "Simplify.Project.Front.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Simplify.Project.Front.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Simplify.Project.Front.dll"]

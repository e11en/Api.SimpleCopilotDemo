#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build

WORKDIR /src

# Restore packages
COPY ["src/Api.CopilotDemo/Api.CopilotDemo.csproj", "Api.CopilotDemo/"]
RUN dotnet restore "Api.CopilotDemo/Api.CopilotDemo.csproj"

WORKDIR "/src/Api.CopilotDemo"

# Copy needed files
COPY ./src/Api.CopilotDemo .

RUN dotnet build "Api.CopilotDemo.csproj" -c Release -o /app/build --no-restore

FROM build AS publish
RUN dotnet publish "Api.CopilotDemo.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Api.CopilotDemo.dll"]
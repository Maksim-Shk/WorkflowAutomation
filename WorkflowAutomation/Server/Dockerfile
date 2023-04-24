#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["WorkflowAutomation/Server/WorkflowAutomation.Server.csproj", "WorkflowAutomation/Server/"]
COPY ["WorkflowAutomation.Application/WorkflowAutomation.Application.csproj", "WorkflowAutomation.Application/"]
COPY ["WorkflowAutomation.Domain/WorkflowAutomation.Domain.csproj", "WorkflowAutomation.Domain/"]
COPY ["WorkflowAutomation.Persistence/WorkflowAutomation.Persistence.csproj", "WorkflowAutomation.Persistence/"]
COPY ["WorkflowAutomation/Client/WorkflowAutomation.Client.csproj", "WorkflowAutomation/Client/"]
COPY ["WorkflowAutomation/Shared/WorkflowAutomation.Shared.csproj", "WorkflowAutomation/Shared/"]
RUN dotnet restore "WorkflowAutomation/Server/WorkflowAutomation.Server.csproj"
COPY . .
WORKDIR "/src/WorkflowAutomation/Server"
RUN dotnet build "WorkflowAutomation.Server.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WorkflowAutomation.Server.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WorkflowAutomation.Server.dll"]
#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Application.API/Cliente/NotificationMicroserviceAPI.csproj", "Application.API/Cliente/"]
COPY ["Application.API/InfraForAPI/InfraForAPI.csproj", "Application.API/InfraForAPI/"]
COPY ["CoreApp/InfraForGlobal/InfraForGlobal.csproj", "CoreApp/InfraForGlobal/"]
COPY ["CoreApp/ManagerCore/BLLCore.csproj", "CoreApp/ManagerCore/"]
COPY ["CoreApp/DTOModelView/DTO.csproj", "CoreApp/DTOModelView/"]
RUN dotnet restore "Application.API/Cliente/NotificationMicroserviceAPI.csproj"
COPY . .
WORKDIR "/src/Application.API/Cliente"
RUN dotnet build "NotificationMicroserviceAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "NotificationMicroserviceAPI.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "NotificationMicroserviceAPI.dll"]
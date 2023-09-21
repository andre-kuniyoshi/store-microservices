
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["WebApps/AspNetCoreMVC/AspNetCoreMVC.csproj", "WebApps/AspNetCoreMVC/"]
COPY ["BuildingBlocks/Core/Core.csproj", "BuildingBlocks/Core/"]
RUN dotnet restore "WebApps/AspNetCoreMVC/AspNetCoreMVC.csproj"
COPY . .
WORKDIR "/src/WebApps/AspNetCoreMVC"
#RUN dotnet build "AspNetCoreMVC.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AspNetCoreMVC.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AspNetCoreMVC.dll"]
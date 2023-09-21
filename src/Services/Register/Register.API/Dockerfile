
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Services/Register/Register.API/Register.API.csproj", "Services/Register/Register.API/"]
COPY ["Services/Register/Register.Application/Register.Application.csproj", "Services/Register/Register.Application/"]
COPY ["BuildingBlocks/Core/Core.csproj", "BuildingBlocks/Core/"]
COPY ["Services/Register/Register.Infra/Register.Infra.csproj", "Services/Register/Register.Infra/"]
RUN dotnet restore "Services/Register/Register.API/Register.API.csproj"
COPY . .
WORKDIR "/src/Services/Register/Register.API"
#RUN dotnet build "Register.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Register.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Register.API.dll"]
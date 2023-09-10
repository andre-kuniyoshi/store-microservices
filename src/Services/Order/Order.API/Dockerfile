
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Services/Order/Order.API/Order.API.csproj", "Services/Order/Order.API/"]
COPY ["BuildingBlocks/EventBus.Messages/EventBus.Messages.csproj", "BuildingBlocks/EventBus.Messages/"]
COPY ["Services/Order/Order.Application/Order.Application.csproj", "Services/Order/Order.Application/"]
COPY ["Services/Order/Order.Domain/Order.Domain.csproj", "Services/Order/Order.Domain/"]
COPY ["BuildingBlocks/Core/Core.csproj", "BuildingBlocks/Core/"]
COPY ["Services/Order/Order.Infra/Order.Infra.csproj", "Services/Order/Order.Infra/"]
RUN dotnet restore "Services/Order/Order.API/Order.API.csproj"
COPY . .
WORKDIR "/src/Services/Order/Order.API"
RUN dotnet build "Order.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Order.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Order.API.dll"]
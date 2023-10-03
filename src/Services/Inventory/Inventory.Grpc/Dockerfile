
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Services/Inventory/Inventory.Grpc/Inventory.Grpc.csproj", "Services/Inventory/Inventory.Grpc/"]
COPY ["Services/Inventory/Inventory.Application/Inventory.Application.csproj", "Services/Inventory/Inventory.Application/"]
COPY ["BuildingBlocks/Core/Core.csproj", "BuildingBlocks/Core/"]
COPY ["Services/Inventory/Inventory.Domain/Inventory.Domain.csproj", "Services/Inventory/Inventory.Domain/"]
COPY ["Services/Inventory/Inventory.Infra/Inventory.Infra.csproj", "Services/Inventory/Inventory.Infra/"]
RUN dotnet restore "Services/Inventory/Inventory.Grpc/Inventory.Grpc.csproj"
COPY . .
WORKDIR "/src/Services/Inventory/Inventory.Grpc"
#RUN dotnet build "Inventory.Grpc.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Inventory.Grpc.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Inventory.Grpc.dll"]
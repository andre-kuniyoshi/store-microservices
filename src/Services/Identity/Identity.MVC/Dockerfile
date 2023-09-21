#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Services/Identity/Identity.MVC/Identity.MVC.csproj", "Services/Identity/Identity.MVC/"]
COPY ["Services/Identity/Identity.Infra/Identity.Infra.csproj", "Services/Identity/Identity.Infra/"]
COPY ["BuildingBlocks/Core/Core.csproj", "BuildingBlocks/Core/"]
RUN dotnet restore "Services/Identity/Identity.MVC/Identity.MVC.csproj"
COPY . .
WORKDIR "/src/Services/Identity/Identity.MVC"
#RUN dotnet build "Identity.MVC.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Identity.MVC.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Identity.MVC.dll"]
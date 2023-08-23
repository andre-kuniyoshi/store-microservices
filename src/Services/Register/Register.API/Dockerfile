#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/Server-side/Register.API/Register.API.csproj", "src/Server-side/Register.API/"]
COPY ["src/Server-side/Register.Application/Register.Application.csproj", "src/Server-side/Register.Application/"]
COPY ["src/Server-side/Register.Infra/Register.Infra.csproj", "src/Server-side/Register.Infra/"]
RUN dotnet restore "src/Server-side/Register.API/Register.API.csproj"
COPY . .
WORKDIR "/src/src/Server-side/Register.API"
RUN dotnet build "Register.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Register.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Register.API.dll"]
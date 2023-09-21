
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["WebApps/AspNetRazor/AspNetRazor.csproj", "WebApps/AspNetRazor/"]
RUN dotnet restore "WebApps/AspNetRazor/AspNetRazor.csproj"
COPY . .
WORKDIR "/src/WebApps/AspNetRazor"
#RUN dotnet build "AspNetRazor.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AspNetRazor.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AspNetRazor.dll"]
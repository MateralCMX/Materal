FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["Gateway/NuGet.Config","Gateway/"]
COPY ["Common/ocelot.KMJQ129.json","Common/ocelot.json"]
COPY ["Gateway/Gateway.csproj", "Gateway/"]
COPY ["Common/Common.csproj", "Common/"]
RUN dotnet restore "Gateway/Gateway.csproj" --configfile "Gateway/NuGet.Config"
COPY . .
WORKDIR "/src/Gateway"
RUN dotnet build "Gateway.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Gateway.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Gateway.dll"]
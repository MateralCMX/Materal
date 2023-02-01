#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 5000

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["NuGet.Config",""]
COPY ["Materal.Gateway.WebAPI/Materal.Gateway.WebAPI.csproj", "Materal.Gateway.WebAPI/"]
COPY ["Materal.Gateway.Common/Materal.Gateway.Common.csproj", "Materal.Gateway.Common/"]
RUN dotnet restore "Materal.Gateway.WebAPI/Materal.Gateway.WebAPI.csproj"
COPY . .
WORKDIR "/src/Materal.Gateway.WebAPI"
RUN dotnet build "Materal.Gateway.WebAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Materal.Gateway.WebAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Materal.Gateway.WebAPI.dll", "--urls=http://127.0.0.1:5000"]
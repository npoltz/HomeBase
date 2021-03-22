#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 5020

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["HomeBase.Api/HomeBase.Api.csproj", "HomeBase.Api/"]
COPY ["HomeBase.Core/HomeBase.Core.csproj", "HomeBase.Core/"]
COPY ["HomeBase.Data/HomeBase.Data.csproj", "HomeBase.Data/"]
RUN dotnet restore "HomeBase.Api/HomeBase.Api.csproj"
COPY . .
WORKDIR "/src/HomeBase.Api"
RUN dotnet build "HomeBase.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "HomeBase.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "HomeBase.Api.dll"]

#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["HomeBase.Web/HomeBase.Web.csproj", "HomeBase.Web/"]
RUN dotnet restore "HomeBase.Web/HomeBase.Web.csproj"
COPY . .
WORKDIR "/src/HomeBase.Web"
RUN dotnet build "HomeBase.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "HomeBase.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "HomeBase.Web.dll"]
#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 13001

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["CargillTest.API/CargillTest.API.csproj", "CargillTest.API/"]
COPY ["CargillTest.DAL/CargillTest.DAL.csproj", "CargillTest.DAL/"]
RUN dotnet restore "./CargillTest.API/CargillTest.API.csproj"
COPY . .
WORKDIR "/src/CargillTest.API"
RUN dotnet build "./CargillTest.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./CargillTest.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CargillTest.API.dll"]
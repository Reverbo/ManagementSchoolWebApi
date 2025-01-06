#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Management.Application/Management.Application.csproj", "Management.Application/"] 
COPY ["Management.Domain/Management.Domain.csproj", "Management.Domain/"] 
COPY ["Management.Infrasctructure/Management.Infrasctructure.csproj", "Management.Infrasctructure/"] 
RUN dotnet restore "./Management.Application/Management.Application.csproj"
COPY . .
WORKDIR "/src/Management.Application"
RUN dotnet build "./Management.Application.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Management.Application.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Management.Application.dll"]
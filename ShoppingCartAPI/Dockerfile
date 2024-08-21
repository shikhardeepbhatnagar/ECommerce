#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 80
ENV ASPNETCORE_ENVIRONMENT=Development
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["./ShoppingCartAPI/ShoppingCartAPI.csproj", "ShoppingCartAPI/"]
COPY ["./Framework/EventBus.Messages/EventBus.Messages.csproj", "Framework/EventBus.Messages"]
RUN dotnet restore "./ShoppingCartAPI/./ShoppingCartAPI.csproj"
WORKDIR "/src/ShoppingCartAPI"
COPY . .
RUN dotnet build "./ShoppingCartAPI/ShoppingCartAPI.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./ShoppingCartAPI/ShoppingCartAPI.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
RUN mkdir -p /app/Data && chmod -R 777 /app/Data
ENTRYPOINT ["dotnet", "ShoppingCartAPI.dll"]
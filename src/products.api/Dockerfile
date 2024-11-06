# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 5000
EXPOSE 5001


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["products.api/products.api.csproj", "products.api/"]
COPY ["products.application/products.application.csproj", "products.application/"]
COPY ["products.crosscutting/products.crosscutting.csproj", "products.crosscutting/"]
COPY ["products.domain/products.domain/products.domain.csproj", "products.domain/products.domain/"]
COPY ["products.infra/products.infra.csproj", "products.infra/"]
COPY ["products.IoC/products.IoC.csproj", "products.IoC/"]
RUN dotnet restore "./products.api/products.api.csproj"
COPY . .
WORKDIR "/src/products.api"
RUN dotnet build "./products.api.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./products.api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "products.api.dll"]
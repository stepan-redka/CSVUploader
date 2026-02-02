# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy csproj and restore dependencies
COPY TestApp/TestApp.csproj TestApp/
RUN dotnet restore TestApp/TestApp.csproj

# Copy everything else and build
COPY TestApp/ TestApp/
WORKDIR /src/TestApp
RUN dotnet build TestApp.csproj -c Release -o /app/build

# Publish stage
FROM build AS publish
RUN dotnet publish TestApp.csproj -c Release -o /app/publish /p:UseAppHost=false

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Copy published app
COPY --from=publish /app/publish .

# Set environment variables
ENV ASPNETCORE_URLS=http://+:8080
ENV ASPNETCORE_ENVIRONMENT=Development

ENTRYPOINT ["dotnet", "TestApp.dll"]

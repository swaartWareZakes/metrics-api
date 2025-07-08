# ----------- Build Stage -----------
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy project file and restore dependencies
COPY *.csproj ./
RUN dotnet restore

# Copy the entire source (including version files)
COPY . ./

# ✅ Ensure version.json is copied to output
RUN dotnet publish -c Release -o /app/out

# ----------- Runtime Stage -----------
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

LABEL maintainer="you@yourcompany.com"
LABEL org.opencontainers.image.source="https://github.com/yourrepo/MetricsApi"

# ✅ Copy published output from build stage
COPY --from=build /app/out ./

# ✅ Explicitly copy version.json if needed separately
COPY --from=build /src/version.json ./version.json

# Create and use non-root user
RUN adduser --disabled-password appuser
USER appuser

# Expose HTTP port
EXPOSE 80

# ✅ Ensure .NET uses port 80
ENV ASPNETCORE_URLS=http://+:80

# ✅ Healthcheck for orchestrators
HEALTHCHECK --interval=30s --timeout=5s --start-period=10s \
  CMD curl --fail http://localhost/health/status || exit 1

# ✅ Run the app
ENTRYPOINT ["dotnet", "MetricsApi.dll"]

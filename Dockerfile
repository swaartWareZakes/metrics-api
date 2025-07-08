# ----------- Build Stage -----------
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY *.csproj ./
RUN dotnet restore

COPY . ./

# ✅ Copy version.json explicitly so it's available in final image
COPY version.json ./version.json

RUN dotnet publish -c Release -o /app/out

# ----------- Runtime Stage -----------
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

LABEL maintainer="you@yourcompany.com"
LABEL org.opencontainers.image.source="https://github.com/yourrepo/MetricsApi"

# ✅ Copy app output including version.json from build stage
COPY --from=build /app/out ./

# ✅ Copy version.json from source stage too
COPY --from=build /src/version.json ./version.json

RUN adduser --disabled-password appuser
USER appuser

# Expose HTTP
EXPOSE 80

# ✅ Ensure .NET listens on port 80 inside container
ENV ASPNETCORE_URLS=http://+:80

# Healthcheck for orchestrators
HEALTHCHECK --interval=30s --timeout=5s --start-period=10s \
  CMD curl --fail http://localhost/health/status || exit 1

ENTRYPOINT ["dotnet", "MetricsApi.dll"]

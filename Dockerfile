FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copy all files
COPY . .

# Restore and build the WebApp project
RUN dotnet restore "Endpoint_WebApp/Endpoint_WebApp.csproj"
WORKDIR "/src/Endpoint_WebApp"
RUN dotnet publish "Endpoint_WebApp.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app
COPY --from=build /app/publish .

# Railway provides PORT environment variable.
# We map ASPNETCORE_HTTP_PORTS to the PORT variable if provided, or default to 8080.
EXPOSE 8080
ENV ASPNETCORE_HTTP_PORTS=8080

ENTRYPOINT ["dotnet", "Endpoint_WebApp.dll"]

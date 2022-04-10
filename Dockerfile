# Build stage
FROM mcr.microsoft.com/dotnet/sdk:6.0-focal AS build
WORKDIR /source
COPY . .
RUN dotnet restore "./Identity.API/Identity.API.csproj" --disable-parallel \
	&& dotnet publish "./Identity.API/Identity.API.csproj" -c release -o /app --no-restore

# Serve stage
FROM mcr.microsoft.com/dotnet/aspnet:6.0-focal AS stage
WORKDIR /app
COPY --from=build /app .

EXPOSE 5000

ENTRYPOINT ["dotnet", "Identity.API.dll"]
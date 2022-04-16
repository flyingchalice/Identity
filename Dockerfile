# Build stage
FROM mcr.microsoft.com/dotnet/sdk:6.0-focal AS build
COPY Identity.sln /

COPY src/Identity/Identity.API/ /src/Identity/Identity.API/
COPY src/Identity/Identity.Application/ /src/Identity/Identity.Application/
COPY src/Identity/Identity.Infrastructure/ /src/Identity/Identity.Infrastructure/
COPY src/Identity/Identity.Domain/ /src/Identity/Identity.Domain/

RUN dotnet restore --disable-parallel \
&& dotnet publish /src/Identity/Identity.API/Identity.API.csproj -c Release -o /app -r linux-x64 --self-contained

# Serve stage
FROM mcr.microsoft.com/dotnet/aspnet:6.0-focal AS stage

WORKDIR /app

COPY --from=build /app .

EXPOSE 5000

ENTRYPOINT ["dotnet", "Identity.API.dll"]
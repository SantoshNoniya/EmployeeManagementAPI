# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /src

COPY ["TaxCalculation.API/TaxCalculation.API.csproj", "TaxCalculation.API/"]
COPY ["TaxCalculation.Application/TaxCalculation.Application.csproj", "TaxCalculation.Application/"]
COPY ["TaxCalculation.Domain/TaxCalculation.Domain.csproj", "TaxCalculation.Domain/"]
COPY ["TaxCalculation.Infrastructure/TaxCalculation.Infrastructure.csproj", "TaxCalculation.Infrastructure/"]

RUN dotnet restore "TaxCalculation.API/TaxCalculation.API.csproj"

COPY . .

WORKDIR "/src/TaxCalculation.API"

RUN dotnet build "TaxCalculation.API.csproj" -c Release -o /app/build


# Publish stage
FROM build AS publish

RUN dotnet publish "TaxCalculation.API.csproj" \
    -c Release \
    -o /app/publish


# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final

WORKDIR /app

COPY --from=publish /app/publish .

EXPOSE 8080

ENTRYPOINT ["dotnet", "TaxCalculation.API.dll"]
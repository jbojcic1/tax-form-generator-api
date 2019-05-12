FROM mcr.microsoft.com/dotnet/core/sdk:2.2 AS build
WORKDIR /app

# copy csproj and restore as distinct layers
COPY *.sln .
COPY TaxFormGeneratorApi/*.csproj ./TaxFormGeneratorApi/
COPY Tests/*.csproj ./Tests/
RUN dotnet restore

# copy everything else and build app
COPY TaxFormGeneratorApi/. ./TaxFormGeneratorApi/
WORKDIR /app/TaxFormGeneratorApi
RUN dotnet publish -c Release -o out


FROM mcr.microsoft.com/dotnet/core/aspnet:2.2 AS runtime
WORKDIR /app
COPY --from=build /app/TaxFormGeneratorApi/out ./
ENTRYPOINT ["dotnet", "TaxFormGeneratorApi.dll"]

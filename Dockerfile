FROM mcr.microsoft.com/dotnet/core/sdk:2.2

WORKDIR /app

COPY Tests ./Tests
COPY TaxFormGeneratorApi ./TaxFormGeneratorApi

EXPOSE 5001
EXPOSE 5000

WORKDIR /app/TaxFormGeneratorApi
ENTRYPOINT ["dotnet", "run"]

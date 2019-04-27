FROM mcr.microsoft.com/dotnet/core/sdk:2.2

WORKDIR /app

COPY . .

EXPOSE 5001
EXPOSE 5000

WORKDIR /app/TaxFormGeneratorApi
ENTRYPOINT ["dotnet", "run"]

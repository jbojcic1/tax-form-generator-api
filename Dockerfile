FROM mcr.microsoft.com/dotnet/core/sdk:2.2

WORKDIR /app

COPY ./TaxFormGeneratorApi .

EXPOSE 5001
EXPOSE 5000


ENTRYPOINT ["dotnet", "run"]
# "&&", "dotnet", "ef", "database", "update",
FROM mcr.microsoft.com/dotnet/core/sdk:2.2

COPY . .

ENTRYPOINT ["dotnet", "ef", "database", "update"]
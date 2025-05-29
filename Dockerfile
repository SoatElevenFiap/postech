FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /src 

COPY . .

RUN dotnet restore "src/Soat.Eleven.FastFood.sln"

RUN dotnet publish "src/Soat.Eleven.FastFood.Api/Soat.Eleven.FastFood.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS migrator

WORKDIR /app

COPY --from=build /app/publish .

COPY . .

COPY wait-for-it.sh .
RUN chmod +x wait-for-it.sh

RUN apt-get update && apt-get install -y postgresql-client locales tzdata && rm -rf /var/lib/apt/lists/*

RUN dotnet tool install --global dotnet-ef --version 8.*

ENV PATH="/root/.dotnet/tools:${PATH}"

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final

WORKDIR /app 

COPY --from=build /app/publish .

ENV ConnectionStrings__DefaultConnection="Host=db;Database=fastfood;Username=admin;Password=admin123"

EXPOSE 8080

ENTRYPOINT ["dotnet", "Soat.Eleven.FastFood.Api.dll"]


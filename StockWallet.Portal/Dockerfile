FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["StockWallet.Portal/StockWallet.Portal.csproj", "StockWallet.Portal/"]
RUN dotnet restore "StockWallet.Portal/StockWallet.Portal.csproj"
COPY . .
WORKDIR "/src/StockWallet.Portal"
RUN dotnet build "StockWallet.Portal.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "StockWallet.Portal.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "StockWallet.Portal.dll"]

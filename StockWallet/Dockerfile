FROM mcr.microsoft.com/dotnet/aspnet:6.0-alpine AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0-alpine AS build
WORKDIR /src
COPY . .
RUN rm -rf "StockWallet/bin StockWallet/obj"
RUN rm -rf "StockWallet.Db.Mysql/bin StockWallet.Db.Mysql/obj"
RUN rm -rf "StockWallet.Domain/bin StockWallet.Domain/obj"

RUN dotnet restore -r linux-musl-x64 "StockWallet/StockWallet.csproj"
WORKDIR "/src/StockWallet"
RUN dotnet build "StockWallet.csproj" -c Release -r linux-musl-x64  --self-contained -o /app/build

FROM build AS publish
RUN dotnet publish "StockWallet.csproj" -c Release -r linux-musl-x64 --self-contained -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "StockWallet.dll"]

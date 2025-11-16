# 実行用ベースイメージ
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 5000

# SDK ビルドステージ
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# プロジェクトファイルだけコピー
COPY FlowApp.csproj ./

# restore
RUN dotnet restore FlowApp.csproj

# ソースコードをすべてコピー
COPY . ./

# ビルド・パブリッシュ
RUN dotnet publish FlowApp.csproj -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# 実行ステージ
FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "FlowApp.dll"]

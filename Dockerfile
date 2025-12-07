FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 5000
ENV ASPNETCORE_URLS=http://+:5000

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["GoodHamburger/GoodHamburger.csproj", "."]
COPY ["infrastructure/infrastructure/infrastructure.csproj", "infrastructure/infrastructure/"]
COPY ["CoreLibrary/CoreLibrary.csproj", "CoreLibrary/"]
COPY ["domain/Domain.csproj", "domain/"]
RUN dotnet restore "./GoodHamburger.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "GoodHamburger/GoodHamburger.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "GoodHamburger/GoodHamburger.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "GoodHamburger.dll"]
#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["CarMarket.UI/CarMarket.UI.csproj", "CarMarket.UI/"]
COPY ["CarMarket.BusinessLogic/CarMarket.BusinessLogic.csproj", "CarMarket.BusinessLogic/"]
COPY ["CarMarket.Core/CarMarket.Core.csproj", "CarMarket.Core/"]
RUN dotnet restore "CarMarket.UI/CarMarket.UI.csproj"
COPY . .
WORKDIR "/src/CarMarket.UI"
RUN dotnet build "CarMarket.UI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CarMarket.UI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CarMarket.UI.dll"]
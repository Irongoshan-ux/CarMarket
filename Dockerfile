# https://hub.docker.com/_/microsoft-dotnet
FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /CarMarket

# copy csproj and restore as distinct layers
COPY *.sln ./

COPY ./Server/*.csproj ./Server/
COPY ./CarMarket.UI/*.csproj ./CarMarket.UI/
COPY ./CarMarket.Data/*.csproj ./CarMarket.Data/
COPY ./CarMarket.Core/*.csproj ./CarMarket.Core/
COPY ./CarMarket.BusinessLogic/*.csproj ./CarMarket.BusinessLogic/
COPY ./CarMarket.BusinessLogic.Tests/*.csproj ./CarMarket.BusinessLogic.Tests/

RUN dotnet restore -r linux-x64

# copy everything else and build app
COPY . ./
WORKDIR /CarMarket
RUN dotnet publish -c release -o /out -r linux-x64 --no-cache /restore

# final stage/image
FROM mcr.microsoft.com/dotnet/sdk:5.0
WORKDIR /CarMarket
COPY --from=build ./out .
ENTRYPOINT ["dotnet", "CarMarket.Server.dll"]

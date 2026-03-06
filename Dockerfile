FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY KutuphaneTakip/*.csproj ./KutuphaneTakip/
RUN dotnet restore ./KutuphaneTakip/KutuphaneTakip.csproj

COPY . .
WORKDIR /src/KutuphaneTakip
RUN dotnet publish -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "KutuphaneTakip.dll"]
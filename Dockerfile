
FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
RUN apt-get update -yq \
    && apt-get install curl gnupg -yq \
    && curl -sL https://deb.nodesource.com/setup_10.x | bash \
    && apt-get install nodejs -yq
    
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
RUN apt-get update -yq \
    && apt-get install curl gnupg -yq \
    && curl -sL https://deb.nodesource.com/setup_10.x | bash \
    && apt-get install nodejs -yq

WORKDIR /src
COPY ["vcar.csproj", "."]
RUN dotnet restore "./vcar.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "vcar.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "vcar.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
# ENTRYPOINT ["dotnet", "vcar.dll"]
CMD ASPNETCORE_URLS=http://*:$PORT dotnet vcar.dll
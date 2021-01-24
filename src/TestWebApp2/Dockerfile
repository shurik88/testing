FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY [".", "TestWebApp2/"]
RUN dotnet restore "TestWebApp2/TestWebApp2.csproj"
COPY . .
WORKDIR "/src/TestWebApp2"
RUN dotnet build "TestWebApp2.csproj" -c Release -o /app/build

FROM build AS publish
RUN curl -sL https://deb.nodesource.com/setup_10.x | bash -
RUN apt-get install --assume-yes nodejs
RUN dotnet publish "TestWebApp2.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
COPY TestWebApp2.pfx /app/TestWebApp2.pfx
ENTRYPOINT ["dotnet", "TestWebApp2.dll"]
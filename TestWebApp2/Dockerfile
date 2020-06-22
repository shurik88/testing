FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80

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
ENTRYPOINT ["dotnet", "TestWebApp2.dll"]


#FROM microsoft/dotnet:2.1-sdk AS build
#WORKDIR /app
#COPY *.csproj ./
#RUN dotnet restore
#COPY . ./
#RUN dotnet publish --no-restore -c Release -o out
#FROM microsoft/dotnet:2.1-aspnetcore-runtime AS runtime
#WORKDIR /app
#COPY — from=build /app/out .
#ENTRYPOINT [“dotnet”, “WebApplication2.dll”]`
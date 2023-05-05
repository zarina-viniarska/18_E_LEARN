FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
MAINTAINER Trofimchuk Andrii
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
COPY . /app
WORKDIR "/app/18_E_LEARN.Web"
RUN dotnet restore "18_E_LEARN.Web.csproj"
RUN dotnet publish "18_E_LEARN.Web.csproj" -c Release -o /app/build

WORKDIR /app/build
ENTRYPOINT ["dotnet", "18_E_LEARN.Web.dll", "--urls=http://0.0.0.0:80"]

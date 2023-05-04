#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["18_E_LEARN.Web/18_E_LEARN.Web.csproj", "18_E_LEARN.Web/"]
COPY ["18_E_LEARN.BusinessLogic/18_E_LEARN.BusinessLogic.csproj", "18_E_LEARN.BusinessLogic/"]
COPY ["18_E_LEARN.DataAccess/18_E_LEARN.DataAccess.csproj", "18_E_LEARN.DataAccess/"]
RUN dotnet restore "18_E_LEARN.Web/18_E_LEARN.Web.csproj"
COPY . .
WORKDIR "/src/18_E_LEARN.Web"
RUN dotnet build "18_E_LEARN.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "18_E_LEARN.Web.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "18_E_LEARN.Web.dll"]
#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["MetricsConversion.WebApi/MetricsConversion.WebApi.csproj", "MetricsConversion.WebApi/"]
RUN dotnet restore "MetricsConversion.WebApi/MetricsConversion.WebApi.csproj"
COPY . .
WORKDIR "/src/MetricsConversion.WebApi"
RUN dotnet build "MetricsConversion.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MetricsConversion.WebApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MetricsConversion.WebApi.dll"]
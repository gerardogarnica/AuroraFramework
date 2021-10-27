FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Gateway/Aurora.Platform.Gateway/Aurora.Platform.Gateway.csproj", "Gateway/Aurora.Platform.Gateway/"]
COPY ["Framework/Aurora.Framework.Common/Aurora.Framework.Common.csproj", "Framework/Aurora.Framework.Common/"]
RUN dotnet restore "Gateway/Aurora.Platform.Gateway/Aurora.Platform.Gateway.csproj"
COPY . .
WORKDIR "/src/Gateway/Aurora.Platform.Gateway"
RUN dotnet build "Aurora.Platform.Gateway.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Aurora.Platform.Gateway.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Aurora.Platform.Gateway.dll"]
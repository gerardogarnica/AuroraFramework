#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Services/Common/Aurora.Common.API/Aurora.Common.API.csproj", "Services/Common/Aurora.Common.API/"]
RUN dotnet restore "Services/Common/Aurora.Common.API/Aurora.Common.API.csproj"
COPY . .
WORKDIR "/src/Services/Common/Aurora.Common.API"
RUN dotnet build "Aurora.Common.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Aurora.Common.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Aurora.Common.API.dll"]
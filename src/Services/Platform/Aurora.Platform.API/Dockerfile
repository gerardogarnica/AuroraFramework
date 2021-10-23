#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Services/Platform/Aurora.Platform.API/Aurora.Platform.API.csproj", "Services/Platform/Aurora.Platform.API/"]
RUN dotnet restore "Services/Platform/Aurora.Platform.API/Aurora.Platform.API.csproj"
COPY . .
WORKDIR "/src/Services/Platform/Aurora.Platform.API"
RUN dotnet build "Aurora.Platform.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Aurora.Platform.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Aurora.Platform.API.dll"]
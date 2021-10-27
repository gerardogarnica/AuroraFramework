FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Services/Platform/Aurora.Platform.API/Aurora.Platform.API.csproj", "Services/Platform/Aurora.Platform.API/"]
COPY ["Framework/Aurora.Framework.Common/Aurora.Framework.Common.csproj", "Framework/Aurora.Framework.Common/"]
COPY ["Framework/Aurora.Framework.Logic/Aurora.Framework.Logic.csproj", "Framework/Aurora.Framework.Logic/"]
COPY ["Framework/Aurora.Framework.Platform/Aurora.Framework.Platform.csproj", "Framework/Aurora.Framework.Platform/"]
COPY ["Framework/Aurora.Framework.Services/Aurora.Framework.Services.csproj", "Framework/Aurora.Framework.Services/"]
COPY ["Services/Platform/Aurora.Platform.Domain/Aurora.Platform.Domain.csproj", "Services/Platform/Aurora.Platform.Domain/"]
COPY ["Services/Platform/Aurora.Platform.Repositories/Aurora.Platform.Repositories.csproj", "Services/Platform/Aurora.Platform.Repositories/"]
COPY ["Services/Platform/Aurora.Platform.Services/Aurora.Platform.Services.csproj", "Services/Platform/Aurora.Platform.Services/"]
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
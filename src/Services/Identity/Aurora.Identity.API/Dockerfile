FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Services/Identity/Aurora.Identity.API/Aurora.Identity.API.csproj", "Services/Identity/Aurora.Identity.API/"]
COPY ["Framework/Aurora.Framework.Common/Aurora.Framework.Common.csproj", "Framework/Aurora.Framework.Common/"]
COPY ["Framework/Aurora.Framework.Logic/Aurora.Framework.Logic.csproj", "Framework/Aurora.Framework.Logic/"]
COPY ["Framework/Aurora.Framework.Platform/Aurora.Framework.Platform.csproj", "Framework/Aurora.Framework.Platform/"]
COPY ["Framework/Aurora.Framework.Services/Aurora.Framework.Services.csproj", "Framework/Aurora.Framework.Services/"]
COPY ["Services/Platform/Aurora.Platform.Domain/Aurora.Platform.Domain.csproj", "Services/Platform/Aurora.Platform.Domain/"]
COPY ["Services/Platform/Aurora.Platform.Repositories/Aurora.Platform.Repositories.csproj", "Services/Platform/Aurora.Platform.Repositories/"]
COPY ["Services/Platform/Aurora.Platform.Services/Aurora.Platform.Services.csproj", "Services/Platform/Aurora.Platform.Services/"]
RUN dotnet restore "Services/Identity/Aurora.Identity.API/Aurora.Identity.API.csproj"
COPY . .
WORKDIR "/src/Services/Identity/Aurora.Identity.API"
RUN dotnet build "Aurora.Identity.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Aurora.Identity.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Aurora.Identity.API.dll"]
FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Services/Common/Aurora.Common.API/Aurora.Common.API.csproj", "Services/Common/Aurora.Common.API/"]
COPY ["Framework/Aurora.Framework.Common/Aurora.Framework.Common.csproj", "Framework/Aurora.Framework.Common/"]
COPY ["Framework/Aurora.Framework.Logic/Aurora.Framework.Logic.csproj", "Framework/Aurora.Framework.Logic/"]
COPY ["Framework/Aurora.Framework.Services/Aurora.Framework.Services.csproj", "Framework/Aurora.Framework.Services/"]
COPY ["Services/Common/Aurora.Common.Domain/Aurora.Common.Domain.csproj", "Services/Common/Aurora.Common.Domain/"]
COPY ["Services/Common/Aurora.Common.Repositories/Aurora.Common.Repositories.csproj", "Services/Common/Aurora.Common.Repositories/"]
COPY ["Services/Common/Aurora.Common.Services/Aurora.Common.Services.csproj", "Services/Common/Aurora.Common.Services/"]
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
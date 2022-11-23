#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["ShopsRUs/ShopsRUs.csproj", "ShopsRUs/"]
COPY ["ShopsRUs.Core/ShopsRUs.Core.csproj", "ShopsRUs.Core/"]
COPY ["ShopsRUs.Domain/ShopsRUs.Domain.csproj", "ShopsRUs.Domain/"]
COPY ["ShopsRUs.Infrastructure/ShopsRUs.Infrastructure.csproj", "ShopsRUs.Infrastructure/"]
COPY ["ShopsRUs.Test/ShopsRUs.Test.csproj", "ShopsRUs.Test/"]
RUN dotnet restore "ShopsRUs/ShopsRUs.csproj"
COPY . .
WORKDIR "/src/ShopsRUs"
RUN dotnet build "ShopsRUs.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ShopsRUs.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
COPY --from=publish /src/ShopsRUs/ShopsRUs.xml ./
COPY --from=publish /src/ShopsRUs/ShopsRUs.pfx ./
ENTRYPOINT ["dotnet", "ShopsRUs.dll"]
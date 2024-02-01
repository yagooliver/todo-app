FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 5000

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
# COPY ["NuGet.config", "."]
COPY ./*.sln ./
COPY ["./src/IdentityServer/IdentityServer.Service/IdentityServer.Service.csproj", "IdentityServer.Service/"]
# COPY ["./src/TodoList/TodoList.Service/TodoList.Service.csproj", "IdentityServer.Service/"]
# COPY ["./src/TodoList/TodoList.Application/TodoList.Application.csproj", "IdentityServer.Service/"]
# COPY ["./src/TodoList/TodoList.Domain/TodoList.Domain.csproj", "IdentityServer.Service/"]
# COPY ["./src/TodoList/TodoList.Infra.Data.PostgresSQL/TodoList.Infra.Data.PostgresSQL.csproj", "IdentityServer.Service/"]
# COPY ["./src/Gateway/TodoList.Gateway.csproj", "Gateway/"]

RUN dotnet restore "./IdentityServer.Service/IdentityServer.Service.csproj"
COPY . .
# WORKDIR "/src/IdentityServer.Service"
FROM build AS publish
RUN dotnet publish -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
# COPY ["TBCA.cer", "/usr/local/share/ca-certificates/TBCA.crt"]
# RUN update-ca-certificates
ENTRYPOINT ["dotnet", "IdentityServer.Service.dll"]
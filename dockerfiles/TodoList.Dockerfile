FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 7001

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
# COPY ["NuGet.config", "."]
COPY ../../../*.sln ./
COPY ["./TodoList.Service/TodoList.Service.csproj", "TodoList.Service/"]
COPY ["./TodoList.Application/TodoList.Application.csproj", "TodoList.Application/"]
COPY ["./TodoList.Domain/TodoList.Domain.csproj", "TodoList.Domain/"]
COPY ["./TodoList.Infra.Data.PostgresSQL/TodoList.Infra.Data.PostgresSQL.csproj", "TodoList.Infra.Data.PostgresSQL/"]
RUN dotnet restore "./TodoList.Service/TodoList.Service.csproj"
COPY . .
WORKDIR "/src/TodoList.Service"
RUN dotnet build "./TodoList.Service.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "./TodoList.Service.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
# COPY ["TBCA.cer", "/usr/local/share/ca-certificates/TBCA.crt"]
# RUN update-ca-certificates
ENTRYPOINT ["dotnet", "TodoList.Service.dll"]
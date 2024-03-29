FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 5000

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
# COPY ["NuGet.config", "."]
COPY ./*.sln ./
COPY ["./src/Gateway/TodoList.Gateway.csproj", "Gateway/"]
RUN dotnet restore "./Gateway/TodoList.Gateway.csproj"
COPY . .
FROM build AS publish
RUN dotnet publish -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
# COPY ["TBCA.cer", "/usr/local/share/ca-certificates/TBCA.crt"]
# RUN update-ca-certificates
ENTRYPOINT ["dotnet", "TodoList.Gateway.dll"]
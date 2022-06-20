#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["Trucker.Gateway/Trucker.Gateway.csproj", "Trucker.Gateway/"]
RUN dotnet restore "Trucker.Gateway/Trucker.Gateway.csproj"
COPY . .
WORKDIR "/src/Trucker.Gateway"
RUN dotnet build "Trucker.Gateway.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Trucker.Gateway.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Trucker.Gateway.dll"]
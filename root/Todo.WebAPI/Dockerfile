#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# Creating a base layer
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app

# dotnet build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY . .
RUN dotnet build "./Todo.WebAPI/Todo.WebAPI.csproj" -c Release -o /app/build

# dotnet publish
FROM build AS publish
RUN dotnet publish "./Todo.WebAPI/Todo.WebAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Todo.WebAPI.dll"]
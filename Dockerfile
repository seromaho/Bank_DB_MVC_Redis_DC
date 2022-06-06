#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["Bank_DB_MVC_Redis_DC.csproj", "."]
RUN dotnet restore "Bank_DB_MVC_Redis_DC.csproj"
COPY . .
WORKDIR "/src/"
RUN dotnet build "Bank_DB_MVC_Redis_DC.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Bank_DB_MVC_Redis_DC.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Bank_DB_MVC_Redis_DC.dll"]

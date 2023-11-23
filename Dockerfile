#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443
EXPOSE 1433

ENV DOTNET_ENVIRONMENT="Production"

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/Server/Blanche.Server.csproj", "src/Server/"]
COPY ["src/Client/Blanche.Client.csproj", "src/Client/"]
COPY ["src/Shared/Blanche.Shared.csproj", "src/Shared/"]
COPY ["src/Domain/Blanche.Domain.csproj", "src/Domain/"]
COPY ["src/Mappers/Blanche.Mappers.csproj", "src/Mappers/"]
RUN dotnet restore "src/Server/Blanche.Server.csproj"
COPY . .
#RUN dotnet test
WORKDIR "/src/src/Server"
RUN dotnet build "Blanche.Server.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Blanche.Server.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Blanche.Server.dll"]

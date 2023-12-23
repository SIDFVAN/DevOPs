#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443
EXPOSE 1433

ENV DOTNET_ENVIRONMENT="Production"

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
RUN apt update \
    && apt install -y sudo libxkbcommon-x11-0 libc6 libc6-dev libgtk2.0-0 libnss3 libatk-bridge2.0-0 libx11-xcb1 libxcb-dri3-0 libdrm-common libgbm1 libasound2 libxrender1 libfontconfig1 libxshmfence1 libgdiplus libva-dev

WORKDIR /src
COPY ["src/Server/Blanche.Server.csproj", "src/Server/"]
COPY ["src/Client/Blanche.Client.csproj", "src/Client/"]
COPY ["src/Shared/Blanche.Shared.csproj", "src/Shared/"]
COPY ["src/Domain/Blanche.Domain.csproj", "src/Domain/"]
COPY ["src/Mappers/Blanche.Mappers.csproj", "src/Mappers/"]
COPY ["tests/tests.csproj", "tests/"]
COPY . .
RUN dotnet restore "src/Server/Blanche.Server.csproj"
COPY . .
 #RUN dotnet test
WORKDIR "/src/src/Server"
RUN dotnet build "Blanche.Server.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Blanche.Server.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app/publish
COPY --from=publish /app/publish .
RUN chmod 775 /app/publish/runtimes/linux-x64/native/IronCefSubprocess
ENTRYPOINT ["dotnet", "Blanche.Server.dll"]

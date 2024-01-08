FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 80
EXPOSE 443
ENV ASPNETCORE_URLS=http://+:80
#ENV ASPNETCORE_URLS=http://+:80;https://+:433

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["AICook.Gateway/AICook.Gateway.csproj", "AICook.Gateway/"]
RUN dotnet restore "AICook.Gateway/AICook.Gateway.csproj"
COPY . .
RUN dotnet build "AICook.Gateway/AICook.Gateway.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "AICook.Gateway/AICook.Gateway.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AICook.Gateway.dll"]

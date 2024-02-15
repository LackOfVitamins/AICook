FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 5020
ENV ASPNETCORE_URLS=http://+:5020

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["AICook.Model/AICook.Model.csproj", "AICook.Model/"]
COPY ["AICook.Event/AICook.Event.csproj", "AICook.Event/"]
COPY ["AICook.Identity/AICook.Identity.csproj", "AICook.Identity/"]
RUN dotnet restore "AICook.Identity/AICook.Identity.csproj"
COPY . .
RUN dotnet build "AICook.Identity/AICook.Identity.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "AICook.Identity/AICook.Identity.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AICook.Identity.dll"]

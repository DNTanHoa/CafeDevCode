#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/CafeDevCode.Website/CafeDevCode.Website.csproj", "src/CafeDevCode.Website/"]
RUN dotnet restore "src/CafeDevCode.Website/CafeDevCode.Website.csproj"
COPY . .
WORKDIR "/src/src/CafeDevCode.Website"
RUN dotnet build "CafeDevCode.Website.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CafeDevCode.Website.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CafeDevCode.Website.dll"]
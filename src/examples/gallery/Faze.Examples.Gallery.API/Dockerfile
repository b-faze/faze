#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["examples/gallery/Faze.Examples.Gallery.API/Faze.Examples.Gallery.API.csproj", "examples/gallery/Faze.Examples.Gallery.API/"]
RUN dotnet restore "examples/gallery/Faze.Examples.Gallery.API/Faze.Examples.Gallery.API.csproj"
COPY . .
WORKDIR "/src/examples/gallery/Faze.Examples.Gallery.API"
RUN dotnet build "Faze.Examples.Gallery.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Faze.Examples.Gallery.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Faze.Examples.Gallery.API.dll"]
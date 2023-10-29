#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/services/ECE.Customer.API/ECE.Customer.API.csproj", "src/services/ECE.Customer.API/"]
RUN dotnet restore "src/services/ECE.Customer.API/ECE.Customer.API.csproj"
COPY . .
WORKDIR "/src/src/services/ECE.Customer.API"
RUN dotnet build "ECE.Customer.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ECE.Customer.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ECE.Customer.API.dll"]
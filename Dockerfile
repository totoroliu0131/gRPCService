﻿FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["GrpcService/GrpcService.csproj", "GrpcService/"]
RUN dotnet restore "GrpcService/GrpcService.csproj"
COPY . .
WORKDIR "/src/GrpcService"
RUN dotnet build "GrpcService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "GrpcService.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
RUN sed -i 's/DEFAULT@SECLEVEL=2/DEFAULT@SECLEVEL=1/g' /etc/ssl/openssl.cnf
ENTRYPOINT ["dotnet", "GrpcService.dll"]

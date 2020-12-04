#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build

# copy csproj and restore as distinct layers
COPY GameLoanManager.API/GameLoanManager.API.csproj GameLoanManager.API/
RUN dotnet restore GameLoanManager.API/GameLoanManager.API.csproj
COPY . ./
WORKDIR /GameLoanManager.API

FROM build AS publish
RUN dotnet publish GameLoanManager.API.csproj -c Release -o /app

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS final
WORKDIR /app

COPY --from=publish /app .

EXPOSE 80
ENTRYPOINT ["dotnet", "GameLoanManager.API.dll"]


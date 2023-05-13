#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
ENV TZ="America/Sao_Paulo"
# force docker to install the ms core fonts.
#https://github.com/ststeiger/PdfSharpCore/issues/161
RUN apt-get update; apt-get install -y fontconfig fonts-liberation
RUN fc-cache -f -v

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src


COPY ["ApiPlaceHolderDemo.csproj", "."]
RUN dotnet restore "./ApiPlaceHolderDemo.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "ApiPlaceHolderDemo.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ApiPlaceHolderDemo.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ApiPlaceHolderDemo.dll"]
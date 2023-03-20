#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/nightly/sdk:8.0-preview AS build
ARG TARGETARCH
COPY . /src
WORKDIR /src
RUN dotnet restore "flamflam.SubmissionService.csproj" -a $TARGETARCH
RUN dotnet build "flamflam.SubmissionService.csproj" -c Release -a $TARGETARCH -o /app/build

FROM build AS publish
ARG TARGETARCH
RUN dotnet publish "flamflam.SubmissionService.csproj" -c Release -a $TARGETARCH -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "flamflam.SubmissionService.dll"]
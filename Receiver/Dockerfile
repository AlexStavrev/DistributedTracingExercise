FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Receiver/Receiver.csproj", "Receiver/"]
RUN dotnet restore "Receiver/Receiver.csproj"
COPY . .
WORKDIR "/src/Receiver"
RUN dotnet build "Receiver.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Receiver.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Receiver.dll"]

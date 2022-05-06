FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY "ECom.sln" "ECom.sln"
COPY "Services/Balance/Balance.App.csproj" "Services/Balance/Balance.App.csproj"
COPY "Services/Balance/Balance.Domain.csproj" "Services/Balance/Balance.Domain.csproj"
COPY "Services/Balance/Balance.Infrastructure.csproj" "Services/Balance/Balance.Infrastructure.csproj"
RUN dotnet restore "ECom.sln" -r linux-musl-x64
COPY . .
WORKDIR /src/Services/Balance
RUN dotnet publish -r linux-musl-x64 -c Release -o /publish --self-contained false --no-restore
FROM mcr.microsoft.com/dotnet/aspnet:6.0    
WORKDIR /app
COPY --from=build /publish .
EXPOSE 80
ENTRYPOINT ["dotnet","Balance.dll"]
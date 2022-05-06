FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src

COPY "ECom.sln" "ECom.sln"
COPY "Services/Balance/Balance.App/Balance.App.csproj" "Services/Balance/Balance.App/Balance.App.csproj"
COPY "Services/Balance/Balance.Domain/Balance.Domain.csproj" "Services/Balance/Balance.Domain/Balance.Domain.csproj"
COPY "Services/Balance/Balance.Infrastructure/Balance.Infrastructure.csproj" "Services/Balance/Balance.Infrastructure/Balance.Infrastructure.csproj"

COPY "Services/Catalog/Catalog.App/Catalog.App.csproj" "Services/Catalog/Catalog.App/Catalog.App.csproj"
COPY "Services/Catalog/Catalog.Domain/Catalog.Domain.csproj" "Services/Catalog/Catalog.Domain/Catalog.Domain.csproj"
COPY "Services/Catalog/Catalog.Infrastructure/Catalog.Infrastructure.csproj" "Services/Catalog/Catalog.Infrastructure/Catalog.Infrastructure.csproj"

COPY "Services/Ordering/Ordering.App/Ordering.App.csproj" "Services/Ordering/Ordering.App/Ordering.App.csproj"
COPY "Services/Ordering/Ordering.Domain/Ordering.Domain.csproj" "Services/Ordering/Ordering.Domain/Ordering.Domain.csproj"
COPY "Services/Ordering/Ordering.Infrastructure/Ordering.Infrastructure.csproj" "Services/Ordering/Ordering.Infrastructure/Ordering.Infrastructure.csproj"
COPY "Services/Ordering/Ordering.Tests.Functional/Ordering.Tests.Functional.csproj" "Services/Ordering/Ordering.Tests.Functional/Ordering.Tests.Functional.csproj"
COPY "Services/Ordering/Ordering.Tests.Unit/Ordering.Tests.Unit.csproj" "Services/Ordering/Ordering.Tests.Unit/Ordering.Tests.Unit.csproj"

COPY "Tests/Services/Application.FunctionalTests/Application.FunctionalTests.csproj" "Tests/Services/Application.FunctionalTests/Application.FunctionalTests.csproj"

COPY "	" .
RUN dotnet restore "Services/Balance/Balance.App/Balance.App.csproj" -r linux-musl-x64
COPY . .

WORKDIR /src/Services/Balance
RUN dotnet publish -r linux-musl-x64 -c Release -o /publish --self-contained false --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:6.0    
WORKDIR /app
COPY --from=build /publish .
EXPOSE 80
ENTRYPOINT ["dotnet","Balance.dll"]
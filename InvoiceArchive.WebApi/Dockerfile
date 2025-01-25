FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["InvoiceArchive.WebAPI/InvoiceArchive.WebAPI.csproj", "InvoiceArchive.WebAPI/"]
COPY ["InvoiceArchive.Application/InvoiceArchive.Application.csproj", "InvoiceArchive.Application/"]
COPY ["InvoiceArchive.Domain/InvoiceArchive.Domain.csproj", "InvoiceArchive.Domain/"]
COPY ["InvoiceArchive.Infrastructure/InvoiceArchive.Infrastructure.csproj", "InvoiceArchive.Infrastructure/"]
COPY ["InvoiceArchive.Persistence/InvoiceArchive.Persistence.csproj", "InvoiceArchive.Persistence/"]
RUN dotnet restore "InvoiceArchive.WebAPI/InvoiceArchive.WebAPI.csproj"
COPY . .
WORKDIR "/src/InvoiceArchive.WebAPI"
RUN dotnet build "InvoiceArchive.WebAPI.csproj" -c Debug -o /app/build

FROM build AS publish
RUN dotnet publish "InvoiceArchive.WebAPI.csproj" -c Debug -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "InvoiceArchive.WebAPI.dll"]
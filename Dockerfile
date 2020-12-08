FROM mcr.microsoft.com/dotnet/core/sdk:3.1
ARG BUILD_CONFIGURATION=Debug
ENV ASPNETCORE_ENVIRONMENT=Development
ENV DOTNET_USE_POLLING_FILE_WATCHER=true  
ENV ASPNETCORE_URLS=http://+:80  
EXPOSE 80

WORKDIR /Travel
 
# Copy csproj and restore as distinct layers

COPY ["Travel/Travel.csproj","Travel/"]
COPY ["Travel.Api.Common/Travel.Api.Common.csproj","Travel.Api.Common/"]
COPY ["Travel.Api.IntegrationTests/Travel.Api.IntegrationTests.csproj","Travel.Api.IntegrationTests/"]
COPY ["Travel.Api.Models/Travel.Api.Models.csproj","Travel.Api.Models/"]
COPY ["Travel.Communication/Travel.Communication.csproj","Travel.Communication/"]
COPY ["Travel.Contracts/Travel.Contracts.csproj","Travel.Contracts/"]
COPY ["Travel.Data.Access/Travel.Data.Access.csproj","Travel.Data.Access/"]
COPY ["Travel.Data.Model/Travel.Data.Model.csproj","Travel.Data.Model/"]
COPY ["Travel.Queries/Travel.Queries.csproj","Travel.Queries/"]
COPY ["Travel.Queries.Tests/Travel.Queries.Tests.csproj","Travel.Queries.Tests/"]
COPY ["Travel.Security/Travel.Security.csproj","Travel.Security/"]
RUN dotnet restore "Travel/Travel.csproj"

# Copy everything else and build
COPY . ./
RUN dotnet publish -c Release -o out
 
# Build runtime image
FROM mcr.microsoft.com/dotnet/core/sdk:3.1
WORKDIR /Travel
COPY --from=build-env /Travel/out .
ENTRYPOINT ["dotnet", "Travel.dll"]
 




FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build

WORKDIR /src

COPY *.sln .
COPY TheRide.API/*.csproj ./TheRide.API/
COPY TheRide.API.UnitTests/*.csproj ./TheRide.API.UnitTests/
RUN dotnet restore

COPY TheRide.API TheRide.API/
COPY TheRide.API.UnitTests TheRide.API.UnitTests/

# Run unit tests
ARG RESULTS_FOLDER="/TestResults/UnitTests"
ARG COVERAGE_FOLDER="$RESULTS_FOLDER/coverage.opencover.xml"
RUN mkdir -p ${RESULTS_FOLDER} && \
    (dotnet test -c Release --no-restore --logger trx --results-directory "$RESULTS_FOLDER" /p:CoverletOutput="$COVERAGE_FOLDER" /p:CollectCoverage=true /p:CoverletOutputFormat=opencover || echo "Tests failed")

# Run build
RUN dotnet build -c Release --no-restore \
    && dotnet publish -c Release --no-restore --no-build TheRide.API/TheRide.API.csproj

FROM build as artifacts
WORKDIR /src
COPY --from=build /TestResults/ /TestResults/
CMD sh -c 'cp -r /TestResults/* /artifacts'

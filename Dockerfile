FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build-env
WORKDIR /app

# Copy everything
COPY CodingTest/appsettings.json .
#COPY CodingTest/matches.txt .
COPY . ./
# Restore as distinct layers
RUN dotnet restore
# Build and publish a release
RUN dotnet publish -c Release -o /app/CodingTest

RUN cp /app/CodingTest/appsettings.json .
#RUN cp /app/matches.txt .

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:5.0
#RUN mkdir -p /app/downloads
WORKDIR /app
COPY --from=build-env /app .
COPY --from=build-env /app/CodingTest /CodingTest/appsettings.json
#COPY --from=build-env /app /matches.txt

ENTRYPOINT ["dotnet", "./CodingTest/CodingChallenge.dll"]
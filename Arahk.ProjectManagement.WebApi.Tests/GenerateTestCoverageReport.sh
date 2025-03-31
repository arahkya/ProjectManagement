#!/bin/bash

echo "Generate Test Coverage Report"
echo "-------------------------------------"
echo "Clearing previous coverage report..."
rm -rf ./TestResults
rm -rf ./TestHtmlReport

echo "Running dotnet test with coverage..."
dotnet test --collect:"XPlat Code Coverage" --results-directory ./TestResults

# get first directory in TestResults
dir=$(ls -d ./TestResults/*/ | head -n 1)

# dotnet tool install -g dotnet-reportgenerator-globaltool

reportgenerator -reports:$dir"coverage.cobertura.xml" -targetdir:"TestHtmlReport" -reporttypes:Html

open TestHtmlReport/index.html
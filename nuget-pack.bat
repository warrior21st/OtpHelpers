del .\nuget\*.nupkg
dotnet pack  ./src/OtpHelpers/OtpHelpers.csproj  --output ../../nuget --configuration Release
pause
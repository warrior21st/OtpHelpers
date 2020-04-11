@rem 发布到nuget服务器
dotnet nuget push .\nuget\*.nupkg -k [key] -s http://xxx:123
pause
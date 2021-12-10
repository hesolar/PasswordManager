FROM mcr.microsoft.com/dotnet/sdk:5.0
COPY bin/Release/net5.0/publish/ ConsoleApp2/
WORKDIR /ConsoleApp2
ENTRYPOINT ["dotnet", "PassManager.dll"]
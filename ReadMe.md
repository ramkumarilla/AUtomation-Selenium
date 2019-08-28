# To run selenium in .netcore using Firefox
## First of all you need to successfully run this selenium solution on your local machine to test it out
```git clone <this-git-repo>```
* This repo contains gecko driver (0.24 for MacOS) under drivers folder. if you are using windows then download driver for windows and out it under same folder - no need to change the code code works for both platforms.
## Make sure you have Firefox browser installed on your Machine
* Now just restore the packages and build
* ```dotnet restore && dotnet build```
* And we are good to run test
* ```dotnet vstest bin/Debug/netcoreapp2.2/automation-nunit.dll /Settings:custom.runsettings```
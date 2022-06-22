@echo off
dotnet build src/Umbraco.ShareX.CustomUploader --configuration Release /t:rebuild /t:pack -p:BuildTools=1 -p:PackageOutputPath=../../releases/nuget

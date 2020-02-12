# <img src="https://raw.githubusercontent.com/prospa-group/DotnetPackaging/master/prospa60x60.png" alt="Prospa Engineering" width="50px"/> Prospa dotnet new Templates 

|Branch|Azure Devops|
|------|:--------:|
|dev|[![Build Status](https://dev.azure.com/prospaoss/dotnet/_apis/build/status/prospa-group-oss.DotnetTemplates?branchName=master)](https://dev.azure.com/prospaoss/dotnet/_build/latest?definitionId=1&branchName=dev)
|master|[![Build Status](https://dev.azure.com/prospaoss/dotnet/_apis/build/status/prospa-group-oss.DotnetTemplates?branchName=master)](https://dev.azure.com/prospaoss/dotnet/_build/latest?definitionId=1&branchName=master)|

## Install from Nuget

```console
dotnet new --install Prospa.Templates::*
```

Confirm templates are installed:

```console
dotnet new list
``` 

## Project Templates Included

### AspNetCore API

#### Create a new solution to add the project template to:

See details [here](https://github.com/prospa-group/DotnetSolution)

#### Create a new AspNetCore API using the template:

```console
cd src
dotnet new prospaapi -n "MyNew.API" 
--keyvaultName {Keyvault name is required, don't include the environment prefix or use the DNS name. e.g. template-keyvault}
```

#### Attach the API to the solution

```console
cd ..
dotnet sln add .\src\MyNew.API\MyNew.API.csproj
```

> The template is configured to add secrets from Azure Keyvault using MSI. Before running the application you'll first need to either:
> 1. Install the [Azure CLI](https://docs.microsoft.com/en-us/cli/azure/install-azure-cli?view=azure-cli-latest) and then run `az login` OR
> 2. Disable this by removing the `keyvaultName` key from `appsettings.{env}.json`

## How to Build

```csharp
./build.ps1 -Target Local
```

Package will be output to `/.artifacts/packages/Prospa.Templates.{version}.nupkg`

## Test locally

After building the project, the templates can be installed using the following:

### Install

```console
dotnet new -i <path to project>\.artifacts\packages\Prospa.Templates.{version}.nupkg
```

### Re-Install

```console
dotnet new -u Prospa.Templates
dotnet new -i <path to project>\.artifacts\packages\Prospa.Templates.{version}.nupkg
```

### Install location

`%USERPROFILE%\.templateengine\dotnetcli\<SDK version>`

## Versioning

To version bump, edit the `VersionPrefix` and/or `VersionSuffix` in `./version.props`

> When on non-release branches the `VersionSuffix` will always be set to `alpha` with the build number appended.

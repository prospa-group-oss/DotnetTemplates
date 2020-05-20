### Worker - Azure

This template scaffolds a worker which standardises the following:
- Azure Application Insights for application traces and telemtry
- Azure Keyvault for storing application secrets
- Log configuration and enrichment with optional Datadog integration

#### Create a new solution to add the project template to:

See details [here](https://github.com/prospa-group/DotnetSolution)

#### Create a new project using the template:

```console
cd src
dotnet new prospaworker -n "MyNew.Worker" 
--keyvaultName {Keyvault name is required, don't include the environment prefix or use the DNS name. e.g. template-keyvault}
--appDomain {Application domain name for tagging log entries}
```

#### Attach the project to the solution

```console
cd ..
dotnet sln add .\src\MyNew.Worker\MyNew.Worker.csproj
```

#### Configure App Configuration

The template is configured to add Azure App Configuration as a configuration provider. You'll need add the endpoints to appsettings i.e. add the Azure App Configuration URL setting the `SharedAzureAppConfigurationEndpoint` value in appsettings.
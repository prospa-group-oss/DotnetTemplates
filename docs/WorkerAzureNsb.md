### Worker - Azure - NServiceBus

This template scaffolds a NServiceBus endpoint worker which standardises the following:

- Azure Application Insights for application traces and telemtry
- Azure Keyvault for storing application secrets
- Log configuration and enrichment with optional Datadog integration
- NServiceBus Endpoint Configuration

#### Create a new solution to add the project template to:

See details [here](https://github.com/prospa-group/DotnetSolution)

#### Create a new project using the template:

```console
cd src
dotnet new prospaworkernsb -n "MyNew.Worker"
--appDomain {Application domain name for tagging log entries}
```

#### Attach the project to the solution

```console
cd ..
dotnet sln add .\src\MyNew.Worker\MyNew.Worker.csproj
```
#### Configure App Configuration

The template is configured to add Azure App Configuration as a configuration provider. You'll need add the endpoints to appsettings i.e. add the Azure App Configuration URL setting the `SharedAzureAppConfigurationEndpoint` value in appsettings.
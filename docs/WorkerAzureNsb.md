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
dotnet new prospaworkersnb -n "MyNew.Worker" 
--keyvaultName {Keyvault name is required, don't include the environment prefix or use the DNS name. e.g. template-keyvault}
--appDomain {Application domain name for tagging log entries}
```

#### Attach the project to the solution

```console
cd ..
dotnet sln add .\src\MyNew.Worker\MyNew.Worker.csproj
```

#### Configure Azure Keyvault

The template is configured to add Azure Keyvault as a configuration provider. You'll need to create the KeyVault resource for the application and use MSI to connect.

Install the [Azure CLI](https://docs.microsoft.com/en-us/cli/azure/install-azure-cli?view=azure-cli-latest) and then run `az login`

```
rg="demo-mygroupname-rg"
keyvaultName="keyvaultName"
az group create -l australiaeast -n $rg
az keyvault create -n $keyvaultName -g $rg -l australiaeast
az keyvault secret set --vault-name $keyvaultName -n "EndpointKey" --value "secret"
az keyvault secret set --vault-name $keyvaultName -n "DataDogApiKey" --value "DataDog api key"
```
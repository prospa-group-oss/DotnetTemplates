### AspNetCore API with NServiceBus Integration - Azure

This template scaffolds an ASP.NET core API which standardises the following:
- A health checks where the status is provided at `/health?EndpointKey=your-secret`
- A a ping endpoint at `/ping`
- OAuth2 for authorization
- Swagger for API documentation
- Azure Application Insights for application traces and telemtry
- [Problem+Json](https://tools.ietf.org/html/rfc7807) for error responses
- API versioning via URLs
- Azure Keyvault for storing application secrets
- OWASP recommended security headers
- Request details logging on failed requests, including the ability to scrub sensitive request headers such as `Authorization` 
- Log Enrichment middleware providing structured log entries which includes entries such `CorrelationId`, `ClientId`, `SubjectId`
- Configures NServiceBus with Azure ServiceBus integration.

#### Create a new solution to add the project template to:

See details [here](https://github.com/prospa-group/DotnetSolution)

#### Create a new project using the template:

```console
cd src
dotnet new prospaapiazurensb -n "MyNew.API" 
--keyvaultName {Keyvault name is required, don't include the environment prefix or use the DNS name. e.g. template-keyvault}
--appDomain {Application domain name for tagging log entries}
```

#### Attach the project to the solution

```console
cd ..
dotnet sln add .\src\MyNew.API\MyNew.API.csproj
```

#### Create an Azure Resource Group

An Azure Resource Group is required to hold Azure Keyvault and Azure Service Bus resources. To create a Resource Group:

1. Install the [Azure CLI](https://docs.microsoft.com/en-us/cli/azure/install-azure-cli?view=azure-cli-latest) and then run `az login`

2. Create the resource group
```
location=australiaeast
rg=demo-mygroupname-rg
az group create -l $location -n $rg
```

#### Configure Azure Service Bus

NServiceBus in the template is configured to use Azure Service Bus. To create an Azure Service Bus Resource create the namespace and keep the connection string to store in Keyvault

```
namespaceName="demo-myservicebusname-sb"
az servicebus namespace create --resource-group $rg --name "demo-myservicebusname-sb" --location $location
serviceBusConnectionString=$(az servicebus namespace authorization-rule keys list -g $rg --namespace-name $namespaceName --name RootManageSharedAccessKey --query primaryConnectionString --output tsv)
```

#### Configure App Configuration

The template is configured to add Azure App Configuration as a configuration provider. You'll need add the endpoints to appsettings i.e. add the Azure App Configuration URL setting the `SharedAzureAppConfigurationEndpoint` value in appsettings.
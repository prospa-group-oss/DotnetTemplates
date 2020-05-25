### AspNetCore API - Azure

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

#### Create a new solution to add the project template to:

See details [here](https://github.com/prospa-group/DotnetSolution)

#### Create a new project using the template:

```console
cd src
dotnet new prospaapiazure -n "MyNew.API"
--appDomain {Application domain name for tagging log entries}
```

#### Attach the project to the solution

```console
cd ..
dotnet sln add .\src\MyNew.API\MyNew.API.csproj
```

#### Configure App Configuration

The template is configured to add Azure App Configuration as a configuration provider. You'll need add the endpoints to appsettings i.e. add the Azure App Configuration URL setting the `SharedAzureAppConfigurationEndpoint` value in appsettings.
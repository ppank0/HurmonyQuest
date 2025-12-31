using ApplicationService.Functions.Services;
using Microsoft.Azure.Functions.Worker.Builder;
using Microsoft.Extensions.Azure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = FunctionsApplication.CreateBuilder(args);

builder.ConfigureFunctionsWebApplication();

// Application Insights isn't enabled by default. See https://aka.ms/AAt8mw4.
// builder.Services
//     .AddApplicationInsightsTelemetryWorkerService()
//     .ConfigureFunctionsApplicationInsights();

var azureBlobConnectionString = builder.Configuration.GetConnectionString("AzureBlobConnection");
builder.Services.AddAzureClients(azureBuilder =>
{
    azureBuilder.AddBlobServiceClient(azureBlobConnectionString);
    azureBuilder.AddTableServiceClient(azureBlobConnectionString);
});

builder.Services.AddScoped<IVideoMetadataTableService, VideoMetadataTableService>();

builder.Build().Run();

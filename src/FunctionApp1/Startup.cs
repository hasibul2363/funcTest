using System;
using System.Collections.Generic;
using System.Text;
using FunctionApp1;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: FunctionsStartup(typeof(Startup))]
namespace FunctionApp1
{
    public class Startup : FunctionsStartup
    {
        private IConfiguration _configuration;
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddSingleton<EnvironmentInfo>(new EnvironmentInfo
                {Environment = builder.GetContext().EnvironmentName});
            builder.Services.Configure<AuthenticationSettings>(_configuration.GetSection("AuthenticationSettings"));

        }
        public override void ConfigureAppConfiguration(IFunctionsConfigurationBuilder builder)
        {
            var context = builder.GetContext();
            var environment = Environment.GetEnvironmentVariable("AZURE_FUNCTIONS_ENVIRONMENT");
            builder.ConfigurationBuilder
                .SetBasePath(context.ApplicationRootPath)
                .AddJsonFile($"appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{environment}.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables();
            _configuration = builder.ConfigurationBuilder.Build();
        }
    }

    public class AuthenticationSettings
    {
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public Dictionary<string, string> HostAndResourceMappings { get; set; }
        public string AuthUrl { get; set; }
    }

    public class EnvironmentInfo
    {
        public string Environment { get; set; }

    }
}

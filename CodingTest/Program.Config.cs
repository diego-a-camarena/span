using CodingChallenge.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.IO;
using CodingChallengeServices.Interfaces;
using CodingChallengeServices.Services;
using Microsoft.Extensions.Options;

namespace CodingChallenge
{
    partial class Program
    {
        private static void StartUp()
        {
            SetConfiguration();
            ConfigureIoC();
        }
        

        private static void SetConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        private static void ConfigureIoC()
        {
            var services = new ServiceCollection();
            services.Configure<FilePath>(Configuration.GetSection("FilePath"));
            services.AddScoped<IFile, FileService>();
            services.AddScoped<ILeague, LeagueService>();
            ServiceProvider = services.BuildServiceProvider();
            FilePath = ServiceProvider.GetService<IOptions<FilePath>>().Value;
            FileService = ServiceProvider.GetService<IFile>();
            LeagueService = ServiceProvider.GetService<ILeague>();
        }
    }
}

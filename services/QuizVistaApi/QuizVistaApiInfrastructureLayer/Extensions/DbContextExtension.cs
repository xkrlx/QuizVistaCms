using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using QuizVistaApiInfrastructureLayer.DbContexts;
using System;
using System.Diagnostics;

namespace QuizVistaApiInfrastructureLayer.Extensions
{
    public static class DbContextExtension
    {
        public static void ConfigureDatabaseConnection(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<QuizVistaDbContext>(builder =>
            {
                ConfigureDbOptions(builder, configuration);
            });
        }

        private static void ConfigureDbOptions(DbContextOptionsBuilder builder, IConfiguration configuration)
        {
            builder
                .UseSqlServer(configuration.GetSection("connectionStrings")["QUIZ_VISTA"])
                .LogTo(message => Debug.WriteLine(message))
                .EnableDetailedErrors();

        }
    }
}

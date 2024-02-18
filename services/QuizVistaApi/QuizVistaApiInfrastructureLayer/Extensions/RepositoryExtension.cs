using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using QuizVistaApiInfrastructureLayer.Attributes;
using QuizVistaApiInfrastructureLayer.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace QuizVistaApiInfrastructureLayer.Extensions
{
    public static class RepositoryExtension
    {
        public static void AddRepositories(this IServiceCollection services)
        {
            var entityTypes = typeof(RepositoryExtension).Assembly.GetTypes()
            .Where(t => t.GetCustomAttribute<EntityAttribute>() != null && !t.IsAbstract)
            .ToList();

            
            foreach (var entityType in entityTypes)
            {


                var repositoryInterfaceType = typeof(IRepository<>).MakeGenericType(entityType);
                var repositoryImplementationType = typeof(Repository<>).MakeGenericType(entityType);

                services.AddScoped(repositoryInterfaceType, repositoryImplementationType);
            }

        }
    }
}

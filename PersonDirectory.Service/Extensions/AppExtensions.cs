using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PersonDirectory.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace PersonDirectory.Service.Extensions
{
    public static class AppExtensions
    {
        public static IServiceCollection SetUpDALDependencies(this IServiceCollection serviceCollection, string connectionString)
        {
            serviceCollection.AddDbContext<PersonDirectoryContext>(options => options.UseSqlServer(connectionString).UseLazyLoadingProxies(false));
            return serviceCollection;
        }
    }
}

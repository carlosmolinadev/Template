using Infrastructure.Persistance.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;
using SimpleTemplate.Application.Contracts;
using SimpleTemplate.Persistance.Repositories;
using System.Data.Common;

namespace SimpleTemplate.Persistance
{
    public static class PersistenceServiceRegistration
    {
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<DbConnection>((sp) => new NpgsqlConnection(configuration.GetConnectionString("DefaultConnection")));
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

            return services;
        }
    }
}

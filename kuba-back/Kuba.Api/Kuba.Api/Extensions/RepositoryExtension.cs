using Kuba.Domain.Interfaces;
using Kuba.Infra.Repositories;

namespace Kuba.Api.Extensions
{
    public static class RepositoryExtension
    {
        public static IServiceCollection AddRepositoryExtensions(this IServiceCollection services)
        {
            services.AddScoped<IIncidentRepository, IncidentRepository>();
            return services;
        }
    }
}

using api.layer.BusinessLayer;
using api.layer.DataAccessLayer;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;

namespace api.layer.Extensions
{
    /// <summary>
    /// Service Extension Class for Registering Services
    /// </summary>
    [ExcludeFromCodeCoverage]
    public static class ServiceExtensions
    {
        /// <summary>
        /// Service Extension Method for Registering Services
        /// </summary>
        /// <param name="services">Services Registered in API</param>
        /// <returns>Serice Collection for DataAccess/Repository/Business Layer</returns>
        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            services.AddTransient<IGitActionsManager, GitActionsManager>();
            services.AddTransient<IGitActionsDAO, GitActionsDAO>();
            return services;
        }
    }
}
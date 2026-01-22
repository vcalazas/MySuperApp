using MSP.Data.Repositories;
using MSP.Domain.Business;
using MSP.Domain.Repositories;

namespace MSP.API.Services
{
    public static class ServicesConfiguration
    {

        public static IServiceCollection ConfigureRepositories(this IServiceCollection services)
        {
            services.AddTransient<ISystemSettingsRepository, SystemSettingsRepository>();
            return services;
        }

        public static IServiceCollection ConfigureBusiness(this IServiceCollection services)
        {
            services.AddTransient<ISystemSettingsBusiness, SystemSettingsBusiness>();
            return services;
        }

    }
}

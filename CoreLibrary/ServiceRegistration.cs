
using CoreLibrary.Features;
using CoreLibrary.Interface.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Microservice.core;

public static class ServiceRegistration
{
    public static void AddCoreLayer(this IServiceCollection services)
    {
        services.AddTransient<IMenuService, MenuService>();
        services.AddTransient<IOrderService, OrderService>();
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
    }
}

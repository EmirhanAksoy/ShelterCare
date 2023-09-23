using Microsoft.Extensions.DependencyInjection;
namespace ShelterCare.Application.Extensions;
public static class AddMediatRExtension
{
    public static void AddMediatR(this IServiceCollection services)
    {
        var assembly = AppDomain.CurrentDomain.Load("ShelterCare.Application");
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(assembly));
    }
}

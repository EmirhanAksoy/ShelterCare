using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace ShelterCare.Application.Extensions;

public static class AddFluentValidationExtension
{
    public static void AddFluentValidation(this IServiceCollection services)
    {
        services.AddValidatorsFromAssemblyContaining<CreateShelterCommandValidation>();
        services.AddValidatorsFromAssemblyContaining<CreateAnimalSpecieCommandValidation>();
        services.AddValidatorsFromAssemblyContaining<CreateAnimalOwnerCommandValidation>();
    }
}

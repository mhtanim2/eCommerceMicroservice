
using BusinessLoginLayer.Mappers;
using BusinessLoginLayer.RabbitMQ;
using BusinessLoginLayer.ServiceContracts;
using BusinessLoginLayer.Services;
using BusinessLoginLayer.Validators;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace BusinessLoginLayer;

public static class DependencyInjection
{
    public static IServiceCollection AddBusinessLogicLayer(this IServiceCollection services) 
    {
        services.AddAutoMapper(typeof(ProductAddRequestToProductMappingProfile).Assembly);
        services.AddValidatorsFromAssemblyContaining<ProductAddRequestValidator>();

        services.AddScoped<IProductsService, ProductsService>();
        services.AddTransient<IRabbitMQPublisher, RabbitMQPublisher>();
        return services;
    }
}

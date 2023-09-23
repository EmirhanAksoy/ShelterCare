using Microsoft.Extensions.DependencyInjection;
using Npgsql;
using System.Data;

namespace ShelterCare.Infrastructure.Repository.Extensions;

public static class NpgsqlConnectionConnectionExtension
{
    public static void AddNpgsqlConnection(this IServiceCollection serviceCollection,string connectionString)
    {
        serviceCollection.AddTransient<IDbConnection>(_ =>
        {
            return new NpgsqlConnection(connectionString); 
        });
    }
}

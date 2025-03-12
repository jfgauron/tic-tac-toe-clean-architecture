using TicTacToe.Application.Interfaces;
using TicTacToe.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace TicTacToe.Presentation;

public static class InfrastructureDependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
      services.AddScoped(typeof(IRepository<,>), typeof(InMemoryRepository<,>));
      
      return services;
    }
}

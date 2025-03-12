using TicTacToe.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace TicTacToe.Presentation;

public static class ApplicationDependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
      return services;
    }
}

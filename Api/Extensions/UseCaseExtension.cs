using Core.Notifications;
using Infrastructure.Services;
using UseCase.UseCase;
using UseCase.UseCase.Interfaces;

namespace WebApi.Extensions;

public static class UseCaseExtension
{
	public static IServiceCollection AddUseCase(this IServiceCollection services)
	{
		return
			services
				.AddServices()
				.AddNotifications();
	}

	private static IServiceCollection AddServices(this IServiceCollection services)
	{
		return services
			.AddScoped<IUserUseCase, UserUseCase>()
			.AddScoped<Infrastructure.Services.Interfaces.IUserService, UserService>();
    }

	private static IServiceCollection AddNotifications(this IServiceCollection services)
	{
		return services
			.AddScoped<NotificationContext>();
	}
}
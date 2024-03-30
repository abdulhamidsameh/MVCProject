using Microsoft.Extensions.DependencyInjection;
using MVCProject.BLL;
using MVCProject.BLL.Interfaces;
using MVCProject.BLL.Repositories;

namespace MVCProject.PL.Extensions
{
	public static class ApplicationServicesExtensions
	{
		public static void AddApplicationServices(this IServiceCollection services)
		{
			services.AddScoped<IUnitOfWork, UnitOfWork>();
		}
	}
}

using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using MVCProject.BLL;
using MVCProject.BLL.Interfaces;
using MVCProject.BLL.Repositories;
using MVCProject.DAL.Data;
using MVCProject.DAL.Models;
using System;

namespace MVCProject.PL.Extensions
{
	public static class ApplicationServicesExtensions
	{
		public static void AddApplicationServices(this IServiceCollection services)
		{
			services.AddScoped<IUnitOfWork, UnitOfWork>();
			
			services.AddIdentity<ApplicationUser, IdentityRole>(options =>
			{
				options.Password.RequiredUniqueChars = 2;
				options.Password.RequireDigit = true;
				options.Password.RequireNonAlphanumeric = true;
				options.Password.RequireUppercase = true;
				options.Password.RequireLowercase = true;
				options.Password.RequiredLength = 5;

				options.Lockout.AllowedForNewUsers = true;
				options.Lockout.MaxFailedAccessAttempts = 5;
				options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromDays(5);

				options.User.RequireUniqueEmail = true;
			}).AddEntityFrameworkStores<MVCProjectDbContext>();

		}
	}
}

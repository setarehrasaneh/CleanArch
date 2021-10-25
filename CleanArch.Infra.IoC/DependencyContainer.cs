using CleanArch.Application.Interfaces;
using CleanArch.Application.Services;
using CleanArch.Domain.Interfaces;
using CleanArch.Infra.Data.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArch.Infra.IoC
{
    public class DependencyContainer
    {
        public static void RegisterServises(IServiceCollection servise)
        {
            servise.AddScoped<ICourseService, CourseService>();
            servise.AddScoped<ICourseRepository, CourseRepository>();
        }
    }
}

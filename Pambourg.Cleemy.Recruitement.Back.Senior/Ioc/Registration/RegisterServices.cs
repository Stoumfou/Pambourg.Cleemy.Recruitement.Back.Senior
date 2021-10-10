using Microsoft.Extensions.DependencyInjection;
using Pambourg.Cleemy.Recruitement.Back.Senior.Services;
using Pambourg.Cleemy.Recruitement.Back.Senior.Services.Interfaces;

namespace Pambourg.Cleemy.Recruitement.Back.Senior.Ioc.Registration
{
    public static partial class ServiceCollection
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<IExpenseService, ExpenseService>();
        }
    }
}

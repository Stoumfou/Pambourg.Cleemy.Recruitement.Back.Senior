using Microsoft.Extensions.DependencyInjection;
using Pambourg.Cleemy.Recruitement.Back.Senior.Repositories;
using Pambourg.Cleemy.Recruitement.Back.Senior.Repositories.Interfaces;

namespace Pambourg.Cleemy.Recruitement.Back.Senior.Ioc.Registration
{
    public static partial class ServiceCollection
    {
        public static void RegisterRepositories(this IServiceCollection services)
        {
            services.AddScoped<IExpenseRepository, ExpenseRepository>();
        }
    }
}

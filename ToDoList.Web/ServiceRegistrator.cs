using ToDoList.DAL.Base;
using ToDoList.DAL.QueryServices;
using ToDoList.DAL.Repositories;
using ToDoList.Model.Entities.ToDoTask;

namespace ToDoList.Web
{
    public static class ServiceRegistrator
    {
        public static void RegisterRequiredServices(IServiceCollection serviceCollection, IConfigurationManager configurationManager)
            => RegisterRequiredServices(serviceCollection, GetDbConnectionString(configurationManager));

        public static void RegisterRequiredServices(IServiceCollection serviceCollection, string connectionString)
        {
            serviceCollection.AddTransient<IAppDbContext>(x => new AppDbContext(connectionString));
            serviceCollection.AddScoped<IToDoTaskQueryService, ToDoTaskQueryService>();
            serviceCollection.AddScoped<IToDoTaskRepository, ToDoTaskRepository>();
            serviceCollection.AddScoped<IToDoTaskValidator, ToDoTaskValidator>();
        }

        private static string GetDbConnectionString(IConfigurationManager configurationManager)
            => configurationManager["ConnectionStrings.Database"]
            ?? throw new ApplicationException("Database connection info not found");
    }
}
